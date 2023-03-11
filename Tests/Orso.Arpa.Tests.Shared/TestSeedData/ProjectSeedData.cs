using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Projects;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class ProjectSeedData
    {
        public static IList<Project> Projects
        {
            get
            {
                return new List<Project>
                {
                    RockingXMas,
                    HoorayForHollywood,
                    Schneekönigin,
                    ChorwerkstattTour,
                    ChorwerkstattFreiburg,
                    ChorwerkstattBerlin
                };
            }
        }

        public static Project RockingXMas
        {
            get
            {
                return new Project(
                    Guid.Parse("a19d84f1-4ac1-49c3-abfe-527092b80b6d"),
                    new Create.Command
                    {
                        Title = "Rocking X-mas Freiburg",
                        ShortTitle = "RockXmas",
                        Description = "Rocking around the christmas tree",
                        Code = "1005",
                        TypeId = SelectValueMappingSeedData.ProjectTypeMappings[0].Id,
                        GenreId = SelectValueMappingSeedData.ProjectGenreMappings[0].Id,
                        StartDate = new DateTime(2020, 12, 24),
                        EndDate = new DateTime(2020, 12, 26),
                        Status = ProjectStatus.Pending,
                        ParentId = null,
                        IsCompleted = true,
                    });
            }
        }

        public static Project HoorayForHollywood
        {
            get
            {
                return new Project(
                    Guid.Parse("8f8c500f-71f5-49be-92c8-150ac9e88219"),
                    new Create.Command
                    {
                        Title = "Hooray for Hollywood Freiburg",
                        ShortTitle = "Hollywood",
                        Description = "Winter is Coming",
                        Code = "1006",
                        TypeId = SelectValueMappingSeedData.ProjectTypeMappings[1].Id,
                        GenreId = SelectValueMappingSeedData.ProjectGenreMappings[1].Id,
                        StartDate = new DateTime(2020, 05, 05),
                        EndDate = new DateTime(2020, 06, 06),
                        Status = ProjectStatus.Confirmed,
                        ParentId = null,
                        IsCompleted = false
                    });
            }
        }

        public static Project Schneekönigin
        {
            get
            {
                return new Project(
                    Guid.Parse("b781c54d-8115-4561-b01e-9836fa05175e"),
                    new Create.Command
                    {
                        Title = "Die Schneekönigin",
                        ShortTitle = "Schnee",
                        Description = "Let it snow",
                        Code = "1007",
                        TypeId = SelectValueMappingSeedData.ProjectTypeMappings[0].Id,
                        GenreId = SelectValueMappingSeedData.ProjectGenreMappings[0].Id,
                        StartDate = new DateTime(2020, 12, 1),
                        EndDate = new DateTime(2020, 12, 10),
                        Status = ProjectStatus.Pending,
                        ParentId = null,
                        IsCompleted = false,
                    });
            }
        }

        public static Project ChorwerkstattTour
        {
            get
            {
                return new Project(
                    Guid.Parse("785f48b6-0f55-406f-8180-ec60501407d7"),
                    new Create.Command
                    {
                        Title = "Chorwerkstatt",
                        ShortTitle = "ChWeTour",
                        Description = "Ein Blick hinter die Kulissen…",
                        Code = "1002",
                        TypeId = SelectValueMappingSeedData.ProjectTypeMappings[1].Id,
                        GenreId = SelectValueMappingSeedData.ProjectGenreMappings[3].Id,
                        StartDate = new DateTime(2020, 9, 1),
                        EndDate = new DateTime(2020, 11, 30),
                        Status = ProjectStatus.Confirmed,
                        ParentId = null,
                        IsCompleted = false,
                    });
            }
        }

        public static Project ChorwerkstattFreiburg
        {
            get
            {
                return new Project(
                    Guid.Parse("a9668e17-e6df-4b08-8db5-30c88f89d78c"),
                    new Create.Command
                    {
                        Title = "Chorwerkstatt Freiburg",
                        ShortTitle = "ChWeFr",
                        Description = "Ein Blick hinter die Kulissen…",
                        Code = "1003",
                        TypeId = SelectValueMappingSeedData.ProjectTypeMappings[0].Id,
                        GenreId = SelectValueMappingSeedData.ProjectGenreMappings[3].Id,
                        StartDate = new DateTime(2020, 10, 10),
                        EndDate = new DateTime(2020, 10, 30),
                        Status = ProjectStatus.Cancelled,
                        ParentId = Guid.Parse("785f48b6-0f55-406f-8180-ec60501407d7"),
                        IsCompleted = false,
                    });
            }
        }

        public static Project ChorwerkstattBerlin
        {
            get
            {
                return new Project(
                    Guid.Parse("f1bea5ee-6569-4db1-9a46-c425a5571be2"),
                    new Create.Command
                    {
                        Title = "Chorwerkstatt Berlin",
                        ShortTitle = "ChWeB",
                        Description = "Ein Blick hinter die Kulissen…",
                        Code = "1004",
                        TypeId = SelectValueMappingSeedData.ProjectTypeMappings[0].Id,
                        GenreId = SelectValueMappingSeedData.ProjectGenreMappings[3].Id,
                        StartDate = new DateTime(2020, 11, 1),
                        EndDate = new DateTime(2020, 11, 30),
                        Status = ProjectStatus.Confirmed,
                        ParentId = Guid.Parse("785f48b6-0f55-406f-8180-ec60501407d7"),
                        IsCompleted = false,
                    });
            }
        }
    }
}
