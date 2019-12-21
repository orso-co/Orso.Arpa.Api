using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Domain.Entities;
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
                    .ForAllOtherMembers(opt => opt.Ignore());
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
