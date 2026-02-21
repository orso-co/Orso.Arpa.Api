using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.TicketDomain.Model
{
    public class TicketLink : BaseEntity
    {
        public TicketLink(Guid? id, Guid ticketId, string label, string url) : base(id)
        {
            TicketId = ticketId;
            Label = label;
            Url = url;
        }

        [JsonConstructor]
        protected TicketLink()
        {
        }

        public Guid TicketId { get; private set; }
        public virtual Ticket Ticket { get; private set; }

        public string Label { get; private set; }
        public string Url { get; private set; }
    }
}
