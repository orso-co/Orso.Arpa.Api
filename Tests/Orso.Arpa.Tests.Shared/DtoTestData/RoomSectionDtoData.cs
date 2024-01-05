using System;
using Orso.Arpa.Application.RoomApplication.Model;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class RoomSectionDtoData
    {
        public static RoomSectionDto AulaWeiherhofSchulePiano
        {
            get
            {
                return new RoomSectionDto
                {
                    Id = Guid.Parse("b68a15ac-2c98-45aa-8655-c17388771783"),
                    Name = "Piano",
                    Quantity = 1,
                    Description = null
                };
            }
        }
    }
}
