using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Regions
{
    public static class Create
    {
        public class Command : IRequest<Region>
        {
            public string Name { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Region>();
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IReadOnlyRepository repository)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(c => c.Name)
                    .MustAsync(async (name, cancellation) => !await repository.GetAll<Region>()
                        .ExistsAsync(r => r.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase), cancellation))
                    .WithMessage("A region with the requested name does already exist");
            }
        }

        public class Handler : IRequestHandler<Command, Region>
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

            public async Task<Region> Handle(Command request, CancellationToken cancellationToken)
            {
                var region = new Region(Guid.NewGuid(), request);

                Region createdRegion = await _repository.AddAsync(region);

                if (await _unitOfWork.CommitAsync())
                {
                    return createdRegion;
                }

                throw new Exception("Problem creating region");
            }
        }
    }
}
