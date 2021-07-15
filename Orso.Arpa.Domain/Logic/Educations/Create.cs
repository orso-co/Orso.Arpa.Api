using System;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Educations
{
    public static class Create
    {
        public class Command : ICreateCommand<Education>
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
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.MusicianProfileId)
                    .EntityExists<Command, MusicianProfile>(arpaContext);

                RuleFor(c => c.TypeId)
                    .SelectValueMapping<Command, Education>(arpaContext, a => a.Type);
            }
        }
    }
}
