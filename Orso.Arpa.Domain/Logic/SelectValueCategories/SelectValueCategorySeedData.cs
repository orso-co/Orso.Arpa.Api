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
                    AppointmentEmolument,
                    AppointmentEmolumentPattern,
                    AppointmentExpectation,
                    AppointmentParticipationPrediction,
                    AppointmentParticipationResult,
                    AppointmentStatus,
                    ProjectGenre
                };
            }
        }

        public static SelectValueCategory AddressType
        {
            get
            {
                return new SelectValueCategory(
                    Guid.Parse("d438c160-0588-41fa-93c3-cd33c0f97063"),
                    nameof(PersonAddress),
                    nameof(PersonAddress.Type),
                    "Address Type");
            }
        }

        public static SelectValueCategory AppointmentParticipationResult
        {
            get
            {
                return new SelectValueCategory(
                    Guid.Parse("f5d4763e-5862-4b62-ab92-2748ad213b10"),
                    nameof(AppointmentParticipation),
                    nameof(AppointmentParticipation.Result),
                    "Result");
            }
        }

        public static SelectValueCategory AppointmentParticipationPrediction
        {
            get
            {
                return new SelectValueCategory(
                    Guid.Parse("5cf52155-927f-4d64-a482-348f952bab21"),
                    nameof(AppointmentParticipation),
                    nameof(AppointmentParticipation.Prediction),
                    "Prediction Participant");
            }
        }

        public static SelectValueCategory AppointmentExpectation
        {
            get
            {
                return new SelectValueCategory(
                    Guid.Parse("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"),
                    nameof(Appointment),
                    nameof(Appointment.Expectation),
                    "Expectation KBB");
            }
        }

        public static SelectValueCategory ProjectGenre
        {
            get
            {
                return new SelectValueCategory(
                    Guid.Parse("4649b6b9-1362-41c2-ac5c-884f216dff30"),
                    nameof(Project),
                    nameof(Project.Genre),
                    "Genre");
            }
        }

        public static SelectValueCategory ProjectState
        {
            get
            {
                return new SelectValueCategory(
                    Guid.Parse("9804d695-d8c7-40bd-814f-8458b55fb583"),
                    nameof(Project),
                    nameof(Project.State),
                    "State");
            }
        }

        public static SelectValueCategory ProjectType
        {
            get
            {
                return new SelectValueCategory(
                    Guid.Parse("53ed1791-36d7-4534-867c-15175e6f4584"),
                    nameof(Project),
                    nameof(Project.Type),
                    "State");
            }
        }

        public static SelectValueCategory AppointmentCategory
        {
            get
            {
                return new SelectValueCategory(
                    Guid.Parse("791c7439-c72a-47ca-ad8d-193e2cad09e1"),
                    nameof(Appointment),
                    nameof(Appointment.Category),
                    "Category");
            }
        }

        public static SelectValueCategory AppointmentStatus
        {
            get
            {
                return new SelectValueCategory(
                    Guid.Parse("09be8eff-72e4-40a8-a1ed-717deb185a69"),
                    nameof(Appointment),
                    nameof(Appointment.Status),
                    "Status");
            }
        }

        public static SelectValueCategory AppointmentEmolument
        {
            get
            {
                return new SelectValueCategory(
                    Guid.Parse("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"),
                    nameof(Appointment),
                    nameof(Appointment.Emolument),
                    "Emolument");
            }
        }

        public static SelectValueCategory AppointmentEmolumentPattern
        {
            get
            {
                return new SelectValueCategory(
                    Guid.Parse("e4ff93b9-318e-41ed-b067-51ee94f41adf"),
                    nameof(Appointment),
                    nameof(Appointment.EmolumentPattern),
                    "Emolument Pattern");
            }
        }
    }
}
