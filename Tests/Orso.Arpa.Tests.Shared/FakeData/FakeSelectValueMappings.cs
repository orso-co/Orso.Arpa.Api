using Orso.Arpa.Domain.SelectValueDomain.Model;
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

        public static SelectValueMapping Rehearsal
        {
            get
            {
                SelectValueMapping selectValueMapping = SelectValueMappingSeedData.AppointmentCategoryMappings[0];
                selectValueMapping.SetProperty(nameof(SelectValueMapping.SelectValue), SelectValueSeedData.Rehearsal);
                return selectValueMapping;
            }
        }
    }
}
