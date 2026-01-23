using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Util;
using Orso.Arpa.Domain.VenueDomain.Model;

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
                list.AddRange(ProjectGenreMappings);
                list.AddRange(ProjectTypeMappings);
                list.AddRange(MusicianProfileQualificationMappings);
                list.AddRange(MusicianProfileSalaryMappings);
                list.AddRange(AuditionStatusMappings);
                list.AddRange(AuditionRepetitorStatusMappings);
                list.AddRange(MusicianProfileDocumentsMappings);
                list.AddRange(MusicianProfileSectionInstrumentAvailabilityMappings);
                list.AddRange(EducationTypeMappings);
                list.AddRange(CurriculumVitaeReferenceTypeMappings);
                list.AddRange(PersonGenderMappings);
                list.AddRange(ContactDetailTypeMappings);
                list.AddRange(BankAccountStatusMappings);
                list.AddRange(RoomEquipmentTypeMappings);
                list.AddRange(RoomCapacityMappings);
                list.AddRange(PersonMembershipSupportLevelMappings);
                list.AddRange(PersonMembershipMembershipStatusMappings);
                list.AddRange(PersonMembershipPaymentMethodMappings);
                list.AddRange(PersonMembershipPaymentFrequencyMappings);
                list.AddRange(PersonMembershipClubMappings);
                return list;
            }
        }

        public static IList<SelectValueMapping> AddressTypeMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("fb44b625-7086-48e6-bcc6-a004dd472012"), SelectValueCategorySeedData.AddressType.Id, SelectValueSeedData.Private.Id, 10),
                    new(Guid.Parse("63437ce4-b63b-4558-9f91-1474b896bf1c"), SelectValueCategorySeedData.AddressType.Id, SelectValueSeedData.Business.Id, 20),
                    new(Guid.Parse("f81c698e-0017-41c0-8ff9-78dbaa3d2676"), SelectValueCategorySeedData.AddressType.Id, SelectValueSeedData.Other.Id, 30),
                    new(Guid.Parse("0cc663ed-67fa-4a34-908c-3120ba9fe8c1"), SelectValueCategorySeedData.AddressType.Id, SelectValueSeedData.Unknown.Id, 40),
                ];
            }
        }

        public static IList<SelectValueMapping> AppointmentCategoryMappings
        {
            get
            {
                return
                [
                   new(Guid.Parse("86672779-5e70-4965-b59c-032086d00914"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Rehearsal.Id, 10),
                    new(Guid.Parse("2634c0cc-31d2-4f61-813d-7ec60fc8ab34"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.SectionalRehearsal.Id, 15),
                    new(Guid.Parse("5b89cf6e-0194-4e01-bb32-8b1813a51e16"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.RehearsalWeekendChoir.Id, 20),
                    new(Guid.Parse("ac1ccdd4-39aa-4767-95ea-099a829f275b"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.ChoreographyRehearsal.Id, 25),
                    new(Guid.Parse("694de886-8566-45d0-afc7-6ded18a2b6e6"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.WarmUpRehearsal.Id, 30),
                    new(Guid.Parse("e1b4cb6e-937e-409a-a86d-7a66c39242dd"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Soundcheck.Id, 35),

                    new(Guid.Parse("0c8af1d2-ae39-464d-9e03-a1487cfd7321"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Concert.Id, 50),
                    new(Guid.Parse("466aa422-0ef2-4e7f-a6a8-d188d80491f6"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Party.Id, 55),

                    new(Guid.Parse("b62cc155-f1a9-4904-8e6a-95e85339da83"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.VocalCoaching.Id, 60),
                    new(Guid.Parse("9cf090a3-680d-4770-b929-0a0d080576a0"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Audition.Id, 65),

                    new(Guid.Parse("4e9d4a1b-cae7-4002-93a1-cef3f209146b"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Transfer.Id,68),

                    new(Guid.Parse("c1b6d08b-f31e-4f38-a8c0-761e42fbd2b7"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Meeting.Id, 70),
                    new(Guid.Parse("547b561e-cea7-4296-9b1d-4dd41e0d5179"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Assembly.Id, 75),
                    new(Guid.Parse("dd4556b3-d8b3-4002-8bde-68e327945916"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.StageBriefing.Id, 78),

                    new(Guid.Parse("a39a92fe-bea2-40fa-817b-e7272bfc9d4b"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.SeeComment.Id, 80),

                    new(Guid.Parse("cfc62012-4d74-4cf6-a06e-7fc3dbacbff8"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Workshop.Id, 85),
                    new(Guid.Parse("e9c79ae9-5498-459d-8852-9f135da7afae"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.PhotoSession.Id, 87),

                    new(Guid.Parse("d8c99a34-025d-455b-b317-92453da36631"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Show.Id, 89),
                    new(Guid.Parse("642cc60f-e582-47ed-a40f-ea490dd3d950"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.WatchShow.Id, 90),

                    new(Guid.Parse("609f9ece-42ce-4cc9-a89b-1fec1ddbc5fe"), SelectValueCategorySeedData.AppointmentCategory.Id, SelectValueSeedData.Other.Id, 95),
                ];
            }
        }

        public static IList<SelectValueMapping> AppointmentSalaryMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("88da1c17-9efc-4f69-ba0f-39c76592845b"), SelectValueCategorySeedData.AppointmentSalary.Id, SelectValueSeedData.Yes.Id, 10),
                    new(Guid.Parse("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"), SelectValueCategorySeedData.AppointmentSalary.Id, SelectValueSeedData.No.Id, 20),
                    new(Guid.Parse("5b936e5f-3743-4cc3-91af-0cc8742c846e"), SelectValueCategorySeedData.AppointmentSalary.Id, SelectValueSeedData.Ambiguous.Id, 30),
                    new(Guid.Parse("bbe90120-55f3-4474-a059-1334d0adaa57"), SelectValueCategorySeedData.AppointmentSalary.Id, SelectValueSeedData.SpecialCase.Id, 40),
                ];
            }
        }

        public static IList<SelectValueMapping> AppointmentSalaryPatternMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("8b51c75f-d597-48ef-8451-5f5fc32d57d1"), SelectValueCategorySeedData.AppointmentSalaryPattern.Id, SelectValueSeedData.Gloeckner2018.Id, 50),
                    new(Guid.Parse("104fc525-bb0b-49dc-b2b2-9a8f63e45c92"), SelectValueCategorySeedData.AppointmentSalaryPattern.Id, SelectValueSeedData.OrchestraConcertLumpSum10h.Id, 20),
                    new(Guid.Parse("f15b88b2-395d-4195-af25-8b8879640baf"), SelectValueCategorySeedData.AppointmentSalaryPattern.Id, SelectValueSeedData.OrchestraConcertLumpSum12h.Id, 10),
                    new(Guid.Parse("74278b65-fd54-49d2-9cd2-061dd6318c7d"), SelectValueCategorySeedData.AppointmentSalaryPattern.Id, SelectValueSeedData.OrchestraConcertLumpSum1808.Id, 40),
                    new(Guid.Parse("836c69d6-46f1-40d4-b75d-6aa86f9ec066"), SelectValueCategorySeedData.AppointmentSalaryPattern.Id, SelectValueSeedData.OrchestraRehearsalHourlyRate.Id, 30),
                ];
            }
        }

        public static IList<SelectValueMapping> AppointmentExpectationMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("867622fa-7aa5-43f3-b3ef-5290d1f61734"), SelectValueCategorySeedData.AppointmentExpectation.Id, SelectValueSeedData.Pending.Id, 10),
                    new(Guid.Parse("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"), SelectValueCategorySeedData.AppointmentExpectation.Id, SelectValueSeedData.Confirmed.Id, 20),
                    new(Guid.Parse("b09bc4a6-06ab-4d45-8b82-7971e662ccb5"), SelectValueCategorySeedData.AppointmentExpectation.Id, SelectValueSeedData.Mandatory.Id, 30),
                    new(Guid.Parse("d64abb04-dc1c-4e17-bed5-a62196a59c49"), SelectValueCategorySeedData.AppointmentExpectation.Id, SelectValueSeedData.Optional.Id, 40),
                ];
            }
        }

        public static IList<SelectValueMapping> ProjectTypeMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("34f05f05-ef23-4f36-94e7-73b917530c51"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.Concert.Id, 10),
                    new(Guid.Parse("7f76d426-cab7-4f4f-aba3-bd430bcec003"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.ConcertTour.Id, 20),
                    new(Guid.Parse("ae2f10ff-39ae-427e-a5e8-ddcd89422d44"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.Workshop.Id, 30),
                    new(Guid.Parse("44710a6b-93c0-4aac-b552-e0423f1b106a"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.Party.Id, 80),
                    new(Guid.Parse("3f166c3c-c85e-404b-aad3-c8996f4fb75f"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.Rehearsal.Id, 50),
                    new(Guid.Parse("d8f337d0-84fc-4a4d-b75c-fbe2208808ea"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.RehearsalWeekend.Id, 55),
                    new(Guid.Parse("574e0c4b-cbb3-4750-926b-3df4c377fc5e"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.SpecialProject.Id, 85),
                    new(Guid.Parse("679116ec-7840-4c6d-bb45-fa2d89d6e779"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.CDRecording.Id, 70),
                    new(Guid.Parse("5c3f5e18-7afd-4404-98db-658e852901dc"), SelectValueCategorySeedData.ProjectType.Id, SelectValueSeedData.Contest.Id, 60),
                ];
            }
        }

        public static IList<SelectValueMapping> ProjectGenreMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("d733e38d-1d80-4054-b654-4ea4a128b0a8"), SelectValueCategorySeedData.ProjectGenre.Id, SelectValueSeedData.ClassicalMusic.Id,10),
                    new(Guid.Parse("e7e78e76-3850-4eb5-892f-d90be6c256a4"), SelectValueCategorySeedData.ProjectGenre.Id, SelectValueSeedData.Crossover.Id, 20),
                    new(Guid.Parse("29e1142f-aa9e-4b94-ae21-9a63f7b65c15"), SelectValueCategorySeedData.ProjectGenre.Id, SelectValueSeedData.ChamberMusic.Id,30),
                    new(Guid.Parse("5578f637-14b7-4c11-85a8-0b94d83da678"), SelectValueCategorySeedData.ProjectGenre.Id, SelectValueSeedData.FilmMusic.Id,40),
                    new(Guid.Parse("8daa5ae4-3885-4739-803a-693c7cfdf314"), SelectValueCategorySeedData.ProjectGenre.Id, SelectValueSeedData.DancePerformance.Id,50),
                    new(Guid.Parse("4ef47024-d8a5-4b2d-8584-aeb29263dddb"), SelectValueCategorySeedData.ProjectGenre.Id, SelectValueSeedData.ContemporaryMusic.Id, 60),
                ];
            }
        }

        public static IList<SelectValueMapping> MusicianProfileQualificationMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("f036bca9-95d4-4526-b845-fff9208ab103"), SelectValueCategorySeedData.MusicianProfileQualification.Id, SelectValueSeedData.Amateur.Id, 10),
                    new(Guid.Parse("6304b935-633d-4bba-a90f-9bd864c867c6"), SelectValueCategorySeedData.MusicianProfileQualification.Id, SelectValueSeedData.Student.Id, 20),
                    new(Guid.Parse("20f9698c-2f3d-41fd-9f33-1feeababfb1c"), SelectValueCategorySeedData.MusicianProfileQualification.Id, SelectValueSeedData.SemiProfessional.Id, 30),
                    new(Guid.Parse("30f592f6-485a-468a-bfb2-4854be733e74"), SelectValueCategorySeedData.MusicianProfileQualification.Id, SelectValueSeedData.Professional.Id, 40),
                    new(Guid.Parse("42d76464-4b4b-4884-b8e3-1f69baaced4c"), SelectValueCategorySeedData.MusicianProfileQualification.Id, SelectValueSeedData.Unknown.Id, 50),
                ];
            }
        }

        public static IList<SelectValueMapping> MusicianProfileSalaryMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("58a0d16f-2dac-4836-930e-1dd320430ef4"), SelectValueCategorySeedData.MusicianProfileSalary.Id, SelectValueSeedData.Without.Id, 10),
                    new(Guid.Parse("459dc1ea-de92-4a26-9b7b-848d67154cae"), SelectValueCategorySeedData.MusicianProfileSalary.Id, SelectValueSeedData.WithStrict.Id, 20),
                    new(Guid.Parse("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"), SelectValueCategorySeedData.MusicianProfileSalary.Id, SelectValueSeedData.WithNegotiable.Id, 30),
                    new(Guid.Parse("d80bf2be-de2f-4d72-ba02-6081b5ba77d2"), SelectValueCategorySeedData.MusicianProfileSalary.Id, SelectValueSeedData.Unknown.Id, 40),
                ];
            }
        }

        public static IList<SelectValueMapping> AuditionStatusMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("be152c92-b807-4850-8327-9d1916dabead"), SelectValueCategorySeedData.AuditionStatus.Id, SelectValueSeedData.Passed.Id, 20),
                    new(Guid.Parse("7b8defe6-9922-43d6-8df0-3a73f47d6980"), SelectValueCategorySeedData.AuditionStatus.Id, SelectValueSeedData.Failed.Id, 30),
                    new(Guid.Parse("0e997440-53f2-4823-8581-4d4716525885"), SelectValueCategorySeedData.AuditionStatus.Id, SelectValueSeedData.Awaiting.Id, 10),
                    new(Guid.Parse("fab42540-8c9d-4b18-9341-660f60dd7644"), SelectValueCategorySeedData.AuditionStatus.Id, SelectValueSeedData.Cancelled.Id, 40),
                    new(Guid.Parse("3acd5be1-5fbc-4de4-a45c-2e230c413c85"), SelectValueCategorySeedData.AuditionStatus.Id, SelectValueSeedData.Unnecessary.Id, 50),
                    new(Guid.Parse("24c5bbe1-37eb-4368-ac7c-a6061058bbef"), SelectValueCategorySeedData.AuditionStatus.Id, SelectValueSeedData.Unknown.Id, 60),
                ];
            }
        }

        public static IList<SelectValueMapping> AuditionRepetitorStatusMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("a88b874f-9879-482f-85ec-1ddda9bb545c"), SelectValueCategorySeedData.AuditionRepetitorStatus.Id, SelectValueSeedData.IsAskingForPianist.Id, 10),
                    new(Guid.Parse("9808c1f6-0bbd-4054-acca-779d56a8a934"), SelectValueCategorySeedData.AuditionRepetitorStatus.Id, SelectValueSeedData.BringsPianist.Id, 20),
                    new(Guid.Parse("0d1b888f-0f45-4f02-806b-480d5594bd27"), SelectValueCategorySeedData.AuditionRepetitorStatus.Id, SelectValueSeedData.NoPianistNeeded.Id, 30),
                    new(Guid.Parse("98addc5f-f2fa-4640-8441-d4220b7daea2"), SelectValueCategorySeedData.AuditionRepetitorStatus.Id, SelectValueSeedData.Unknown.Id, 40),
                ];
            }
        }

        public static IList<SelectValueMapping> MusicianProfileDocumentsMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("f9cc5445-8a6e-480b-bffb-410089f55896"), SelectValueCategorySeedData.MusicianProfileDocuments.Id, SelectValueSeedData.CV.Id, 10),
                    new(Guid.Parse("a3e5843b-05c3-452c-a29d-da8de738181a"), SelectValueCategorySeedData.MusicianProfileDocuments.Id, SelectValueSeedData.LetterOfRecommendation.Id, 60),
                    new(Guid.Parse("1b53d96a-f9a1-4037-b103-f7aae9b278d7"), SelectValueCategorySeedData.MusicianProfileDocuments.Id, SelectValueSeedData.Diploma.Id, 20),
                    new(Guid.Parse("edfad6f1-6584-4798-a09a-9f6146127d82"), SelectValueCategorySeedData.MusicianProfileDocuments.Id, SelectValueSeedData.Audio.Id, 50),
                    new(Guid.Parse("f1626a63-6bf1-442a-86ad-8a86242bde94"), SelectValueCategorySeedData.MusicianProfileDocuments.Id, SelectValueSeedData.Video.Id, 40),
                    new(Guid.Parse("887e7e2e-0c90-4c4c-9504-3f2a5af7fbcb"), SelectValueCategorySeedData.MusicianProfileDocuments.Id, SelectValueSeedData.Photo.Id, 30),
                    new(Guid.Parse("4298e1f5-ea1d-4a83-9b32-e5dc3a7cbca9"), SelectValueCategorySeedData.MusicianProfileDocuments.Id, SelectValueSeedData.Other.Id, 70),
                ];
            }
        }

        public static IList<SelectValueMapping> MusicianProfileSectionInstrumentAvailabilityMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("d33ea034-0c5f-458d-bef5-26d2c12b6b03"), SelectValueCategorySeedData.MusicianProfileSectionInstrumentAvailability.Id, SelectValueSeedData.PrivateOwnership.Id, 10),
                    new(Guid.Parse("c6b28eb5-e9d6-4250-bc79-6fa9bfbdbc5a"), SelectValueCategorySeedData.MusicianProfileSectionInstrumentAvailability.Id, SelectValueSeedData.NeedToBorrow.Id, 20),
                    new(Guid.Parse("7869a9b0-fb13-4c00-ac7c-2fa1b27a00af"), SelectValueCategorySeedData.MusicianProfileSectionInstrumentAvailability.Id, SelectValueSeedData.ProvisionByStaff.Id, 30),
                    new(Guid.Parse("0298c0d1-57e2-415a-9d6c-3f47e9ab6f22"), SelectValueCategorySeedData.MusicianProfileSectionInstrumentAvailability.Id, SelectValueSeedData.Unknown.Id, 40),
                ];
            }
        }

        public static IList<SelectValueMapping> EducationTypeMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("99251f16-deca-437e-84e2-a747e1a8ad7f"), SelectValueCategorySeedData.EducationType.Id, SelectValueSeedData.PrivateLesson.Id),
                    new(Guid.Parse("d259e4bc-9302-4b42-9b0c-2087fc1680e7"), SelectValueCategorySeedData.EducationType.Id, SelectValueSeedData.MusicSchool.Id),
                    new(Guid.Parse("5f071c88-813b-47c2-85a3-1d89321b7302"), SelectValueCategorySeedData.EducationType.Id, SelectValueSeedData.University.Id),
                    new(Guid.Parse("574e8627-14fa-4a76-b05a-b80305994f98"), SelectValueCategorySeedData.EducationType.Id, SelectValueSeedData.Conservatory.Id),
                    new(Guid.Parse("025a7a5c-3c61-4527-8ae0-769ad546bf1a"), SelectValueCategorySeedData.EducationType.Id, SelectValueSeedData.MasterClass.Id),
                    new(Guid.Parse("149d5e63-a800-423a-b893-f1b763989d04"), SelectValueCategorySeedData.EducationType.Id, SelectValueSeedData.Other.Id),
                ];
            }
        }
        public static IList<SelectValueMapping> CurriculumVitaeReferenceTypeMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("dead0ae2-bb2b-4584-992e-dddeb7f23d53"), SelectValueCategorySeedData.CurriculumVitaeReferenceType.Id, SelectValueSeedData.EnsemblePosition.Id),
                    new(Guid.Parse("3245182e-7985-4c07-828e-d69287ff2a2d"), SelectValueCategorySeedData.CurriculumVitaeReferenceType.Id, SelectValueSeedData.SoloPerformance.Id),
                    new(Guid.Parse("d30083ca-235f-43fa-9cba-3acdacf52b93"), SelectValueCategorySeedData.CurriculumVitaeReferenceType.Id, SelectValueSeedData.CompetitionPrize.Id),
                    new(Guid.Parse("28d79b43-18be-48b2-a6c9-776ddea0bdb2"), SelectValueCategorySeedData.CurriculumVitaeReferenceType.Id, SelectValueSeedData.Recommendation.Id),
                    new(Guid.Parse("8822614e-3e7c-4224-bb9c-468cec939bbc"), SelectValueCategorySeedData.CurriculumVitaeReferenceType.Id, SelectValueSeedData.Other.Id),
                ];
            }
        }

        public static IList<SelectValueMapping> PersonGenderMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("32761c45-e481-4eb9-a23e-d73330482572"), SelectValueCategorySeedData.PersonGender.Id, SelectValueSeedData.Female.Id),
                    new(Guid.Parse("1c16a5fe-6ac6-4e94-be6e-82a0a0fbe1c9"), SelectValueCategorySeedData.PersonGender.Id, SelectValueSeedData.Male.Id),
                    new(Guid.Parse("88d680fe-b6cc-486f-8f79-2525189b8b13"), SelectValueCategorySeedData.PersonGender.Id, SelectValueSeedData.Diverse.Id),
                ];
            }
        }

        public static IList<SelectValueMapping> ContactDetailTypeMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("f0bf8326-623e-4caa-bd92-bc05c721a6cf"), SelectValueCategorySeedData.ContactDetailType.Id, SelectValueSeedData.Private.Id, 10),
                    new(Guid.Parse("8205e3e6-8f58-49de-a438-02fce2aa0548"), SelectValueCategorySeedData.ContactDetailType.Id, SelectValueSeedData.Business.Id, 20),
                    new(Guid.Parse("0432acc1-9332-4885-af64-52e37f7637a9"), SelectValueCategorySeedData.ContactDetailType.Id, SelectValueSeedData.Other.Id, 30),
                    new(Guid.Parse("bfb1c88f-1fba-4f83-b17a-479399f53f6d"), SelectValueCategorySeedData.ContactDetailType.Id, SelectValueSeedData.Unknown.Id, 40),
                ];
            }
        }

        public static IList<SelectValueMapping> BankAccountStatusMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("a24f4ce6-b3c6-4d58-9e31-cb3a83ae2694"), SelectValueCategorySeedData.BankAccountStatus.Id, SelectValueSeedData.BankAccountExpired.Id, 10),
                    new(Guid.Parse("2f03daef-5795-45b6-9535-cf7748f84476"), SelectValueCategorySeedData.BankAccountStatus.Id, SelectValueSeedData.ReturnDebitReceived.Id, 20),
                    new(Guid.Parse("77164303-d91d-4fa1-9c2c-ae9c05298e30"), SelectValueCategorySeedData.BankAccountStatus.Id, SelectValueSeedData.IncorrectBankDetails.Id, 30),
                    new(Guid.Parse("c59900fa-7dc6-4ca7-8a35-c73c7ea582b9"), SelectValueCategorySeedData.BankAccountStatus.Id, SelectValueSeedData.OtherSeeCommentField.Id, 40),
                ];
            }
        }

        public static IList<SelectValueMapping> RoomEquipmentTypeMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("eec36685-52a5-42a4-969d-aa7946f3b14b"), SelectValueCategorySeedData.RoomEquipmentType.Id, SelectValueSeedData.Wlan.Id, 10),
                    new(Guid.Parse("2bf96d06-b639-45af-82f2-409d020e7fbb"), SelectValueCategorySeedData.RoomEquipmentType.Id, SelectValueSeedData.Kitchen.Id, 20),
                    new(Guid.Parse("e9a135f7-9894-42d0-9175-df1f0b53172b"), SelectValueCategorySeedData.RoomEquipmentType.Id, SelectValueSeedData.Tableware.Id, 30),
                    new(Guid.Parse("5d13ce58-e896-4443-9a63-5068ef6289b2"), SelectValueCategorySeedData.RoomEquipmentType.Id, SelectValueSeedData.Flipchart.Id, 40),
                    new(Guid.Parse("5f1ce14f-7d85-465d-8cfb-dd551d09d4a5"), SelectValueCategorySeedData.RoomEquipmentType.Id, SelectValueSeedData.Beamer.Id, 50),
                    new(Guid.Parse("006691f9-2948-4aa4-8d92-0f05643220e0"), SelectValueCategorySeedData.RoomEquipmentType.Id, SelectValueSeedData.Tables.Id, 60),
                    new(Guid.Parse("efe41455-e9d5-4bcc-94b8-086a5926df96"), SelectValueCategorySeedData.RoomEquipmentType.Id, SelectValueSeedData.Chairs.Id, 70),
                    new(Guid.Parse("d658dd90-c17d-4005-b6bd-6ede3e9166d6"), SelectValueCategorySeedData.RoomEquipmentType.Id, SelectValueSeedData.Platforms.Id, 80),
                    new(Guid.Parse("5e49bb48-fadd-48d9-bac5-dd567002b978"), SelectValueCategorySeedData.RoomEquipmentType.Id, SelectValueSeedData.Stage.Id, 90),
                    new(Guid.Parse("a348e3e6-d110-42b4-9865-1d8e96a746bc"), SelectValueCategorySeedData.RoomEquipmentType.Id, SelectValueSeedData.MusicStands.Id, 100),
                    new(Guid.Parse("6b82e9fa-cb38-410d-a35d-1ed84cae902f"), SelectValueCategorySeedData.RoomEquipmentType.Id, SelectValueSeedData.DrinkinFountain.Id, 110),
                ];
            }
        }

         public static IList<SelectValueMapping> RoomCapacityMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("9814654f-b7af-42f9-a77c-434899714652"), SelectValueCategorySeedData.RoomCapacity.Id, SelectValueSeedData.VoiceRehearsal.Id, 10),
                    new(Guid.Parse("cb08b618-a2f0-4c5b-872c-6b6821453429"), SelectValueCategorySeedData.RoomCapacity.Id, SelectValueSeedData.Choir.Id, 20),
                    new(Guid.Parse("47faa760-b0a2-4900-9505-1b61d4ef99f2"), SelectValueCategorySeedData.RoomCapacity.Id, SelectValueSeedData.Orchestra.Id, 30),
                    new(Guid.Parse("4ed969a3-ba48-4116-b934-6ff1bb6719ac"), SelectValueCategorySeedData.RoomCapacity.Id, SelectValueSeedData.Tutti.Id, 40),
                ];
            }
        }

        // PersonMembership Mappings

        public static IList<SelectValueMapping> PersonMembershipSupportLevelMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("11111111-1111-4111-8111-111111111111"), SelectValueCategorySeedData.PersonMembershipSupportLevel.Id, SelectValueSeedData.Sonata.Id, 10),
                    new(Guid.Parse("22222222-2222-4222-8222-222222222222"), SelectValueCategorySeedData.PersonMembershipSupportLevel.Id, SelectValueSeedData.Concerto.Id, 20),
                    new(Guid.Parse("33333333-3333-4333-8333-333333333333"), SelectValueCategorySeedData.PersonMembershipSupportLevel.Id, SelectValueSeedData.Symphony.Id, 30),
                    new(Guid.Parse("44444444-4444-4444-8444-444444444444"), SelectValueCategorySeedData.PersonMembershipSupportLevel.Id, SelectValueSeedData.Opera.Id, 40),
                ];
            }
        }

        public static IList<SelectValueMapping> PersonMembershipMembershipStatusMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("55555555-5555-4555-8555-555555555555"), SelectValueCategorySeedData.PersonMembershipMembershipStatus.Id, SelectValueSeedData.FullMember.Id, 10),
                    new(Guid.Parse("66666666-6666-4666-8666-666666666666"), SelectValueCategorySeedData.PersonMembershipMembershipStatus.Id, SelectValueSeedData.ProjectMember.Id, 20),
                    new(Guid.Parse("77777777-7777-4777-8777-777777777777"), SelectValueCategorySeedData.PersonMembershipMembershipStatus.Id, SelectValueSeedData.SupportingMember.Id, 30),
                ];
            }
        }

        public static IList<SelectValueMapping> PersonMembershipPaymentMethodMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("88888888-8888-4888-8888-888888888888"), SelectValueCategorySeedData.PersonMembershipPaymentMethod.Id, SelectValueSeedData.DirectDebit.Id, 10),
                    new(Guid.Parse("99999999-9999-4999-8999-999999999999"), SelectValueCategorySeedData.PersonMembershipPaymentMethod.Id, SelectValueSeedData.BankTransfer.Id, 20),
                    new(Guid.Parse("aaaaaaaa-aaaa-4aaa-8aaa-aaaaaaaaaaaa"), SelectValueCategorySeedData.PersonMembershipPaymentMethod.Id, SelectValueSeedData.Cash.Id, 30),
                    new(Guid.Parse("bbbbbbbb-bbbb-4bbb-8bbb-bbbbbbbbbbbb"), SelectValueCategorySeedData.PersonMembershipPaymentMethod.Id, SelectValueSeedData.PayPal.Id, 40),
                ];
            }
        }

        public static IList<SelectValueMapping> PersonMembershipPaymentFrequencyMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("cccccccc-cccc-4ccc-8ccc-cccccccccccc"), SelectValueCategorySeedData.PersonMembershipPaymentFrequency.Id, SelectValueSeedData.Monthly.Id, 10),
                    new(Guid.Parse("dddddddd-dddd-4ddd-8ddd-dddddddddddd"), SelectValueCategorySeedData.PersonMembershipPaymentFrequency.Id, SelectValueSeedData.Quarterly.Id, 20),
                    new(Guid.Parse("eeeeeeee-eeee-4eee-8eee-eeeeeeeeeeee"), SelectValueCategorySeedData.PersonMembershipPaymentFrequency.Id, SelectValueSeedData.Yearly.Id, 30),
                ];
            }
        }

        public static IList<SelectValueMapping> PersonMembershipClubMappings
        {
            get
            {
                return
                [
                    new(Guid.Parse("ffffffff-ffff-4fff-8fff-ffffffffffff"), SelectValueCategorySeedData.PersonMembershipClub.Id, SelectValueSeedData.ORSOBerlin.Id, 10),
                    new(Guid.Parse("01234567-89ab-4cde-8f01-234567890abc"), SelectValueCategorySeedData.PersonMembershipClub.Id, SelectValueSeedData.ORSOFreiburg.Id, 20),
                ];
            }
        }
    }
}
