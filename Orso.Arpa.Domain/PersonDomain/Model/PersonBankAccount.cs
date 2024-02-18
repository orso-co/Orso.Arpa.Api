using System;
using Orso.Arpa.Domain._General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.PersonDomain.Model
{
    public class PersonBankAccount : BaseEntity
    {
        public PersonBankAccount(Guid? id, CreateBankAccount.Command command) : base(id)
        {
            PersonId = command.PersonId;
            BankAccount = new BankAccount(command.AccountOwner, command.Iban, command.Bic);
        }
        protected PersonBankAccount() { }

        public void Update(ModifyBankAccount.Command command)
        {
            StatusId = command.StatusId;
            CommentInner = command.CommentInner;
            BankAccount = BankAccount with { Iban = command.Iban, Bic = command.Bic, AccountOwner = command.AccountOwner };
        }

        public BankAccount BankAccount { get; private set; }
        public Guid? StatusId { get; private set; }
        public virtual SelectValueMapping Status { get; private set; }
        public string CommentInner { get; private set; }
        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }
    }
}
