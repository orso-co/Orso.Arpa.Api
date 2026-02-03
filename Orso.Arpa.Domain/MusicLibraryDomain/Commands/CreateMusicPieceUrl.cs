using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class CreateMusicPieceUrl
    {
        public class Command : ICreateCommand<MusicPieceUrl>
        {
            public Guid MusicPieceId { get; set; }
            public string Href { get; set; }
            public string AnchorText { get; set; }
            public Guid? UrlTypeId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.MusicPieceId)
                    .EntityExists<Command, MusicPiece>(arpaContext);

                RuleFor(c => c.Href)
                    .NotEmpty()
                    .MaximumLength(1000);

                RuleFor(c => c.AnchorText)
                    .MaximumLength(200);

                RuleFor(c => c.UrlTypeId)
                    .SelectValueMapping<Command, MusicPieceUrl>(arpaContext, u => u.UrlType);
            }
        }
    }
}
