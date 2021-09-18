using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Logic.SelectValueCategories
{
    public static class SelectValueCategorySeedData
    {
        public static IList<SelectValueCategory> SelectValueCategories
        {
            get
            {
                return new List<SelectValueCategory>
                {
                    AddressType,
                    AppointmentCategory,
                    AppointmentSalary,
                    AppointmentSalaryPattern,
                    AppointmentExpectation,
                    AppointmentParticipationPrediction,
                    AppointmentParticipationResult,
                    AppointmentStatus,
                    ProjectGenre,
                    ProjectType,
                    ProjectState,
                    MusicianProfileQualification,
                    MusicianProfileSalary,
                    MusicianProfileInquiryStatusInner,
                    MusicianProfileInquiryStatusTeam,
                    MusicianProfileDocuments,
                    AuditionStatus,
                    AuditionRepetitorStatus,
                    MusicianProfileSectionInstrumentAvailability,
                    ProjectParticipationInvitationStatus,
                    ProjectParticipationStatusInner,
                    ProjectParticipationStatusInternal,
                    EducationType,
                    CurriculumVitaeReferenceType,
                    PersonGender,
                    ContactDetailType,
                    BankAccountStatus
                };
            }
        }

        public static SelectValueCategory AddressType => new(
            Guid.Parse("d438c160-0588-41fa-93c3-cd33c0f97063"),
            nameof(Address),
            nameof(Address.Type),
            "Address Type");

        public static SelectValueCategory AppointmentParticipationResult => new(
            Guid.Parse("f5d4763e-5862-4b62-ab92-2748ad213b10"),
            nameof(AppointmentParticipation),
            nameof(AppointmentParticipation.Result),
            "Result");

        public static SelectValueCategory AppointmentParticipationPrediction => new(
            Guid.Parse("5cf52155-927f-4d64-a482-348f952bab21"),
            nameof(AppointmentParticipation),
            nameof(AppointmentParticipation.Prediction),
            "Prediction Participant");

        public static SelectValueCategory AppointmentExpectation => new(
            Guid.Parse("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"),
            nameof(Appointment),
            nameof(Appointment.Expectation),
            "Expectation KBB");

        public static SelectValueCategory ProjectGenre => new(
            Guid.Parse("4649b6b9-1362-41c2-ac5c-884f216dff30"),
            nameof(Project),
            nameof(Project.Genre),
            "Genre");

        public static SelectValueCategory ProjectState => new(
            Guid.Parse("9804d695-d8c7-40bd-814f-8458b55fb583"),
            nameof(Project),
            nameof(Project.State),
            "State");

        public static SelectValueCategory ProjectType => new(
            Guid.Parse("53ed1791-36d7-4534-867c-15175e6f4584"),
            nameof(Project),
            nameof(Project.Type),
            "Type");

        public static SelectValueCategory AppointmentCategory => new(
            Guid.Parse("791c7439-c72a-47ca-ad8d-193e2cad09e1"),
            nameof(Appointment),
            nameof(Appointment.Category),
            "Category");

        public static SelectValueCategory AppointmentStatus => new(
            Guid.Parse("09be8eff-72e4-40a8-a1ed-717deb185a69"),
            nameof(Appointment),
            nameof(Appointment.Status),
            "Status");

        public static SelectValueCategory AppointmentSalary => new(
            Guid.Parse("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"),
            nameof(Appointment),
            nameof(Appointment.Salary),
            "Salary");

        public static SelectValueCategory AppointmentSalaryPattern => new(
            Guid.Parse("e4ff93b9-318e-41ed-b067-51ee94f41adf"),
            nameof(Appointment),
            nameof(Appointment.SalaryPattern),
            "Salary Pattern");

        public static SelectValueCategory MusicianProfileQualification => new(
            Guid.Parse("9648daa0-c2b2-4b97-912b-7ce30b9534a8"),
            nameof(MusicianProfile),
            nameof(MusicianProfile.Qualification),
            "Qualification");

        public static SelectValueCategory MusicianProfileSalary => new(
            Guid.Parse("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"),
            nameof(MusicianProfile),
            nameof(MusicianProfile.Salary),
            "Salary");

        public static SelectValueCategory MusicianProfileInquiryStatusInner => new(
            Guid.Parse("d1ca913c-dee7-46d8-9fd4-ea564af8005f"),
            nameof(MusicianProfile),
            nameof(MusicianProfile.InquiryStatusInner),
            "Inquiry status performer");

        public static SelectValueCategory MusicianProfileInquiryStatusTeam => new(
            Guid.Parse("395ead29-7ecc-4999-b479-dffe97437e3a"),
            nameof(MusicianProfile),
            nameof(MusicianProfile.InquiryStatusTeam),
            "Inquiry status staff");

        public static SelectValueCategory MusicianProfileDocuments => new(
            Guid.Parse("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"),
            nameof(MusicianProfile),
            nameof(MusicianProfile.Documents),
            "Available document status");

        public static SelectValueCategory AuditionStatus => new(
            Guid.Parse("072c2a9a-a492-4190-9a49-505ff7056528"),
            nameof(Audition),
            nameof(Audition.Status),
            "Status");

        public static SelectValueCategory AuditionRepetitorStatus => new(
            Guid.Parse("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"),
            nameof(Audition),
            nameof(Audition.RepetitorStatus),
            "Repetitor status");

        public static SelectValueCategory MusicianProfileSectionInstrumentAvailability => new(
            Guid.Parse("e3756ad6-de58-4c22-9a7c-363bc33c613c"),
            nameof(MusicianProfileSection),
            nameof(MusicianProfileSection.InstrumentAvailability),
            "Instrument Availability");

        public static SelectValueCategory ProjectParticipationStatusInner => new(
            Guid.Parse("1bae5715-8363-4221-8735-8def3d2546e1"),
            nameof(ProjectParticipation),
            nameof(ProjectParticipation.ParticipationStatusInner),
            "Participation status inner");

        public static SelectValueCategory ProjectParticipationStatusInternal => new(
            Guid.Parse("13376e1d-2378-4e30-a6d2-808da4a4ba4d"),
            nameof(ProjectParticipation),
            nameof(ProjectParticipation.ParticipationStatusInternal),
            "Participation status internal");

        public static SelectValueCategory ProjectParticipationInvitationStatus => new(
            Guid.Parse("474775e9-f08a-4043-8474-e84f42bf3948"),
            nameof(ProjectParticipation),
            nameof(ProjectParticipation.InvitationStatus),
            "Participation invitation status");

        public static SelectValueCategory EducationType => new(
            Guid.Parse("502a47d4-6c2f-4729-99db-470f4e0e1a3b"),
            nameof(Education),
            nameof(Education.Type),
            "Education type");

        public static SelectValueCategory CurriculumVitaeReferenceType => new(
            Guid.Parse("3addf4f6-1904-4944-86f6-434d2660594f"),
            nameof(CurriculumVitaeReference),
            nameof(CurriculumVitaeReference.Type),
            "Curriculum vitae reference type");

        public static SelectValueCategory PersonGender => new(
            Guid.Parse("5d132bf0-5ad9-4a20-b23d-77efbb7acc0c"),
            nameof(Person),
            nameof(Person.Gender),
            "Person gender");

        public static SelectValueCategory ContactDetailType => new(
            Guid.Parse("3c4dd028-db94-441d-bd3f-ab5b58533407"),
            nameof(ContactDetail),
            nameof(ContactDetail.Type),
            "Contact detail type");

        public static SelectValueCategory BankAccountStatus => new(
            Guid.Parse("d75c2fe5-dba6-475e-a0f1-dd71285c0269"),
            nameof(BankAccount),
            nameof(BankAccount.Status),
            "Bank account status");
    }
}
