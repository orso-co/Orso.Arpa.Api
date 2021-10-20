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
                CreateMap<Command, PersonSection>()
                    .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.SectionId, opt => opt.MapFrom(src => src.StakeholderGroupId));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.Id)
                    .EntityExists<Command, Person>(arpaContext);

                RuleFor(d => d.StakeholderGroupId)
                    .Cascade(CascadeMode.Stop)
                    .EntityExists<Command, Section>(arpaContext)
                    .MustAsync(async (dto, stakeholderGroupId, cancellation) => !(await arpaContext.Set<PersonSection>()
                        .AnyAsync(ar => ar.SectionId == stakeholderGroupId && ar.PersonId == dto.Id, cancellation)))
                    .WithMessage("The stakeholder group is already linked to the person");
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
                Person existingPerson = await _arpaContext.Persons.FindAsync(new object[] { request.Id }, cancellationToken);
                Section existingSection = await _arpaContext.Sections.FindAsync(new object[] { request.StakeholderGroupId }, cancellationToken);

                var personSection = new PersonSection(null, existingPerson, existingSection);

                _arpaContext.Set<PersonSection>().Add(personSection);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem creating person section");
            }
        }
    }
}
