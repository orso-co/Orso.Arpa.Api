using System;
using Orso.Arpa.Application.ProjectApplication;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class ReducedProjectDtoData
    {
        public static ReducedProjectDto RockingXMas
        {
            get
            {
                return new ReducedProjectDto
                {
                    Title = "Rocking X-mas Freiburg",
                    ShortTitle = "RockXmas",
                    Description = "Rocking around the christmas tree",
                    Code = "1005",
                    Id = Guid.Parse("a19d84f1-4ac1-49c3-abfe-527092b80b6d"),
                };
            }
        }

        public static ReducedProjectDto HoorayForHollywood
        {
            get
            {
                return new ReducedProjectDto
                {
                    Title = "Hooray for Hollywood Freiburg",
                    ShortTitle = "Hollywood",
                    Description = "Winter is Coming",
                    Code = "1006",
                    Id = Guid.Parse("8f8c500f-71f5-49be-92c8-150ac9e88219"),
                };
            }
        }

        public static ReducedProjectDto Schneekönigin
        {
            get
            {
                return new ReducedProjectDto
                {
                    Code = "1007",
                    Description = "Let it snow",
                    Id = Guid.Parse("b781c54d-8115-4561-b01e-9836fa05175e"),
                    ShortTitle = "Schnee",
                    Title = "Die Schneekönigin",
                };
            }
        }
    }
}
