using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Persons
{
    public static class AddStakeholderGroup
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid StakeholderGroupId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                _ = CreateMap<Command, PersonSection>()
                    .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.SectionId, opt => opt.MapFrom(src => src.StakeholderGroupId));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(d => d.Id)
                    .EntityExists<Command, Person>(arpaContext);

                _ = RuleFor(d => d.StakeholderGroupId)
                    .Cascade(CascadeMode.Stop)
                    .EntityExists<Command, Section>(arpaContext)
                    .MustAsync(async (dto, stakeholderGroupId, cancellation) => !await arpaContext.Set<PersonSection>()
                        .AnyAsync(ar => ar.SectionId == stakeholderGroupId && ar.PersonId == dto.Id, cancellation))
                    .WithMessage("The stakeholder group is already linked to the person");
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
                Person existingPerson = await _arpaContext.Persons.FindAsync(new object[] { request.Id }, cancellationToken);
                Section existingSection = await _arpaContext.Sections.FindAsync(new object[] { request.StakeholderGroupId }, cancellationToken);

                var personSection = new PersonSection(null, existingPerson, existingSection);

                _ = _arpaContext.Set<PersonSection>().Add(personSection);

                return await _arpaContext.SaveChangesAsync(cancellationToken) > 0
                    ? Unit.Value
                    : throw new AffectedRowCountMismatchException(nameof(PersonSection));
            }
        }
    }
}
