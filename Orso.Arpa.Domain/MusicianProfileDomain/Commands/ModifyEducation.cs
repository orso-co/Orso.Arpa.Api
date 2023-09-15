using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.UserDomain.Enums;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Commands
{
    public static class ModifyEducation
    {
        public class Command : IModifyCommand<Education>
        {
            public Guid Id { get; set; }
            public string TimeSpan { get; set; }
            public string Institution { get; set; }
            public Guid TypeId { get; set; }
            public string Description { get; set; }
            public byte SortOrder { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext, ITokenAccessor tokenAccessor)
            {
                if (tokenAccessor.GetUserRoles().Contains(RoleNames.Staff))
                {
                    RuleFor(d => d.Id)
                        .EntityExists<Command, Education>(arpaContext);
                }
                else
                {
                    RuleFor(d => d.Id)
                        .MustAsync(async (id, cancellation) => await arpaContext
                            .EntityExistsAsync<Education>(e => e.Id == id && e.MusicianProfile.PersonId == tokenAccessor.PersonId, cancellation))
                        .WithErrorCode("404")
                        .WithMessage($"{typeof(CurriculumVitaeReference).Name} could not be found.");
                }
                RuleFor(c => c.TypeId)
                   .SelectValueMapping<Command, Education>(arpaContext, a => a.Type);
            }
        }
    }
}
