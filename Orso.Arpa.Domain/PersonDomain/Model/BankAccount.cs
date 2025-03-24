using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.PersonDomain.Model
{
    public class BankAccount : BaseEntity
    {
        public BankAccount(Guid id, CreateBankAccount.Command command) : base(id)
        {
            Iban = command.Iban;
            Bic = command.Bic;
            PersonId = command.PersonId;
            CommentInner = command.CommentInner;
            AccountOwner = command.AccountOwner;
        }
        protected BankAccount() { }

        public void Update(ModifyBankAccount.Command command)
        {
            Iban = command.Iban;
            Bic = command.Bic;
            StatusId = command.StatusId;
            CommentInner = command.CommentInner;
            AccountOwner = command.AccountOwner;
        }

        public string Iban { get; private set; }
        public string Bic { get; private set; }
        public Guid? StatusId { get; private set; }
        public virtual SelectValueMapping Status { get; private set; }
        public string CommentInner { get; private set; }
        public string AccountOwner { get; private set; }

        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }
    }
}
