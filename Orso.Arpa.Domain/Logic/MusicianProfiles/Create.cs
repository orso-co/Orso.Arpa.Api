using System;
using System.Collections.Generic;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.MusicianProfiles
{
    public static class Create
    {
        public class Command : ICreateCommand<MusicianProfile>
        {
            #region Native
            public byte LevelAssessmentPerformer { get; set; }
            public byte LevelAssessmentStaff { get; set; }
            #endregion

            #region Reference
            public Guid PersonId { get; set; }
            public virtual Person Person { get; set; }

            public Guid InstrumentId { get; set; }
            public virtual Section Instrument { get; set; }

            public Guid? QualificationId { get; set; }
            public virtual SelectValueMapping Qualification { get; set; }

            public Guid? InquiryStatusPerformerId { get; set; }
            public virtual SelectValueMapping InquiryStatusPerformer { get; set; }

            public Guid? InquiryStatusStaffId { get; set; }
            public virtual SelectValueMapping InquiryStatusStaff { get; set; }
            #endregion

            #region Collection
            public IList<MusicianProfileSection> DoublingInstruments { get; set; } = new List<MusicianProfileSection>();
            public IList<PreferredPosition> PreferredPositionsPerformer { get; set; } = new List<PreferredPosition>();
            //public IList<PreferredPosition> PreferredPositionsStaff { get; set; } = new List<PreferredPosition>();
            public IList<PreferredPart> PreferredPartsPerformer { get; set; } = new List<PreferredPart>();
            //public IList<PreferredPart> PreferredPartsStaff { get; set; } = new List<PreferredPart>();
            #endregion
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext, nameof(Command.PersonId));

                RuleFor(c => c.InstrumentId)
                    .EntityExists<Command, Section>(arpaContext, nameof(Command.InstrumentId));

                RuleFor(c => c.QualificationId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Qualification);

                RuleFor(c => c.InquiryStatusPerformerId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusPerformer);

                RuleFor(c => c.InquiryStatusStaffId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusStaff);

                //ToDo Validation for Collections
            }
        }
    }
}
