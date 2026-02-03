using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Delete;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class DeleteMusicPieceUrl
    {
        public class Command : IDeleteCommand<MusicPieceUrl>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, MusicPieceUrl>(arpaContext);
            }
        }
    }
}
