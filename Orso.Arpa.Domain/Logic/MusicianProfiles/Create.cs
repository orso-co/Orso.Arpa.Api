using System;
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
            public byte ProfilePreferencePerformer { get; set; }
            public byte ProfilePreferenceStaff { get; set; }
            public bool IsMainProfile { get; set; }
            public string Background { get; set; }
            public byte ExperienceLevel { get; set; }
            public string SalaryComment { get; set; }
            #endregion

            #region Reference
            public Guid PersonId { get; set; }
            public Guid InstrumentId { get; set; }
            public Guid? QualificationId { get; set; }
            public Guid? SalaryId { get; set; }
            public Guid? InquiryStatusPerformerId { get; set; }
            public Guid? InquiryStatusStaffId { get; set; }
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

                RuleFor(c => c.SalaryId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Salary);

                RuleFor(c => c.InquiryStatusPerformerId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusPerformer);

                RuleFor(c => c.InquiryStatusStaffId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusStaff);
            }
        }
    }
}
