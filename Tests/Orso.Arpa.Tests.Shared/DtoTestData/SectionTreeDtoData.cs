using System.Collections.Generic;
using Orso.Arpa.Application.SectionApplication.Model;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class SectionTreeDtoData
    {
        public static SectionTreeDto Level2SectionTreeDto => new SectionTreeDto
        {
            Data = null,
            IsLeaf = false,
            IsRoot = true,
            Level = 0,
            Children =
            [
                new SectionTreeDto { Data = SectionDtoData.Performers,      IsLeaf = false, Level = 1, IsRoot = false, Children = [
                    new SectionTreeDto { Data = SectionDtoData.Conductor,   IsLeaf = true,  Level = 2, IsRoot = false, Children = [] },
                    new SectionTreeDto { Data = SectionDtoData.Choir,       IsLeaf = true,  Level = 2, IsRoot = false, Children = [] },
                    new SectionTreeDto { Data = SectionDtoData.Orchestra,   IsLeaf = true,  Level = 2, IsRoot = false, Children = [] },
                    new SectionTreeDto { Data = SectionDtoData.Band,        IsLeaf = true,  Level = 2, IsRoot = false, Children = [] },
                    new SectionTreeDto { Data = SectionDtoData.Soloists,    IsLeaf = true,  Level = 2, IsRoot = false, Children = [] },
                ] },
                new SectionTreeDto { Data = SectionDtoData.Members,         IsLeaf = true,  Level = 1, IsRoot = false, Children = [] },
                new SectionTreeDto { Data = SectionDtoData.Visitors,        IsLeaf = true,  Level = 1, IsRoot = false, Children = [] },
                new SectionTreeDto { Data = SectionDtoData.Volunteers,      IsLeaf = true,  Level = 1, IsRoot = false, Children = [] },
                new SectionTreeDto { Data = SectionDtoData.Suppliers,       IsLeaf = true,  Level = 1, IsRoot = false, Children = [] },
                new SectionTreeDto { Data = SectionDtoData.Contractors,     IsLeaf = true,  Level = 1, IsRoot = false, Children = [] },
            ]
        };
    }
}
