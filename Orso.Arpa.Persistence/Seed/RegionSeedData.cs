using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.RegionDomain.Commands;
using Orso.Arpa.Domain.RegionDomain.Model;

namespace Orso.Arpa.Persistence.Seed
{
    public static class RegionSeedData
    {
        public static IList<Region> Regions
        {
            get
            {
                return
                [
                    Freiburg,
                    StuttgartCity,
                    Berlin,
                    BadenWuerttemberg,
                    NorthGermany,
                    Germany,
                    Europe,
                    Tour,
                    Radius100Km,
                    BerlinMitte,
                    BerlinSchoeneberg,
                    Radius20Km,
                    Jamulus
                ];
            }
        }

        public static Region Freiburg
        {
            get
            {
                return new Region
                (
                    Guid.Parse("3e6c559e-8d50-488d-a1ea-5dbc0f44ba9b"),
                    new CreateRegion.Command
                    {
                        Name = "Freiburg",
                        IsForRehearsal = true
                    }
                );
            }
        }

        public static Region StuttgartCity
        {
            get
            {
                return new Region
                (
                    Guid.Parse("ac9544e3-e756-486c-a1dc-62988a882ac2"),
                    new CreateRegion.Command
                    {
                        Name = "Stuttgart City",
                        IsForRehearsal = true
                    }
                );
            }
        }

        public static Region Berlin
        {
            get
            {
                return new Region
                (
                    Guid.Parse("ca3c9cce-1aee-4c50-93e1-be963542741a"),
                    new CreateRegion.Command
                    {
                        Name = "Berlin",
                        IsForPerformance = true
                    }
                );
            }
        }

        public static Region BadenWuerttemberg
        {
            get
            {
                return new Region
                (
                    Guid.Parse("a3ed2672-19bc-4561-9147-490bc0778148"),
                    new CreateRegion.Command
                    {
                        Name = "Baden-Württemberg",
                        IsForPerformance = true
                    }
                );
            }
        }

        public static Region NorthGermany
        {
            get
            {
                return new Region
                (
                    Guid.Parse("1cb82c0c-304c-42bd-bfc1-a3f3e8a50cba"),
                    new CreateRegion.Command
                    {
                        Name = "North Germany",
                        IsForPerformance = true
                    }
                );
            }
        }

        public static Region Germany
        {
            get
            {
                return new Region
                (
                    Guid.Parse("f1208633-c4bb-4c07-adb3-39e3ac1e8703"),
                    new CreateRegion.Command
                    {
                        Name = "Germany",
                        IsForPerformance = true
                    }
                );
            }
        }

        public static Region Europe
        {
            get
            {
                return new Region
                (
                    Guid.Parse("b82dd9aa-4f80-45ca-82cb-db9d0d6ea47d"),
                    new CreateRegion.Command
                    {
                        Name = "Europe",
                        IsForPerformance = true
                    }
                );
            }
        }

        /// <summary>
        /// Tournee
        /// </summary>
        public static Region Tour
        {
            get
            {
                return new Region
                (
                    Guid.Parse("3ad098d3-7367-44f3-a1c3-685d2f8c7e81"),
                    new CreateRegion.Command
                    {
                        Name = "Tour",
                        IsForPerformance = true
                    }
                );
            }
        }

        public static Region Radius100Km
        {
            get
            {
                return new Region
                (
                    Guid.Parse("1e0b63cb-b25c-43cc-bdbf-d0b7f00d90da"),
                    new CreateRegion.Command
                    {
                        Name = "Up to a 100km radius from where I live",
                        IsForPerformance = true
                    }
                );
            }
        }

        public static Region BerlinMitte
        {
            get
            {
                return new Region
                (
                    Guid.Parse("92f9c1a1-0482-481b-8e34-307b4af017f0"),
                    new CreateRegion.Command
                    {
                        Name = "Berlin - Mitte",
                        IsForRehearsal = true
                    }
                );
            }
        }

        public static Region BerlinSchoeneberg
        {
            get
            {
                return new Region
                (
                    Guid.Parse("37d379f9-567f-4522-9301-2cf7308c669a"),
                    new CreateRegion.Command
                    {
                        Name = "Berlin - Schöneberg",
                        IsForRehearsal = true
                    }
                );
            }
        }

        public static Region Radius20Km
        {
            get
            {
                return new Region
                (
                    Guid.Parse("8abcbb9c-3940-4903-9ef0-ba2cffaac2bc"),
                    new CreateRegion.Command
                    {
                        Name = "Up to a 20km radius from where I live",
                        IsForRehearsal = true
                    }
                );
            }
        }

        public static Region Jamulus
        {
            get
            {
                return new Region
                (
                    Guid.Parse("47fbae86-05d6-4a7c-9225-a875ea29de4b"),
                    new CreateRegion.Command
                    {
                        Name = "Jamulus",
                        IsForRehearsal = true
                    }
                );
            }
        }
    }
}
