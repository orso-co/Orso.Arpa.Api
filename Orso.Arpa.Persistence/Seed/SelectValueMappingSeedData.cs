using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.SelectValueCategories;

namespace Orso.Arpa.Persistence.Seed
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
                list.AddRange(AppointmentSalaryMappings);
                list.AddRange(AppointmentSalaryPatternMappings);
                list.AddRange(AppointmentExpectationMappings);
                list.AddRange(AppointmentParticipationPredictionMappings);
                list.AddRange(AppointmentParticipationResultMappings);
                list.AddRange(AppointmentStatusMappings);
                list.AddRange(ProjectGenreMappings);
                list.AddRange(ProjectTypeMappings);
                list.AddRange(ProjectStateMappings);
                list.AddRange(MusicianProfileQualificationMappings);
                list.AddRange(MusicianProfileSalaryMappings);
                list.AddRange(MusicianProfileInquiryStatusPerformerMappings);
                list.AddRange(MusicianProfileInquiryStatusStaffMappings);
                list.AddRange(AuditionStatusMappings);
                list.AddRange(AuditionRepetitorStatusMappings);
                list.AddRange(MusicianProfileAvailableDocumentsMappings);
                list.AddRange(MusicianProfileSectionInstrumentAvailabilityMappings);
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

        public static IList<SelectValueMapping> AppointmentSalaryMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("88da1c17-9efc-4f69-ba0f-39c76592845b"), SelectValueCategorySeedData.AppointmentSalary.Id, SelectValueSeedData.Yes.Id),
                    new SelectValueMapping(Guid.Parse("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"), SelectValueCategorySeedData.AppointmentSalary.Id, SelectValueSeedData.No.Id),
                    new SelectValueMapping(Guid.Parse("5b936e5f-3743-4cc3-91af-0cc8742c846e"), SelectValueCategorySeedData.AppointmentSalary.Id, SelectValueSeedData.Ambiguous.Id),
                    new SelectValueMapping(Guid.Parse("bbe90120-55f3-4474-a059-1334d0adaa57"), SelectValueCategorySeedData.AppointmentSalary.Id, SelectValueSeedData.SpecialCase.Id),
                };
            }
        }

        public static IList<SelectValueMapping> AppointmentSalaryPatternMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("8b51c75f-d597-48ef-8451-5f5fc32d57d1"), SelectValueCategorySeedData.AppointmentSalaryPattern.Id, SelectValueSeedData.Gloeckner2018.Id),
                    new SelectValueMapping(Guid.Parse("104fc525-bb0b-49dc-b2b2-9a8f63e45c92"), SelectValueCategorySeedData.AppointmentSalaryPattern.Id, SelectValueSeedData.OrchestraConcertLumpSum10h.Id),
                    new SelectValueMapping(Guid.Parse("f15b88b2-395d-4195-af25-8b8879640baf"), SelectValueCategorySeedData.AppointmentSalaryPattern.Id, SelectValueSeedData.OrchestraConcertLumpSum12h.Id),
                    new SelectValueMapping(Guid.Parse("74278b65-fd54-49d2-9cd2-061dd6318c7d"), SelectValueCategorySeedData.AppointmentSalaryPattern.Id, SelectValueSeedData.OrchestraConcertLumpSum1808.Id),
                    new SelectValueMapping(Guid.Parse("836c69d6-46f1-40d4-b75d-6aa86f9ec066"), SelectValueCategorySeedData.AppointmentSalaryPattern.Id, SelectValueSeedData.OrchestraRehearsalHourlyRate.Id),
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

        public static IList<SelectValueMapping> ProjectTypeMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("34f05f05-ef23-4f36-94e7-73b917530c51"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.Concert.Id),
                    new SelectValueMapping(Guid.Parse("7f76d426-cab7-4f4f-aba3-bd430bcec003"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.ConcertTour.Id),
                    new SelectValueMapping(Guid.Parse("ae2f10ff-39ae-427e-a5e8-ddcd89422d44"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.Workshop.Id),
                    new SelectValueMapping(Guid.Parse("44710a6b-93c0-4aac-b552-e0423f1b106a"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.Party.Id),
                    new SelectValueMapping(Guid.Parse("3f166c3c-c85e-404b-aad3-c8996f4fb75f"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.Rehearsal.Id),
                    new SelectValueMapping(Guid.Parse("d8f337d0-84fc-4a4d-b75c-fbe2208808ea"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.RehearsalWeekend.Id),
                    new SelectValueMapping(Guid.Parse("574e0c4b-cbb3-4750-926b-3df4c377fc5e"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.SpecialProject.Id),
                    new SelectValueMapping(Guid.Parse("679116ec-7840-4c6d-bb45-fa2d89d6e779"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.CDRecording.Id),
                    new SelectValueMapping(Guid.Parse("5c3f5e18-7afd-4404-98db-658e852901dc"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.Contest.Id),
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
                    new SelectValueMapping(Guid.Parse("5578f637-14b7-4c11-85a8-0b94d83da678"), SelectValueCategorySeedData.ProjectGenre.Id, SelectValueSeedData.FilmMusic.Id),
                    new SelectValueMapping(Guid.Parse("8daa5ae4-3885-4739-803a-693c7cfdf314"), SelectValueCategorySeedData.ProjectGenre.Id, SelectValueSeedData.DancePerformance.Id),
                    new SelectValueMapping(Guid.Parse("4ef47024-d8a5-4b2d-8584-aeb29263dddb"), SelectValueCategorySeedData.ProjectGenre.Id, SelectValueSeedData.ContemporaryMusic.Id),
                };
            }
        }

        public static IList<SelectValueMapping> ProjectStateMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("725a4f4a-37cb-46ba-93a3-7b9cc2b015cb"), SelectValueCategorySeedData.ProjectState.Id, SelectValueSeedData.Pending.Id),
                    new SelectValueMapping(Guid.Parse("b793fa86-2025-4258-8993-8045f4c312d7"), SelectValueCategorySeedData.ProjectState.Id, SelectValueSeedData.Confirmed.Id),
                    new SelectValueMapping(Guid.Parse("65975857-ab27-480d-87c3-dba74d45cb63"), SelectValueCategorySeedData.ProjectState.Id, SelectValueSeedData.Cancelled.Id),
                    new SelectValueMapping(Guid.Parse("bc29bf0a-2ebb-4db8-8765-a5f835492552"), SelectValueCategorySeedData.ProjectState.Id, SelectValueSeedData.Postponed.Id),
                    new SelectValueMapping(Guid.Parse("75f2d1c3-4ba2-4acc-8fd3-6b01ca66d1c9"), SelectValueCategorySeedData.ProjectState.Id, SelectValueSeedData.Archived.Id),
                };
            }
        }

        public static IList<SelectValueMapping> MusicianProfileQualificationMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("f036bca9-95d4-4526-b845-fff9208ab103"), SelectValueCategorySeedData.MusicianProfileQualification.Id, SelectValueSeedData.Amateur.Id),
                    new SelectValueMapping(Guid.Parse("6304b935-633d-4bba-a90f-9bd864c867c6"), SelectValueCategorySeedData.MusicianProfileQualification.Id, SelectValueSeedData.Student.Id),
                    new SelectValueMapping(Guid.Parse("20f9698c-2f3d-41fd-9f33-1feeababfb1c"), SelectValueCategorySeedData.MusicianProfileQualification.Id, SelectValueSeedData.SemiProfessional.Id),
                    new SelectValueMapping(Guid.Parse("30f592f6-485a-468a-bfb2-4854be733e74"), SelectValueCategorySeedData.MusicianProfileQualification.Id, SelectValueSeedData.Professional.Id),
                    new SelectValueMapping(Guid.Parse("42d76464-4b4b-4884-b8e3-1f69baaced4c"), SelectValueCategorySeedData.MusicianProfileQualification.Id, SelectValueSeedData.Unknown.Id),
                };
            }
        }

        public static IList<SelectValueMapping> MusicianProfileSalaryMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("58a0d16f-2dac-4836-930e-1dd320430ef4"), SelectValueCategorySeedData.MusicianProfileSalary.Id, SelectValueSeedData.Without.Id),
                    new SelectValueMapping(Guid.Parse("459dc1ea-de92-4a26-9b7b-848d67154cae"), SelectValueCategorySeedData.MusicianProfileSalary.Id, SelectValueSeedData.WithStrict.Id),
                    new SelectValueMapping(Guid.Parse("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"), SelectValueCategorySeedData.MusicianProfileSalary.Id, SelectValueSeedData.WithNegotiable.Id),
                    new SelectValueMapping(Guid.Parse("d80bf2be-de2f-4d72-ba02-6081b5ba77d2"), SelectValueCategorySeedData.MusicianProfileSalary.Id, SelectValueSeedData.Unknown.Id),
                };
            }
        }

        public static IList<SelectValueMapping> MusicianProfileInquiryStatusPerformerMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("68e947c0-9450-4b64-90d7-553850396a3f"), SelectValueCategorySeedData.MusicianProfileInquiryStatusPerformer.Id, SelectValueSeedData.Gladly.Id),
                    new SelectValueMapping(Guid.Parse("60c1a391-59b4-4cea-ba83-59e09f7512b6"), SelectValueCategorySeedData.MusicianProfileInquiryStatusPerformer.Id, SelectValueSeedData.EmergencyOnly.Id),
                    new SelectValueMapping(Guid.Parse("ab5c5904-2683-47c4-a436-319303b8e62f"), SelectValueCategorySeedData.MusicianProfileInquiryStatusPerformer.Id, SelectValueSeedData.NeverAgain.Id),
                    new SelectValueMapping(Guid.Parse("a15014ad-582e-4388-9b58-aceb52cf41bf"), SelectValueCategorySeedData.MusicianProfileInquiryStatusPerformer.Id, SelectValueSeedData.Unknown.Id),
                    new SelectValueMapping(Guid.Parse("90b5cfa9-890b-4b89-a750-646f3a26db23"), SelectValueCategorySeedData.MusicianProfileInquiryStatusPerformer.Id, SelectValueSeedData.ForContactsOnly.Id),
                };
            }
        }

        public static IList<SelectValueMapping> MusicianProfileInquiryStatusStaffMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("cdfb1c47-22dc-4657-aab8-1dbfaf21e862"), SelectValueCategorySeedData.MusicianProfileInquiryStatusStaff.Id, SelectValueSeedData.Gladly.Id),
                    new SelectValueMapping(Guid.Parse("9363bb46-937e-42bf-bb71-5fb16126b501"), SelectValueCategorySeedData.MusicianProfileInquiryStatusStaff.Id, SelectValueSeedData.EmergencyOnly.Id),
                    new SelectValueMapping(Guid.Parse("03a0cbc1-4546-4b54-b05d-ec37dafeec25"), SelectValueCategorySeedData.MusicianProfileInquiryStatusStaff.Id, SelectValueSeedData.NeverAgain.Id),
                    new SelectValueMapping(Guid.Parse("0fdbc388-feba-4607-9771-7751009f1fc8"), SelectValueCategorySeedData.MusicianProfileInquiryStatusStaff.Id, SelectValueSeedData.Unknown.Id),
                    new SelectValueMapping(Guid.Parse("354ef017-70ca-4c2b-914c-71be7289a0e5"), SelectValueCategorySeedData.MusicianProfileInquiryStatusStaff.Id, SelectValueSeedData.ForContactsOnly.Id),
                };
            }
        }

        public static IList<SelectValueMapping> AuditionStatusMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("be152c92-b807-4850-8327-9d1916dabead"), SelectValueCategorySeedData.AuditionStatus.Id, SelectValueSeedData.Passed.Id),
                    new SelectValueMapping(Guid.Parse("7b8defe6-9922-43d6-8df0-3a73f47d6980"), SelectValueCategorySeedData.AuditionStatus.Id, SelectValueSeedData.Failed.Id),
                    new SelectValueMapping(Guid.Parse("0e997440-53f2-4823-8581-4d4716525885"), SelectValueCategorySeedData.AuditionStatus.Id, SelectValueSeedData.Awaiting.Id),
                    new SelectValueMapping(Guid.Parse("fab42540-8c9d-4b18-9341-660f60dd7644"), SelectValueCategorySeedData.AuditionStatus.Id, SelectValueSeedData.Cancelled.Id),
                    new SelectValueMapping(Guid.Parse("3acd5be1-5fbc-4de4-a45c-2e230c413c85"), SelectValueCategorySeedData.AuditionStatus.Id, SelectValueSeedData.Unnecessary.Id),
                    new SelectValueMapping(Guid.Parse("24c5bbe1-37eb-4368-ac7c-a6061058bbef"), SelectValueCategorySeedData.AuditionStatus.Id, SelectValueSeedData.Unknown.Id),
                };
            }
        }

        public static IList<SelectValueMapping> AuditionRepetitorStatusMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("a88b874f-9879-482f-85ec-1ddda9bb545c"), SelectValueCategorySeedData.AuditionRepetitorStatus.Id, SelectValueSeedData.IsAskingForPianist.Id),
                    new SelectValueMapping(Guid.Parse("9808c1f6-0bbd-4054-acca-779d56a8a934"), SelectValueCategorySeedData.AuditionRepetitorStatus.Id, SelectValueSeedData.BringsPianist.Id),
                    new SelectValueMapping(Guid.Parse("0d1b888f-0f45-4f02-806b-480d5594bd27"), SelectValueCategorySeedData.AuditionRepetitorStatus.Id, SelectValueSeedData.NoPianistNeeded.Id),
                    new SelectValueMapping(Guid.Parse("98addc5f-f2fa-4640-8441-d4220b7daea2"), SelectValueCategorySeedData.AuditionRepetitorStatus.Id, SelectValueSeedData.Unknown.Id),
                };
            }
        }

        public static IList<SelectValueMapping> MusicianProfileAvailableDocumentsMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("f9cc5445-8a6e-480b-bffb-410089f55896"), SelectValueCategorySeedData.MusicianProfileAvailableDocuments.Id, SelectValueSeedData.CV.Id),
                    new SelectValueMapping(Guid.Parse("a3e5843b-05c3-452c-a29d-da8de738181a"), SelectValueCategorySeedData.MusicianProfileAvailableDocuments.Id, SelectValueSeedData.LetterOfRecommendation.Id),
                    new SelectValueMapping(Guid.Parse("1b53d96a-f9a1-4037-b103-f7aae9b278d7"), SelectValueCategorySeedData.MusicianProfileAvailableDocuments.Id, SelectValueSeedData.Diploma.Id),
                    new SelectValueMapping(Guid.Parse("edfad6f1-6584-4798-a09a-9f6146127d82"), SelectValueCategorySeedData.MusicianProfileAvailableDocuments.Id, SelectValueSeedData.Audio.Id),
                    new SelectValueMapping(Guid.Parse("f1626a63-6bf1-442a-86ad-8a86242bde94"), SelectValueCategorySeedData.MusicianProfileAvailableDocuments.Id, SelectValueSeedData.Video.Id),
                    new SelectValueMapping(Guid.Parse("887e7e2e-0c90-4c4c-9504-3f2a5af7fbcb"), SelectValueCategorySeedData.MusicianProfileAvailableDocuments.Id, SelectValueSeedData.Photo.Id),
                    new SelectValueMapping(Guid.Parse("4298e1f5-ea1d-4a83-9b32-e5dc3a7cbca9"), SelectValueCategorySeedData.MusicianProfileAvailableDocuments.Id, SelectValueSeedData.Other.Id),
                };
            }
        }

        public static IList<SelectValueMapping> MusicianProfileSectionInstrumentAvailabilityMappings
        {
            get
            {
                return new List<SelectValueMapping>
                {
                    new SelectValueMapping(Guid.Parse("d33ea034-0c5f-458d-bef5-26d2c12b6b03"), SelectValueCategorySeedData.MusicianProfileSectionInstrumentAvailability.Id, SelectValueSeedData.PrivateOwnership.Id),
                    new SelectValueMapping(Guid.Parse("c6b28eb5-e9d6-4250-bc79-6fa9bfbdbc5a"), SelectValueCategorySeedData.MusicianProfileSectionInstrumentAvailability.Id, SelectValueSeedData.NeedToBorrow.Id),
                    new SelectValueMapping(Guid.Parse("7869a9b0-fb13-4c00-ac7c-2fa1b27a00af"), SelectValueCategorySeedData.MusicianProfileSectionInstrumentAvailability.Id, SelectValueSeedData.ProvisionByStaff.Id),
                    new SelectValueMapping(Guid.Parse("0298c0d1-57e2-415a-9d6c-3f47e9ab6f22"), SelectValueCategorySeedData.MusicianProfileSectionInstrumentAvailability.Id, SelectValueSeedData.Unknown.Id),
                };
            }
        }
    }
}
