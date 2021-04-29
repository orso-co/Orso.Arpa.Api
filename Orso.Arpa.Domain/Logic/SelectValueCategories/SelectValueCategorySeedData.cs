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
                    MusicianProfileInquiryStatusPerformer,
                    MusicianProfileInquiryStatusStaff,
                    MusicianProfileAvailableDocuments,
                    AuditionStatus,
                    AuditionRepetitorStatus
                };
            }
        }

        public static SelectValueCategory AddressType => new(
            Guid.Parse("d438c160-0588-41fa-93c3-cd33c0f97063"),
            nameof(PersonAddress),
            nameof(PersonAddress.Type),
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

        public static SelectValueCategory MusicianProfileInquiryStatusPerformer => new(
            Guid.Parse("d1ca913c-dee7-46d8-9fd4-ea564af8005f"),
            nameof(MusicianProfile),
            nameof(MusicianProfile.InquiryStatusPerformer),
            "Inquiry status performer");

        public static SelectValueCategory MusicianProfileInquiryStatusStaff => new(
            Guid.Parse("395ead29-7ecc-4999-b479-dffe97437e3a"),
            nameof(MusicianProfile),
            nameof(MusicianProfile.InquiryStatusStaff),
            "Inquiry status staff");

        public static SelectValueCategory MusicianProfileAvailableDocuments => new(
            Guid.Parse("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"),
            nameof(MusicianProfile),
            nameof(MusicianProfile.AvailableDocuments),
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
    }
}
