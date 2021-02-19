using System.Collections.Generic;
using Orso.Arpa.Application.SectionApplication;

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
            Children = new List<SectionTreeDto>
            {
                new SectionTreeDto { Data = SectionDtoData.Other, IsLeaf = true, Level = 1, IsRoot = false, Children = new List<SectionTreeDto>() },
                new SectionTreeDto { Data = SectionDtoData.Performers, IsLeaf = false, Level = 1, IsRoot = false, Children = new List<SectionTreeDto> {
                    new SectionTreeDto { Data = SectionDtoData.Choir, IsLeaf = true, Level = 2, IsRoot = false, Children = new List<SectionTreeDto>() },
                    new SectionTreeDto { Data = SectionDtoData.Orchestra, IsLeaf = true, Level = 2, IsRoot = false, Children = new List<SectionTreeDto>() },
                    new SectionTreeDto { Data = SectionDtoData.Band, IsLeaf = true, Level = 2, IsRoot = false, Children = new List<SectionTreeDto>() },
                    new SectionTreeDto { Data = SectionDtoData.Soloists, IsLeaf = true, Level = 2, IsRoot = false, Children = new List<SectionTreeDto>() },
                } },
                new SectionTreeDto { Data = SectionDtoData.Volunteers, IsLeaf = true, Level = 1, IsRoot = false, Children = new List<SectionTreeDto>() },
                new SectionTreeDto { Data = SectionDtoData.Visitors, IsLeaf = true, Level = 1, IsRoot = false, Children = new List<SectionTreeDto>() },
                new SectionTreeDto { Data = SectionDtoData.Crew, IsLeaf = false, Level = 1, IsRoot = false, Children = new List<SectionTreeDto> {
                    new SectionTreeDto { Data = SectionDtoData.Light, IsLeaf = true, Level = 2, IsRoot = false, Children = new List<SectionTreeDto>() },
                    new SectionTreeDto { Data = SectionDtoData.Media, IsLeaf = true, Level = 2, IsRoot = false, Children = new List<SectionTreeDto>() },
                    new SectionTreeDto { Data = SectionDtoData.Sound, IsLeaf = true, Level = 2, IsRoot = false, Children = new List<SectionTreeDto>() },
                    new SectionTreeDto { Data = SectionDtoData.Stage, IsLeaf = true, Level = 2, IsRoot = false, Children = new List<SectionTreeDto>() },
                } },
            }
        };
    }
}
