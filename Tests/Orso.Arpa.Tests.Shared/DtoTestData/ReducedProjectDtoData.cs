using System;
using Orso.Arpa.Application.ProjectApplication.Model;

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

        public static ReducedProjectDto ChorwerkstattFreiburg
        {
            get
            {
                return new ReducedProjectDto
                {
                    Code = "1003",
                    Description = "Ein Blick hinter die Kulissen…",
                    Id = Guid.Parse("a9668e17-e6df-4b08-8db5-30c88f89d78c"),
                    ShortTitle = "ChWeFr",
                    Title = "Chorwerkstatt Freiburg",
                };
            }
        }

        public static ReducedProjectDto ChorwerkstattTour
        {
            get
            {
                return new ReducedProjectDto
                {
                    Code = "1002",
                    Description = "Ein Blick hinter die Kulissen…",
                    Id = Guid.Parse("785f48b6-0f55-406f-8180-ec60501407d7"),
                    ShortTitle = "ChWeTour",
                    Title = "Chorwerkstatt",
                };
            }
        }

        public static ReducedProjectDto ChorwerkstattBerlin
        {
            get
            {
                return new ReducedProjectDto
                {
                    Code = "1004",
                    Description = "Ein Blick hinter die Kulissen…",
                    Id = Guid.Parse("f1bea5ee-6569-4db1-9a46-c425a5571be2"),
                    ShortTitle = "ChWeB",
                    Title = "Chorwerkstatt Berlin",
                };
            }
        }
    }
}
