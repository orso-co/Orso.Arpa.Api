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
    }
}
