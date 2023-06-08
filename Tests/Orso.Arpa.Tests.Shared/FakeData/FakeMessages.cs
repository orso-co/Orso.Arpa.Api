using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeMessages
    {
        public static Message ErsteMessage
        {
            get
            {
                Message message = MessageSeedData.ErsteMessage;
                message.SetProperty(nameof(Message.CreatedBy), "anonymous");
                message.SetProperty(nameof(Message.CreatedAt), FakeDateTime.UtcNow);
                return message;
            }
        }

        public static Message ZweiteMessage
        {
            get
            {
                Message message = MessageSeedData.ZweiteMessage;
                message.SetProperty(nameof(Message.CreatedBy), "anonymous");
                message.SetProperty(nameof(Message.CreatedAt), FakeDateTime.UtcNow);
                return message;
            }
        }
    }
}
