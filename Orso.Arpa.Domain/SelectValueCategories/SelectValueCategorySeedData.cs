using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.SelectValueMappings.Seed;

namespace Orso.Arpa.Domain.SelectValueCategories
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
                    AppointmentParticipationExpectation,
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
                var category = new SelectValueCategory(
                    Guid.Parse("d438c160-0588-41fa-93c3-cd33c0f97063"),
                    nameof(Address),
                    nameof(Address.Type),
                    "Address Type");
                foreach (SelectValueMapping mapping in SelectValueMappingSeedData.AddressTypeMappings)
                {
                    category.SelectValueMappings.Add(mapping);
                }
                return category;
            }
        }

        public static SelectValueCategory AppointmentParticipationResult
        {
            get
            {
                var category = new SelectValueCategory(
                    Guid.Parse("f5d4763e-5862-4b62-ab92-2748ad213b10"),
                    nameof(AppointmentParticipation),
                    nameof(AppointmentParticipation.Result),
                    "Result");
                foreach (SelectValueMapping mapping in SelectValueMappingSeedData.AppointmentParticipationResultMappings)
                {
                    category.SelectValueMappings.Add(mapping);
                }
                return category;
            }
        }

        public static SelectValueCategory AppointmentParticipationPrediction
        {
            get
            {
                var category = new SelectValueCategory(
                    Guid.Parse("5cf52155-927f-4d64-a482-348f952bab21"),
                    nameof(AppointmentParticipation),
                    nameof(AppointmentParticipation.Prediction),
                    "Prediction Participant");
                foreach (SelectValueMapping mapping in SelectValueMappingSeedData.AppointmentParticipationPredictionMappings)
                {
                    category.SelectValueMappings.Add(mapping);
                }
                return category;
            }
        }

        public static SelectValueCategory AppointmentParticipationExpectation
        {
            get
            {
                var category = new SelectValueCategory(
                    Guid.Parse("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"),
                    nameof(AppointmentParticipation),
                    nameof(AppointmentParticipation.Expectation),
                    "Expectation KBB");
                foreach (SelectValueMapping mapping in SelectValueMappingSeedData.AppointmentParticipationExpectationMappings)
                {
                    category.SelectValueMappings.Add(mapping);
                }
                return category;
            }
        }

        public static SelectValueCategory ProjectGenre
        {
            get
            {
                var category = new SelectValueCategory(
                    Guid.Parse("4649b6b9-1362-41c2-ac5c-884f216dff30"),
                    nameof(Project),
                    nameof(Project.Genre),
                    "Genre");
                foreach (SelectValueMapping mapping in SelectValueMappingSeedData.ProjectGenreMappings)
                {
                    category.SelectValueMappings.Add(mapping);
                }
                return category;
            }
        }

        public static SelectValueCategory AppointmentCategory
        {
            get
            {
                var category = new SelectValueCategory(
                    Guid.Parse("d438c160-0588-41fa-93c3-cd33c0f97063"),
                    nameof(Appointment),
                    nameof(Appointment.Category),
                    "Category");
                foreach (SelectValueMapping mapping in SelectValueMappingSeedData.AppointmentCategoryMappings)
                {
                    category.SelectValueMappings.Add(mapping);
                }
                return category;
            }
        }

        public static SelectValueCategory AppointmentStatus
        {
            get
            {
                var category = new SelectValueCategory(
                    Guid.Parse("09be8eff-72e4-40a8-a1ed-717deb185a69"),
                    nameof(Appointment),
                    nameof(Appointment.Status),
                    "Status");
                foreach (SelectValueMapping mapping in SelectValueMappingSeedData.AppointmentStatusMappings)
                {
                    category.SelectValueMappings.Add(mapping);
                }
                return category;
            }
        }

        public static SelectValueCategory AppointmentEmolument
        {
            get
            {
                var category = new SelectValueCategory(
                    Guid.Parse("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"),
                    nameof(Appointment),
                    nameof(Appointment.Emolument),
                    "Emolument");
                foreach (SelectValueMapping mapping in SelectValueMappingSeedData.AppointmentEmolumentMappings)
                {
                    category.SelectValueMappings.Add(mapping);
                }
                return category;
            }
        }

        public static SelectValueCategory AppointmentEmolumentPattern
        {
            get
            {
                var category = new SelectValueCategory(
                    Guid.Parse("d438c160-0588-41fa-93c3-cd33c0f97063"),
                    nameof(Appointment),
                    nameof(Appointment.EmolumentPattern),
                    "Emolument Pattern");
                foreach (SelectValueMapping mapping in SelectValueMappingSeedData.AppointmentEmolumentPatternMappings)
                {
                    category.SelectValueMappings.Add(mapping);
                }
                return category;
            }
        }
    }
}
