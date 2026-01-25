using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class CreateMusicPiecePart
    {
        public class Command : ICreateCommand<MusicPiecePart>
        {
            public Guid MusicPieceId { get; set; }
            public Guid? SectionId { get; set; }
            public string PartName { get; set; }
            public int SortOrder { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.MusicPieceId)
                    .EntityExists<Command, MusicPiece>(arpaContext);

                RuleFor(c => c.SectionId)
                    .EntityExists<Command, Section>(arpaContext)
                    .When(c => c.SectionId.HasValue);

                RuleFor(c => c.PartName)
                    .NotEmpty()
                    .MaximumLength(200);

                RuleFor(c => c.SortOrder)
                    .GreaterThanOrEqualTo(0);
            }
        }
    }
}
