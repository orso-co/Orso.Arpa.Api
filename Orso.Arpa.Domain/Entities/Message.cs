using System;
using Orso.Arpa.Domain.Logic.Messages;

namespace Orso.Arpa.Domain.Entities
{
    public class Message : BaseEntity
    {
        public Message(Guid? id, Create.Command command) : base(id)
        {
            MessageText = command.MessageText;
            Url = command.Url;
            Show = command.Show;
        }
        protected Message() {}

        public void Update(Modify.Command command)
        {
            MessageText = command.MessageText;
            Url = command.Url;
            Show = command.Show;
        }

        public string MessageText { get; private set; }
        public string Url { get; private set; }
        public bool Show { get; private set; }
    }
}
