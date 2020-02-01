using System;
using System.Collections.Generic;
using Orso.Arpa.Application.Logic.Regions;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class RegionDtoData
    {
        public static IList<RegionDto> Regions
        {
            get
            {
                return new List<RegionDto>
                {
                    Freiburg,
                    Berlin,
                    Stuttgart
                };
            }
        }

        public static RegionDto Freiburg
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("3e6c559e-8d50-488d-a1ea-5dbc0f44ba9b"),
                    Name = "Freiburg"
                };
            }
        }

        public static RegionDto Berlin
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("ca3c9cce-1aee-4c50-93e1-be963542741a"),
                    Name = "Berlin"
                };
            }
        }

        public static RegionDto Stuttgart
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("ac9544e3-e756-486c-a1dc-62988a882ac2"),
                    Name = "Stuttgart"
                };
            }
        }
    }
}
