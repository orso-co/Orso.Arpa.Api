using System;
using System.Collections.Generic;
using Orso.Arpa.Application.RegionApplication.Model;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class RegionDtoData
    {
        public static IList<RegionDto> Regions
        {
            get
            {
                var list = new List<RegionDto>();
                list.AddRange(RegionsForRehearsal);
                list.AddRange(RegionsForPerformance);
                return list;
            }
        }

        public static IList<RegionDto> RegionsForRehearsal
        {
            get
            {
                return
                [
                    Freiburg,
                    StuttgartCity,
                    BerlinMitte,
                    BerlinSchoeneberg,
                    Radius20Km,
                    Jamulus
                ];
            }
        }

        public static IList<RegionDto> RegionsForPerformance
        {
            get
            {
                return
                [
                    Berlin,
                    BadenWuerttemberg,
                    NorthGermany,
                    Germany,
                    Europe,
                    Tour,
                    Radius100Km
                ];
            }
        }

        public static RegionDto Freiburg
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("3e6c559e-8d50-488d-a1ea-5dbc0f44ba9b"),
                    Name = "Freiburg",
                    IsForRehearsal = true
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
                    Name = "Berlin",
                    IsForPerformance = true
                };
            }
        }

        public static RegionDto StuttgartCity
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("ac9544e3-e756-486c-a1dc-62988a882ac2"),
                    Name = "Stuttgart City",
                    IsForRehearsal = true
                };
            }
        }

        public static RegionDto BadenWuerttemberg
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("a3ed2672-19bc-4561-9147-490bc0778148"),
                    Name = "Baden-Württemberg",
                    IsForPerformance = true
                };
            }
        }

        public static RegionDto NorthGermany
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("1cb82c0c-304c-42bd-bfc1-a3f3e8a50cba"),
                    Name = "North Germany",
                    IsForPerformance = true
                };
            }
        }

        public static RegionDto Germany
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("f1208633-c4bb-4c07-adb3-39e3ac1e8703"),
                    Name = "Germany",
                    IsForPerformance = true
                };
            }
        }

        public static RegionDto Europe
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("b82dd9aa-4f80-45ca-82cb-db9d0d6ea47d"),
                    Name = "Europe",
                    IsForPerformance = true
                };
            }
        }

        public static RegionDto Tour
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("3ad098d3-7367-44f3-a1c3-685d2f8c7e81"),
                    Name = "Tour",
                    IsForPerformance = true
                };
            }
        }

        public static RegionDto Radius100Km
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("1e0b63cb-b25c-43cc-bdbf-d0b7f00d90da"),
                    Name = "Up to a 100km radius from where I live",
                    IsForPerformance = true
                };
            }
        }

        public static RegionDto BerlinMitte
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("92f9c1a1-0482-481b-8e34-307b4af017f0"),
                    Name = "Berlin - Mitte",
                    IsForRehearsal = true
                };
            }
        }

        public static RegionDto BerlinSchoeneberg
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("37d379f9-567f-4522-9301-2cf7308c669a"),
                    Name = "Berlin - Schöneberg",
                    IsForRehearsal = true
                };
            }
        }

        public static RegionDto Radius20Km
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("8abcbb9c-3940-4903-9ef0-ba2cffaac2bc"),
                    Name = "Up to a 20km radius from where I live",
                    IsForRehearsal = true
                };
            }
        }

        public static RegionDto Jamulus
        {
            get
            {
                return new RegionDto
                {
                    Id = Guid.Parse("47fbae86-05d6-4a7c-9225-a875ea29de4b"),
                    Name = "Jamulus",
                    IsForRehearsal = true
                };
            }
        }
    }
}
