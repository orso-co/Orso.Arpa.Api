using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Appointments
{
    public static class AddSection
    {
        public class Command : IRequest
        {
            public Command(Guid id, Guid sectionId)
            {
                Id = id;
                SectionId = sectionId;
            }

            public Command()
            {
            }

            public Guid Id { get; private set; }
            public Guid SectionId { get; private set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, SectionAppointment>()
                    .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.SectionId, opt => opt.MapFrom(src => src.SectionId));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.Id)
                    .EntityExists<Command, Appointment>(arpaContext);

                RuleFor(d => d.SectionId)
                    .EntityExists<Command, Section>(arpaContext)

                    .MustAsync(async (dto, sectionId, cancellation) => !(await arpaContext.SectionAppointments
                        .AnyAsync(sa => sa.SectionId == sectionId && sa.AppointmentId == dto.Id, cancellation)))
                    .WithMessage("The section is already linked to the appointment");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;
            private readonly IMapper _mapper;

            public Handler(
                IArpaContext arpaContext,
                IMapper mapper)
            {
                _arpaContext = arpaContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment existingAppointment = await _arpaContext.Appointments.FindAsync(new object[] { request.Id }, cancellationToken);
                Section existingSection = await _arpaContext.Sections.FindAsync(new object[] { request.SectionId }, cancellationToken);

                _arpaContext.SectionAppointments.Add(new SectionAppointment(existingSection, existingAppointment));

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem creating section appointment");
            }
        }
    }
}
