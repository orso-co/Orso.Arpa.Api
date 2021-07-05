using System;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.CurriculumVitaeReferences
{
    public static class Create
    {
        public class Command : ICreateCommand<CurriculumVitaeReference>
        {
            public Command(string timeSpan, string institution, Guid typeId,
                string description, byte sortOrder, Guid musicianProfileId)
            {
                TimeSpan = timeSpan;
                Institution = institution;
                TypeId = typeId;
                Description = description;
                SortOrder = sortOrder;
                MusicianProfileId = musicianProfileId;
            }

            public Command()
            {
            }

            public string TimeSpan { get; set; }
            public string Institution { get; set; }
            public Guid? TypeId { get; set; }
            public string Description { get; set; }
            public byte? SortOrder { get; set; }
            public Guid MusicianProfileId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext, ITokenAccessor tokenAccessor)
            {
                if (tokenAccessor.UserRoles.Contains(RoleNames.Staff))
                {
                    RuleFor(c => c.MusicianProfileId)
                        .EntityExists<Command, MusicianProfile>(arpaContext, nameof(Command.MusicianProfileId));
                }
                else
                {
                    RuleFor(c => c.MusicianProfileId)
                        .MustAsync(async (musicianProfileId, cancellation) => await arpaContext
                            .EntityExistsAsync<MusicianProfile>(mp => mp.Id == musicianProfileId && mp.PersonId == tokenAccessor.PersonId, cancellation))
                        .OnFailure(_ => throw new AuthorizationException("This musician profile is not yours. You don't have access to this musician profile."));
                }

                RuleFor(c => c.TypeId)
                    .SelectValueMapping<Command, CurriculumVitaeReference>(arpaContext, a => a.Type);
            }
        }
    }
}
