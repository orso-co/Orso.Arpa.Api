using System;
using Microsoft.Net.Http.Headers;
using Org.BouncyCastle.Crypto;
using Orso.Arpa.Domain.Logic.BankAccounts;

namespace Orso.Arpa.Domain.Entities
{
    public class BankAccount : BaseEntity
    {
        public BankAccount(Guid? id, Create.Command command) : base(id)
        {
            IBAN = command.IBAN;
            BIC = command.BIC;
            PersonId = command.PersonId;
            CommentInner = command.CommentInner;
        }
        protected BankAccount(){}

        public string IBAN { get; private set; }
        public string BIC { get; private set; }
        public Guid? StatusId { get; private set; }
        public virtual SelectValueMapping Status { get; private set; }
        public string CommentInner { get; private set; }

        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }

    }
}
