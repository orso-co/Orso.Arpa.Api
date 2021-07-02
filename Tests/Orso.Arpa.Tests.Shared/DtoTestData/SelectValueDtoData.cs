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
    }
}
