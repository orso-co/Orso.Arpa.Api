using System;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.PersonDomain.Model
{
    public class PersonMembership : BaseEntity
    {
        public PersonMembership(Guid? id, CreatePersonMembership.Command command) : base(id)
        {
            EntryDate = command.EntryDate;
            ExitDate = command.ExitDate;
            AnnualFee = command.AnnualFee;
            SupportLevelId = command.SupportLevelId;
            MembershipStatusId = command.MembershipStatusId;
            PaymentMethodId = command.PaymentMethodId;
            PaymentFrequencyId = command.PaymentFrequencyId;
            ClubId = command.ClubId;
            StaffComment = command.StaffComment;
            PerformerComment = command.PerformerComment;
            PersonId = command.PersonId;
        }

        protected PersonMembership() { }

        public void Update(ModifyPersonMembership.Command command)
        {
            EntryDate = command.EntryDate;
            ExitDate = command.ExitDate;
            AnnualFee = command.AnnualFee;
            SupportLevelId = command.SupportLevelId;
            MembershipStatusId = command.MembershipStatusId;
            PaymentMethodId = command.PaymentMethodId;
            PaymentFrequencyId = command.PaymentFrequencyId;
            ClubId = command.ClubId;
            StaffComment = command.StaffComment;
            PerformerComment = command.PerformerComment;
        }

        // Dates
        public DateTime EntryDate { get; private set; }
        public DateTime? ExitDate { get; private set; }

        // Currency
        public decimal AnnualFee { get; private set; }

        // Support Level (Enum via SelectValueMapping: Sonata, Concerto, Symphony, Opera)
        public Guid? SupportLevelId { get; private set; }
        public virtual SelectValueMapping SupportLevel { get; private set; }

        // Membership Status (Enum via SelectValueMapping: Vollmitglied, Projektmitglied, Fördermitglied)
        public Guid? MembershipStatusId { get; private set; }
        public virtual SelectValueMapping MembershipStatus { get; private set; }

        // Payment Method (Enum via SelectValueMapping: Lastschrift, Überweisung, Bar, Paypal)
        public Guid? PaymentMethodId { get; private set; }
        public virtual SelectValueMapping PaymentMethod { get; private set; }

        // Payment Frequency (Enum via SelectValueMapping: Monatlich, Quartalsweise, Jährlich)
        public Guid? PaymentFrequencyId { get; private set; }
        public virtual SelectValueMapping PaymentFrequency { get; private set; }

        // Club (Enum via SelectValueMapping: ORSO Berlin, ORSO Freiburg, etc.)
        public Guid? ClubId { get; private set; }
        public virtual SelectValueMapping Club { get; private set; }

        // Comments
        [AuditLogIgnore]
        public string StaffComment { get; private set; }

        [AuditLogIgnore]
        public string PerformerComment { get; private set; }

        // Person relationship (1:n)
        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }
    }
}
