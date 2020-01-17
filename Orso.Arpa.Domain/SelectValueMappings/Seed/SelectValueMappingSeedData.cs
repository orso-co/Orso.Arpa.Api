using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.SelectValueCategories;
using Orso.Arpa.Domain.SelectValues.Seed;

namespace Orso.Arpa.Domain.SelectValueMappings.Seed
{
    public static class SelectValueMappingSeedData
    {
        public static IList<SelectValueMapping> SelectValueMappings
        {
            get
            {
                var list = new List<SelectValueMapping>();
                list.AddRange(AddressTypeMappings);
                list.AddRange(AppointmentCategoryMappings);
                list.AddRange(AppointmentEmolumentMappings);
                list.AddRange(AppointmentEmolumentPatternMappings);
                list.AddRange(AppointmentExpectationMappings);
                list.AddRange(AppointmentParticipationPredictionMappings);
                list.AddRange(AppointmentParticipationResultMappings);
                list.AddRange(AppointmentStatusMappings);
                list.AddRange(ProjectGenreMappings);
                return list;
            }
        }

        public static IList<SelectValueMapping> AddressTypeMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("fb44b625-7086-48e6-bcc6-a004dd472012"), SelectValueCategorySeedData.AddressType.Id, SelectValueSeedData.Private.Id),
                    new SelectValueMapping(Guid.Parse("63437ce4-b63b-4558-9f91-1474b896bf1c"), SelectValueCategorySeedData.AddressType.Id, SelectValueSeedData.Work.Id),
                    new SelectValueMapping(Guid.Parse("f81c698e-0017-41c0-8ff9-78dbaa3d2676"), SelectValueCategorySeedData.AddressType.Id, SelectValueSeedData.Other.Id)
                };
            }
        }

        public static IList<SelectValueMapping> AppointmentCategoryMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("c1b6d08b-f31e-4f38-a8c0-761e42fbd2b7"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Meeting.Id),
                    new SelectValueMapping(Guid.Parse("dd4556b3-d8b3-4002-8bde-68e327945916"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.StageBriefing.Id),
                    new SelectValueMapping(Guid.Parse("ac1ccdd4-39aa-4767-95ea-099a829f275b"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.ChoreographyRehearsal.Id),
                    new SelectValueMapping(Guid.Parse("d8c99a34-025d-455b-b317-92453da36631"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Show.Id),
                    new SelectValueMapping(Guid.Parse("e9c79ae9-5498-459d-8852-9f135da7afae"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.PhotoSession.Id),
                    new SelectValueMapping(Guid.Parse("0c8af1d2-ae39-464d-9e03-a1487cfd7321"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Concert.Id),
                    new SelectValueMapping(Guid.Parse("cfc62012-4d74-4cf6-a06e-7fc3dbacbff8"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Workshop.Id),
                    new SelectValueMapping(Guid.Parse("466aa422-0ef2-4e7f-a6a8-d188d80491f6"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Party.Id),
                    new SelectValueMapping(Guid.Parse("86672779-5e70-4965-b59c-032086d00914"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Rehearsal.Id),
                    new SelectValueMapping(Guid.Parse("5b89cf6e-0194-4e01-bb32-8b1813a51e16"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.RehearsalWeekendChoir.Id),
                    new SelectValueMapping(Guid.Parse("642cc60f-e582-47ed-a40f-ea490dd3d950"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.WatchShow.Id),
                    new SelectValueMapping(Guid.Parse("a39a92fe-bea2-40fa-817b-e7272bfc9d4b"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.SeeComment.Id),
                    new SelectValueMapping(Guid.Parse("b62cc155-f1a9-4904-8e6a-95e85339da83"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.VoiceFormation.Id),
                    new SelectValueMapping(Guid.Parse("2634c0cc-31d2-4f61-813d-7ec60fc8ab34"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.SectionalRehearsal.Id),
                    new SelectValueMapping(Guid.Parse("4e9d4a1b-cae7-4002-93a1-cef3f209146b"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Transfer.Id),
                    new SelectValueMapping(Guid.Parse("547b561e-cea7-4296-9b1d-4dd41e0d5179"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Assembly.Id),
                    new SelectValueMapping(Guid.Parse("9cf090a3-680d-4770-b929-0a0d080576a0"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Audition.Id),
                    new SelectValueMapping(Guid.Parse("609f9ece-42ce-4cc9-a89b-1fec1ddbc5fe"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Other.Id),
                };
            }
        }

        public static IList<SelectValueMapping> AppointmentEmolumentMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("88da1c17-9efc-4f69-ba0f-39c76592845b"), SelectValueCategorySeedData.AppointmentEmolument.Id, SelectValueSeedData.Yes.Id),
                    new SelectValueMapping(Guid.Parse("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"), SelectValueCategorySeedData.AppointmentEmolument.Id, SelectValueSeedData.No.Id),
                    new SelectValueMapping(Guid.Parse("5b936e5f-3743-4cc3-91af-0cc8742c846e"), SelectValueCategorySeedData.AppointmentEmolument.Id, SelectValueSeedData.Ambiguous.Id),
                    new SelectValueMapping(Guid.Parse("bbe90120-55f3-4474-a059-1334d0adaa57"), SelectValueCategorySeedData.AppointmentEmolument.Id, SelectValueSeedData.SpecialCase.Id),
                };
            }
        }

        public static IList<SelectValueMapping> AppointmentEmolumentPatternMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("8b51c75f-d597-48ef-8451-5f5fc32d57d1"), SelectValueCategorySeedData.AppointmentEmolumentPattern.Id, SelectValueSeedData.Gloeckner2018.Id),
                    new SelectValueMapping(Guid.Parse("104fc525-bb0b-49dc-b2b2-9a8f63e45c92"), SelectValueCategorySeedData.AppointmentEmolumentPattern.Id, SelectValueSeedData.OrchestraConcertLumpSum10h.Id),
                    new SelectValueMapping(Guid.Parse("f15b88b2-395d-4195-af25-8b8879640baf"), SelectValueCategorySeedData.AppointmentEmolumentPattern.Id, SelectValueSeedData.OrchestraConcertLumpSum12h.Id),
                    new SelectValueMapping(Guid.Parse("74278b65-fd54-49d2-9cd2-061dd6318c7d"), SelectValueCategorySeedData.AppointmentEmolumentPattern.Id, SelectValueSeedData.OrchestraConcertLumpSum1808.Id),
                    new SelectValueMapping(Guid.Parse("836c69d6-46f1-40d4-b75d-6aa86f9ec066"), SelectValueCategorySeedData.AppointmentEmolumentPattern.Id, SelectValueSeedData.OrchestraRehearsalHourlyRate.Id),
                };
            }
        }

        public static IList<SelectValueMapping> AppointmentExpectationMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("867622fa-7aa5-43f3-b3ef-5290d1f61734"), SelectValueCategorySeedData.AppointmentExpectation.Id, SelectValueSeedData.Pending.Id),
                    new SelectValueMapping(Guid.Parse("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"), SelectValueCategorySeedData.AppointmentExpectation.Id, SelectValueSeedData.Confirmed.Id),
                    new SelectValueMapping(Guid.Parse("b09bc4a6-06ab-4d45-8b82-7971e662ccb5"), SelectValueCategorySeedData.AppointmentExpectation.Id, SelectValueSeedData.Mandatory.Id),
                    new SelectValueMapping(Guid.Parse("d64abb04-dc1c-4e17-bed5-a62196a59c49"), SelectValueCategorySeedData.AppointmentExpectation.Id, SelectValueSeedData.Optional.Id),
                };
            }
        }

        public static IList<SelectValueMapping> AppointmentParticipationPredictionMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("319d508e-a6e2-437e-b48b-6be51e3459bd"), SelectValueCategorySeedData.AppointmentParticipationPrediction.Id, SelectValueSeedData.Yes.Id),
                    new SelectValueMapping(Guid.Parse("c9225a82-0348-41bb-a591-7726f8079cde"), SelectValueCategorySeedData.AppointmentParticipationPrediction.Id, SelectValueSeedData.Partly.Id),
                    new SelectValueMapping(Guid.Parse("17d201fc-777b-43bb-9c46-0d07737a8ab7"), SelectValueCategorySeedData.AppointmentParticipationPrediction.Id, SelectValueSeedData.No.Id),
                    new SelectValueMapping(Guid.Parse("50e6049b-a9cd-400b-a475-e2563147aebc"), SelectValueCategorySeedData.AppointmentParticipationPrediction.Id, SelectValueSeedData.DontKnowYet.Id),
                };
            }
        }

        public static IList<SelectValueMapping> AppointmentParticipationResultMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("3801aa69-cc4e-4fd5-947c-bfdd4e95d48e"), SelectValueCategorySeedData.AppointmentParticipationResult.Id, SelectValueSeedData.Present.Id),
                    new SelectValueMapping(Guid.Parse("ade78d45-b010-4ed7-87f0-e30e0558f151"), SelectValueCategorySeedData.AppointmentParticipationResult.Id, SelectValueSeedData.Absent.Id),
                    new SelectValueMapping(Guid.Parse("ff994b2c-a3bd-4676-a974-f53d4fa562ba"), SelectValueCategorySeedData.AppointmentParticipationResult.Id, SelectValueSeedData.Inapplicable.Id),
                    new SelectValueMapping(Guid.Parse("8b7d7f26-b7e5-42e2-afc1-cedddbae841a"), SelectValueCategorySeedData.AppointmentParticipationResult.Id, SelectValueSeedData.Ambiguous.Id),
                    new SelectValueMapping(Guid.Parse("7fb30d45-1faf-4f6a-ac5d-436204ad8e69"), SelectValueCategorySeedData.AppointmentParticipationResult.Id, SelectValueSeedData.AwaitingScan.Id),
                };
            }
        }

        public static IList<SelectValueMapping> AppointmentStatusMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("36176b7e-0926-43d6-b19a-72838ccd2acd"), SelectValueCategorySeedData.AppointmentStatus.Id, SelectValueSeedData.Confirmed.Id),
                    new SelectValueMapping(Guid.Parse("93033f7e-a3c1-45e3-8a17-021d0a4abe5a"), SelectValueCategorySeedData.AppointmentStatus.Id, SelectValueSeedData.Scheduled.Id),
                    new SelectValueMapping(Guid.Parse("0126fded-0a82-4b53-85e4-1c04a4f79296"), SelectValueCategorySeedData.AppointmentStatus.Id, SelectValueSeedData.Refused.Id),
                    new SelectValueMapping(Guid.Parse("b6cf76a5-ec3f-4e81-9499-174d33bb7249"), SelectValueCategorySeedData.AppointmentStatus.Id, SelectValueSeedData.Ambiguous.Id),
                    new SelectValueMapping(Guid.Parse("4dc9db05-357a-43a6-ba20-f2a9e5033802"), SelectValueCategorySeedData.AppointmentStatus.Id, SelectValueSeedData.AwaitingPoll.Id),
                };
            }
        }

        public static IList<SelectValueMapping> ProjectGenreMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("d733e38d-1d80-4054-b654-4ea4a128b0a8"), SelectValueCategorySeedData.ProjectGenre.Id, SelectValueSeedData.ClassicalMusic.Id),
                    new SelectValueMapping(Guid.Parse("e7e78e76-3850-4eb5-892f-d90be6c256a4"), SelectValueCategorySeedData.ProjectGenre.Id, SelectValueSeedData.Crossover.Id),
                    new SelectValueMapping(Guid.Parse("29e1142f-aa9e-4b94-ae21-9a63f7b65c15"), SelectValueCategorySeedData.ProjectGenre.Id, SelectValueSeedData.ChamberMusic.Id),
                };
            }
        }
    }
}
