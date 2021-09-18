using System;
using Microsoft.Net.Http.Headers;
using Org.BouncyCastle.Crypto;

namespace Orso.Arpa.Domain.Entities
{
    public class BankAccount : BaseEntity
    {
        public BankAccount ()
        {

        }
        public string IBAN { get; private set; }
        public string BIC { get; private set; }
        public Guid? StatusId { get; private set; }
        public virtual SelectValueMapping Status { get; private set; }
        public string CommentInner { get; private set; }

        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }

    }
}
