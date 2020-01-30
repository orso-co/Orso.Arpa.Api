using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Regions
{
    public static class Modify
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Region>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IReadOnlyRepository repository)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(c => c.Id)
                    .MustAsync(async (dto, id, cancellation) => await repository.GetByIdAsync<Region>(id) != null)
                    .OnFailure(dto => throw new RestException("Region not found", HttpStatusCode.NotFound, new { Id = "Not found" }));
                RuleFor(c => c.Name)
                    .MustAsync(async (dto, name, cancellation) => !await repository.GetAll<Region>()
                        .ExistsAsync(r => r.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
                            && r.Id != dto.Id, cancellation))
                    .WithMessage("A region with the requested name does already exist");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IRepository _repository;
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(
                IRepository repository,
                IUnitOfWork unitOfWork,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Region existingRegion = await _repository.GetByIdAsync<Region>(request.Id);

                Region region = _mapper.Map(request, existingRegion);

                _repository.Update(region);

                if (await _unitOfWork.CommitAsync())
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating region");
            }
        }
    }
}
