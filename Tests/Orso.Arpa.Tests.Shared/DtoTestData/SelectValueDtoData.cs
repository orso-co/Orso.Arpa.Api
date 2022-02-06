using System;
using System.Collections.Generic;
using Orso.Arpa.Application.SelectValueApplication;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class SelectValueDtoData
    {
        public static IList<SelectValueDto> ProjectGenres
        {
            get
            {
                return new List<SelectValueDto>
                {
                    ClassicalMusic,
                    Crossover,
                    ChamberMusic,
                    FilmMusic,
                    DancePerformance,
                    ContemporaryMusic,
                };
            }
        }

        public static SelectValueDto ClassicalMusic => new()
        {
            Id = Guid.Parse("d733e38d-1d80-4054-b654-4ea4a128b0a8"),
            Name = "Classical Music",
            Description = "",
        };

        public static SelectValueDto ChamberMusic => new()
        {
            Id = Guid.Parse("29e1142f-aa9e-4b94-ae21-9a63f7b65c15"),
            Name = "Chamber Music",
            Description = "",
        };

        public static SelectValueDto ContemporaryMusic => new()
        {
            Id = Guid.Parse("4ef47024-d8a5-4b2d-8584-aeb29263dddb"),
            Name = "Contemporary Music",
            Description = "",
        };

        public static SelectValueDto Crossover => new()
        {
            Id = Guid.Parse("e7e78e76-3850-4eb5-892f-d90be6c256a4"),
            Name = "Crossover",
            Description = "",
        };

        public static SelectValueDto DancePerformance => new()
        {
            Id = Guid.Parse("8daa5ae4-3885-4739-803a-693c7cfdf314"),
            Name = "Dance Performance",
            Description = "",
        };

        public static SelectValueDto FilmMusic => new()
        {
            Id = Guid.Parse("5578f637-14b7-4c11-85a8-0b94d83da678"),
            Name = "Film Music",
            Description = "",
        };

        public static SelectValueDto Female => new()
        {
            Id = Guid.Parse("32761c45-e481-4eb9-a23e-d73330482572"),
            Name = "Female",
            Description = "",
        };

        public static SelectValueDto Diverse => new()
        {
            Id = Guid.Parse("88d680fe-b6cc-486f-8f79-2525189b8b13"),
            Name = "Diverse",
            Description = "",
        };

        public static SelectValueDto Male => new()
        {
            Id = Guid.Parse("1c16a5fe-6ac6-4e94-be6e-82a0a0fbe1c9"),
            Name = "Male",
            Description = "",
        };

        public static SelectValueDto PrivateContactDetail => new()
        {
            Id = Guid.Parse("f0bf8326-623e-4caa-bd92-bc05c721a6cf"),
            Name = "Private",
            Description = "",
        };

        public static SelectValueDto PrivateAddress => new()
        {
            Id = Guid.Parse("fb44b625-7086-48e6-bcc6-a004dd472012"),
            Name = "Private",
            Description = "",
        };

        public static SelectValueDto Acceptance => new()
        {
            Id = Guid.Parse("eef4a4d1-796b-4b37-96f6-f31dbccf0aeb"),
            Name = "Acceptance",
            Description = "",
        };

        public static SelectValueDto Candidate => new()
        {
            Id = Guid.Parse("b0dcb5e9-bbc6-4004-b9d7-0f6723416b9b"),
            Name = "Candidate",
            Description = "",
        };
    }
}
