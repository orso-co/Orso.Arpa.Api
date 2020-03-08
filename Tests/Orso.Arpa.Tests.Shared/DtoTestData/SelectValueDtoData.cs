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
                    ChamberMusic
                };
            }
        }

        public static SelectValueDto ClassicalMusic
        {
            get
            {
                return new SelectValueDto
                {
                    Id = Guid.Parse("d733e38d-1d80-4054-b654-4ea4a128b0a8"),
                    Name = "Classical Music",
                    Description = "",
                };
            }
        }

        public static SelectValueDto Crossover
        {
            get
            {
                return new SelectValueDto
                {
                    Id = Guid.Parse("e7e78e76-3850-4eb5-892f-d90be6c256a4"),
                    Name = "Crossover",
                    Description = "",
                };
            }
        }

        public static SelectValueDto ChamberMusic
        {
            get
            {
                return new SelectValueDto
                {
                    Id = Guid.Parse("29e1142f-aa9e-4b94-ae21-9a63f7b65c15"),
                    Name = "Chamber Music",
                    Description = "",
                };
            }
        }
    }
}
