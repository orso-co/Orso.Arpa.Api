using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeSelectValueMappings
    {
        public static SelectValueMapping Mandatory
        {
            get
            {
                SelectValueMapping selectValueMapping = SelectValueMappingSeedData.AppointmentExpectationMappings[2];
                selectValueMapping.SetProperty(nameof(SelectValueMapping.SelectValue), SelectValueSeedData.Mandatory);
                return selectValueMapping;
            }
        }

        public static SelectValueMapping Invited
        {
            get
            {
                SelectValueMapping selectValueMapping = SelectValueMappingSeedData.ProjectParticipationInvitationStatusMappings[0];
                selectValueMapping.SetProperty(nameof(SelectValueMapping.SelectValue), SelectValueSeedData.Invited);
                return selectValueMapping;
            }
        }

        public static SelectValueMapping Candidate
        {
            get
            {
                SelectValueMapping selectValueMapping = SelectValueMappingSeedData.ProjectParticipationStatusInternalMappings[0];
                selectValueMapping.SetProperty(nameof(SelectValueMapping.SelectValue), SelectValueSeedData.Candidate);
                return selectValueMapping;
            }
        }

        public static SelectValueMapping Acceptance
        {
            get
            {
                SelectValueMapping selectValueMapping = SelectValueMappingSeedData.ProjectParticipationStatusInnerMappings[1];
                selectValueMapping.SetProperty(nameof(SelectValueMapping.SelectValue), SelectValueSeedData.Acceptance);
                return selectValueMapping;
            }
        }

        public static SelectValueMapping Amateur
        {
            get
            {
                SelectValueMapping selectValueMapping = SelectValueMappingSeedData.MusicianProfileQualificationMappings[0];
                selectValueMapping.SetProperty(nameof(SelectValueMapping.SelectValue), SelectValueSeedData.Amateur);
                return selectValueMapping;
            }
        }

        public static SelectValueMapping Concert
        {
            get
            {
                SelectValueMapping selectValueMapping = SelectValueMappingSeedData.ProjectTypeMappings[0];
                selectValueMapping.SetProperty(nameof(SelectValueMapping.SelectValue), SelectValueSeedData.Concert);
                return selectValueMapping;
            }
        }

        public static SelectValueMapping ClassicalMusic
        {
            get
            {
                SelectValueMapping selectValueMapping = SelectValueMappingSeedData.ProjectGenreMappings[0];
                selectValueMapping.SetProperty(nameof(SelectValueMapping.SelectValue), SelectValueSeedData.ClassicalMusic);
                return selectValueMapping;
            }
        }

        public static SelectValueMapping Pending
        {
            get
            {
                SelectValueMapping selectValueMapping = SelectValueMappingSeedData.ProjectStateMappings[0];
                selectValueMapping.SetProperty(nameof(SelectValueMapping.SelectValue), SelectValueSeedData.Pending);
                return selectValueMapping;
            }
        }
    }
}
