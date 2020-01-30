using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Appointments
{
    public static class Modify
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid? CategoryId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string Name { get; set; }
            public string PublicDetails { get; set; }
            public string InternalDetails { get; set; }
            public Guid? StatusId { get; set; }
            public Guid? EmolumentId { get; set; }
            public Guid? EmolumentPatternId { get; set; }
            public Guid? ExpectationId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Appointment>()
                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                    .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                    .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.PublicDetails, opt => opt.MapFrom(src => src.PublicDetails))
                    .ForMember(dest => dest.InternalDetails, opt => opt.MapFrom(src => src.InternalDetails))
                    .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                    .ForMember(dest => dest.EmolumentId, opt => opt.MapFrom(src => src.EmolumentId))
                    .ForMember(dest => dest.EmolumentPatternId, opt => opt.MapFrom(src => src.EmolumentPatternId))
                    .ForMember(dest => dest.ExpectationId, opt => opt.MapFrom(src => src.ExpectationId))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IReadOnlyRepository readOnlyRepository)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(d => d.Id)
                    .MustAsync(async (id, cancellation) => await readOnlyRepository.GetByIdAsync<Appointment>(id) != null)
                    .OnFailure(dto => throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Id = "Not found" }));
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
                Appointment existingAppointment = await _repository.GetByIdAsync<Appointment>(request.Id);

                Appointment appointment = _mapper.Map(request, existingAppointment);

                _repository.Update(appointment);

                if (await _unitOfWork.CommitAsync())
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating appointment");
            }
        }
    }
}
