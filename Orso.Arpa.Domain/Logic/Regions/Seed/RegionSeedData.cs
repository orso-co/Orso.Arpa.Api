using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Regions;

namespace Orso.Arpa.Domain.Regions.Seed
{
    public static class RegionSeedData
    {
        public static IList<Region> Regions
        {
            get
            {
                return new List<Region>
                {
                    Freiburg,
                    Stuttgart,
                    Berlin
                };
            }
        }

        public static Region Freiburg
        {
            get
            {
                return new Region
                (
                    Guid.Parse("3e6c559e-8d50-488d-a1ea-5dbc0f44ba9b"),
                    new Create.Command
                    {
                        Name = "Freiburg"
                    }
                );
                ;
            }
        }

        public static Region Stuttgart
        {
            get
            {
                return new Region
                (
                    Guid.Parse("ac9544e3-e756-486c-a1dc-62988a882ac2"),
                    new Create.Command
                    {
                        Name = "Stuttgart"
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
                    new Create.Command
                    {
                        Name = "Berlin"
                    }
                );
            }
        }
    }
}
