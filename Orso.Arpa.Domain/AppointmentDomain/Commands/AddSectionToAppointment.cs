using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public static class AddSectionToAppointment
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
                    .MustAsync(async (dto, sectionId, cancellation) => !(await arpaContext
                        .EntityExistsAsync<SectionAppointment>(sa => sa.SectionId == sectionId && sa.AppointmentId == dto.Id, cancellation)))
                    .WithMessage("The section is already linked to the appointment");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;
            public Handler(
                IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment existingAppointment = await _arpaContext.Set<Appointment>().FindAsync([request.Id], cancellationToken);
                Section existingSection = await _arpaContext.Set<Section>().FindAsync([request.SectionId], cancellationToken);

                await _arpaContext.Set<SectionAppointment>().AddAsync(new SectionAppointment(null, existingSection, existingAppointment), cancellationToken);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(SectionAppointment));
            }
        }
    }
}
