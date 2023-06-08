using System;
using System.Collections.Generic;
using Orso.Arpa.Application.MessageApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class MessageDtoData
    {
        public static IList<MessageDto> Messages
        {
            get
            {
                return new List<MessageDto>
                {
                    Performer,
                    Staff
                };
            }
        }

        public static MessageDto Staff
        {
            get
            {
                return new MessageDto
                {
                    Id = Guid.Parse("416981c5-2512-442f-8b2e-dd9364faf40f"),
                    MessageText = "ErsteMessage",
                    Url = "https://orsopolis.de",
                    Show = true,
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

        public static MessageDto Performer
        {
            get
            {
                return new MessageDto
                {
                    Id = Guid.Parse("116232e3-f972-4d3e-bd98-5ead7b76cff8"),
                    MessageText = "ZweiteMessage",
                    Url = "https://orso.co",
                    Show = false,
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

    }
}
