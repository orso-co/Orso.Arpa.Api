using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Messages;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class MessageSeedData
    {
        public static IList<Message> Messages
        {
            get
            {
                return new List<Message>
                {
                    ErsteMessage,
                    ZweiteMessage
                };
            }
        }

        public static Message ErsteMessage
        {
            get
            {
                return new Message
                (
                 Guid.Parse("416981c5-2512-442f-8b2e-dd9364faf40f"),
                    new Create.Command
                    {
                        MessageText = "ErsteMessage",
                        Url = "https://orsopolis.de",
                        Show = true
                    }
                );
            }
        }
        public static Message ZweiteMessage
        {
            get
            {
                return new Message
                (
                    Guid.Parse("116232e3-f972-4d3e-bd98-5ead7b76cff8"),
                    new Create.Command
                    {
                        MessageText = "ZweiteMessage",
                        Url = "https://orso.co",
                        Show = false
                    }
                );
            }
        }
    }
}
