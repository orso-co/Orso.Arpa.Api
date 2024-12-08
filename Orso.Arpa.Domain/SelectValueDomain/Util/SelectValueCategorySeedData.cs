using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.AddressDomain.Model;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Domain.SelectValueDomain.Util
{
    public static class SelectValueCategorySeedData
    {
        public static IList<SelectValueCategory> SelectValueCategories
        {
            get
            {
                return
                [
                    AddressType,
                    AppointmentCategory,
                    AppointmentSalary,
                    AppointmentSalaryPattern,
                    AppointmentExpectation,
                    ProjectGenre,
                    ProjectType,
                    MusicianProfileQualification,
                    MusicianProfileSalary,
                    MusicianProfileDocuments,
                    AuditionStatus,
                    AuditionRepetitorStatus,
                    MusicianProfileSectionInstrumentAvailability,
                    EducationType,
                    CurriculumVitaeReferenceType,
                    PersonGender,
                    ContactDetailType,
                    BankAccountStatus,
                    RoomEquipmentType,
                    RoomCapacity
                ];
            }
        }

        public static SelectValueCategory AddressType => new(
            Guid.Parse("d438c160-0588-41fa-93c3-cd33c0f97063"),
            nameof(Address),
            nameof(Address.Type),
            "Address Type");

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

        public static SelectValueCategory RoomEquipmentType => new(
            Guid.Parse("29a3e970-6650-4050-8cc8-2f5120b7fec9"),
            nameof(RoomEquipment),
            nameof(RoomEquipment.Equipment),
            "Room equipment type");

        public static SelectValueCategory RoomCapacity => new(
            Guid.Parse("a0f655d9-2044-4a79-b717-118e7397e697"),
            nameof(Room),
            nameof(Room.Capacity),
            "Room capacity");
    }
}
