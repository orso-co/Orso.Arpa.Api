using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.RegionDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Misc.Extensions;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    public static class ApiEndpoints
    {
        private static string Base => "api";

        public static class AuthController
        {
            private static string Auth => $"{Base}/auth";

            public static string Login() => $"{Auth}/login";

            public static string Register() => $"{Auth}/register";

            public static string Password() => $"{Auth}/password";

            public static string SetRole() => $"{Auth}/role";

            public static string ForgotPassword() => $"{Auth}/forgotpassword";

            public static string ResetPassword() => $"{Auth}/resetpassword";

            public static string ConfirmEmail() => $"{Auth}/confirmemail";

            public static string CreateNewEmailConfirmationToken() => $"{Auth}/emailconfirmationtoken";

            public static string RefreshToken() => $"{Auth}/refreshtoken";

            public static string Logout() => $"{Auth}/logout";
        }

        public static class ClubController {
            private static string Club => $"{Base}/club";
            public static string Get() => Club;
        }
        
        public static class UsersController
        {
            private static string Users => $"{Base}/users";

            public static string Get(UserStatus? userStatus) => userStatus is null ? Users : $"{Users}?userStatus={userStatus}";

            public static string GetById(Guid id) => $"{Users}/{id}";

            public static string Delete(string username) => $"{Users}/{username}";
        }

        public static class RolesController
        {
            private static string Roles => $"{Base}/roles";

            public static string Get() => Roles;
        }

        public static class VenuesController
        {
            private static string Venues => $"{Base}/venues";

            public static string Get() => Venues;

            public static string GetRooms(Guid id) => $"{Venues}/{id}/rooms";

            public static string Post() => Venues;

            public static string Put(Guid id) => $"{Venues}/{id}";

            public static string Delete(Guid id) => $"{Venues}/{id}";
        }

        public static class SelectValuesController
        {
            public static string Get(string tableName, string propertyName) => $"{Base}/tables/{tableName}/properties/{propertyName}";
        }

        public static class SectionsController
        {
            private static string Sections => $"{Base}/sections";

            public static string Get() => Sections;

            public static string GetInstrumentsWithChildren() => $"{Sections}?instrumentsWithChildrenOnly=true";

            public static string GetTree(int? maxLevel) => $"{Sections}/tree?maxLevel={maxLevel}";
            public static string GetDoublingInstruments(Guid id) => $"{Sections}/{id}/doublinginstruments";
            public static string GetPositions(Guid id) => $"{Sections}/{id}/positions";
        }

        public static class ProjectsController
        {
            private static string Projects => $"{Base}/Projects";

            public static string Get(bool includeCompleted = false) => $"{Projects}?includeCompleted={includeCompleted}";

            public static string Get(Guid id) => $"{Projects}/{id}";

            public static string Post() => Projects;

            public static string AddUrl(Guid id) => $"{Projects}/{id}/urls";

            public static string Put(Guid id) => $"{Projects}/{id}";

            public static string Delete(Guid id) => $"{Projects}/{id}";

            public static string GetParticipations(Guid id) => $"{Get(id)}/participations";
            public static string SetParticipation(Guid id) => $"{GetParticipations(id)}";
        }

        public static class UrlsController
        {
            private static string Urls => $"{Base}/Urls";

            public static string Get(Guid id) => $"{Urls}/{id}";

            public static string Put(Guid id) => $"{Urls}/{id}";

            public static string Delete(Guid id) => $"{Urls}/{id}";

            public static string AddRole(Guid id, Guid roleId) => $"{Urls}/{id}/roles/{roleId}";

            public static string RemoveRole(Guid id, Guid roleId) => $"{Urls}/{id}/roles/{roleId}";
        }

        public static class MeController
        {
            private static string Me => $"{Base}/me";

            public static string GetUserProfile() => $"{Me}/profiles/user";

            public static string GetAppointments(int? limit = null, int? offset = null, bool passed = false) => $"{Me}/appointments?limit={limit}&offset={offset}&passed={passed}";

            public static string PutUserProfile() => $"{Me}/profiles/user";

            public static string SetAppointmentParticipationPrediction(Guid appointmentId)
                => $"{Me}/appointments/{appointmentId}/participation/prediction";

            public static string GetQrCode(bool sendEmail) => $"{Me}/qrcode?sendEmail={sendEmail}";
        }

        public static class MyMusicianProfilesController
        {
            private static string MyMusicianProfiles => $"{Base}/me/profiles/musician";

            public static string Post() => $"{MyMusicianProfiles}";

            public static string Get(bool includeDeactivated) => $"{MyMusicianProfiles}?includeDeactivated={includeDeactivated}";

            public static string Put(Guid id) => $"{MyMusicianProfiles}/{id}";

            public static string GetById(Guid id) => $"{MyMusicianProfiles}/{id}";
        }

        public static class MyMusicianProfileDoublingInstrumentsController
        {
            private static string BaseUrl(Guid id) => $"{Base}/me/profiles/musician/{id}/doublinginstruments";
            public static string Post(Guid musicianProfileId) => BaseUrl(musicianProfileId);
            public static string Put(Guid musicianProfileId, Guid doublingInstrumentId) => $"{BaseUrl(musicianProfileId)}/{doublingInstrumentId}";
        }

        public static class MyMusicianProfileRegionPreferencesController
        {
            private static string BaseUrl(Guid id) => $"{Base}/me/profiles/musician/{id}/regionpreferences";
            public static string Post(Guid musicianProfileId) => BaseUrl(musicianProfileId);

            public static string Put(Guid musicianProfileId, Guid regionPreferenceId) => $"{BaseUrl(musicianProfileId)}/{regionPreferenceId}";

            public static string Delete(Guid musicianProfileId, Guid regionPreferenceId) => $"{BaseUrl(musicianProfileId)}/{regionPreferenceId}";
        }

        public static class MyMusicianProfileDocumentsController
        {
            private static string BaseUrl(Guid id) => $"{Base}/me/profiles/musician/{id}/documents";
            public static string Add(Guid musicianProfileId, Guid documentId) => $"{BaseUrl(musicianProfileId)}/{documentId}";
            public static string Remove(Guid musicianProfileId, Guid documentId) => $"{BaseUrl(musicianProfileId)}/{documentId}";
        }

        public static class RegionsController
        {
            private static string Regions => $"{Base}/Regions";

            public static string Get(RegionPreferenceType regionPreferenceType = RegionPreferenceType.None) => $"{Regions}?regionpreferencetype={regionPreferenceType}";

            public static string Post() => Regions;

            public static string Put(Guid id) => $"{Regions}/{id}";

            public static string Get(Guid id) => $"{Regions}/{id}";

            public static string Delete(Guid id) => $"{Regions}/{id}";
        }

        public static class RoomsController
        {
            private static string Rooms => $"{Base}/Rooms";

            public static string Put(Guid id) => $"{Rooms}/{id}";

            public static string Get(Guid id) => $"{Rooms}/{id}";

            public static string Delete(Guid id) => $"{Rooms}/{id}";

            public static string AddSection(Guid id) => $"{Rooms}/{id}/instruments";
            public static string AddEquipment(Guid id) => $"{Rooms}/{id}/equipments";
        }

        public static class RoomSectionsController
        {
            private static string RoomSections => $"{Base}/RoomSections";

            public static string Put(Guid id) => $"{RoomSections}/{id}";

            public static string Get(Guid id) => $"{RoomSections}/{id}";

            public static string Delete(Guid id) => $"{RoomSections}/{id}";
        }

        public static class RoomEquipmentsController
        {
            private static string RoomEquipments => $"{Base}/RoomEquipments";

            public static string Put(Guid id) => $"{RoomEquipments}/{id}";

            public static string Get(Guid id) => $"{RoomEquipments}/{id}";

            public static string Delete(Guid id) => $"{RoomEquipments}/{id}";
        }

        public static class AppointmentsController
        {
            private static string Appointments => $"{Base}/Appointments";

            public static string Get(DateTime? date, DateRange range) => $"{Appointments}?date={date.Value.ToIsoString()}&range={range}";

            public static string Post() => Appointments;

            public static string Put(Guid id) => $"{Appointments}/{id}";

            public static string Get(Guid id, bool includeParticipations) => $"{Appointments}/{id}?includeParticipations={includeParticipations}";

            public static string Get(Guid id) => $"{Appointments}/{id}";

            public static string Room(Guid id, Guid roomId) => $"{Appointments}/{id}/rooms/{roomId}";

            public static string Section(Guid id, Guid sectionId) => $"{Appointments}/{id}/sections/{sectionId}";

            public static string Project(Guid id, Guid projectId) => $"{Appointments}/{id}/projects/{projectId}";

            public static string SetVenue(Guid id, Guid? venueId) => $"{Appointments}/{id}/venue/set/{venueId}";

            public static string SetDates(Guid id) => $"{Appointments}/{id}/dates/set";

            public static string Delete(Guid id) => $"{Appointments}/{id}";

            public static string SetParticipationResult(Guid id, Guid personId) =>
                $"{Appointments}/{id}/participations/{personId}/result";

            public static string SetParticipationPrediction(Guid id, Guid personId) =>
                $"{Appointments}/{id}/participations/{personId}/prediction";

            public static string SendAppointmentChangedNotification(Guid id, bool forceSending) =>
                $"{Appointments}/{id}/notification?forceSending={forceSending}";
        }

        public static class AuditLogsController
        {
            private static string AuditLogs => $"{Base}/auditLogs";

            public static string Get(Guid? entityId, int? skip, int? take) => $"{AuditLogs}?entityid={entityId}&skip={skip}&take={take}";
        }

        public static class MusicianProfilesController
        {
            private static string MusicianProfiles => $"{Base}/profiles/musicians";

            public static string Put(Guid id) => $"{MusicianProfiles}/{id}";

            public static string Get() => $"{MusicianProfiles}";

            public static string Get(Guid id) => $"{MusicianProfiles}/{id}";

            public static string Delete(Guid id) => $"{MusicianProfiles}/{id}";

            public static string GetProjectParticipations(Guid id, bool includeCompleted) => $"{MusicianProfiles}/{id}/projectparticipations?includeCompleted={includeCompleted}";
            public static string GetAppointmentParticipations(Guid id, Guid? projectId, DateTime? startTime, DateTime? endTime)
            {
                string url = $"{MusicianProfiles}/{id}/appointmentparticipations";
                var param = new Dictionary<string, string>();
                if (projectId.HasValue)
                {
                    param.Add("projectId", projectId.ToString());
                }
                if (startTime.HasValue)
                {
                    param.Add("startTime", startTime.Value.ToIsoString());
                }
                if (endTime.HasValue)
                {
                    param.Add("endTime", endTime.Value.ToIsoString());
                }

                return QueryHelpers.AddQueryString(url, param);
            }

            public static string AddEducation(Guid id) => $"{MusicianProfiles}/{id}/educations";
            public static string AddCurriculumVitaeReference(Guid id) => $"{MusicianProfiles}/{id}/curriculumVitaeReferences";
            public static string AddDeactivation(Guid id) => $"{MusicianProfiles}/{id}/deactivation";
            public static string RemoveDeactivation(Guid id) => $"{MusicianProfiles}/{id}/deactivation";
        }

        public static class MusicianProfileDoublingInstrumentsController
        {
            private static string BaseUrl(Guid id) => $"{Base}/profiles/musicians/{id}/doublinginstruments";
            public static string Post(Guid id) => BaseUrl(id);
            public static string Put(Guid id, Guid doublingId) => $"{BaseUrl(id)}/{doublingId}";
            public static string Delete(Guid id, Guid doublingId) => $"{BaseUrl(id)}/{doublingId}";
        }

        public static class EducationsController
        {
            private static string Educations => $"{Base}/Educations";
            public static string Get(Guid id) => $"{Educations}/{id}";
            public static string Put(Guid id) => $"{Educations}/{id}";
            public static string Delete(Guid id) => $"{Educations}/{id}";
        }

        public static class CurriculumVitaeReferencesController
        {
            private static string CurriculumVitaeReferences => $"{Base}/CurriculumVitaeReferences";
            public static string Get(Guid id) => $"{CurriculumVitaeReferences}/{id}";
            public static string Put(Guid id) => $"{CurriculumVitaeReferences}/{id}";
            public static string Delete(Guid id) => $"{CurriculumVitaeReferences}/{id}";
        }

        public static class PersonsController
        {
            private static string Persons => $"{Base}/Persons";

            public static string Get() => Persons;

            public static string GetReduced(DateTime date) => $"{Persons}/reduced?birthday={date.ToIsoString()}";

            public static string Post() => Persons;

            public static string Put(Guid id) => $"{Persons}/{id}";

            public static string Get(Guid id) => $"{Persons}/{id}";

            public static string Delete(Guid id) => $"{Persons}/{id}";

            public static string AddMusicianProfile(Guid id) => $"{Persons}/{id}/profiles/musician";
            public static string GetMusicianProfiles(Guid id, bool includeDeactivated) => $"{Persons}/{id}/profiles/musician?includeDeactivated={includeDeactivated}";
            public static string GetUser(Guid id) => $"{Persons}/{id}/profiles/user";


            public static string StakeholderGroups(Guid id, Guid stakeholderGroupId) => $"{Persons}/{id}/stakeholdergroups/{stakeholderGroupId}";
            public static string Invite() => $"{Persons}/invite";
        }

        public static class NewsController
        {
            private static string News => $"{Base}/News";
            public static string Get(int? limit, int? offset, bool includeHidden) => $"{News}?limit={limit}&offset{offset}&includeHidden={includeHidden}";
            public static string Post() => News;
            public static string Put(Guid id) => $"{News}/{id}";
            public static string Get(Guid id) => $"{News}/{id}";
            public static string Delete(Guid id) => $"{News}/{id}";
        }

        public static class TranslationController
        {
            private static readonly string Translations = $"{Base}/translations";

            public static string Get(string culture) => $"{Translations}/?culture={culture}";

            public static string Put(string culture) => $"{Translations}?culture={culture}";
        }

        public static class PersonBankAccountsController
        {
            private static string PersonBankAccounts(Guid id) => $"{Base}/persons/{id}/bankaccounts";

            public static string Post(Guid personId) => PersonBankAccounts(personId);

            public static string Put(Guid personId, Guid bankAccountId) => $"{PersonBankAccounts(personId)}/{bankAccountId}";

            public static string Delete(Guid personId, Guid bankAccountId) => $"{PersonBankAccounts(personId)}/{bankAccountId}";
        }

        public static class PersonContactDetailsController
        {
            private static string PersonContactDetails(Guid id) => $"{Base}/persons/{id}/contactdetails";

            public static string Post(Guid personId) => PersonContactDetails(personId);

            public static string Put(Guid personId, Guid contactDetailId) => $"{PersonContactDetails(personId)}/{contactDetailId}";

            public static string Delete(Guid personId, Guid contactDetailId) => $"{PersonContactDetails(personId)}/{contactDetailId}";
        }

        public static class PersonAddressesController
        {
            private static string PersonAddresses(Guid id) => $"{Base}/persons/{id}/addresses";

            public static string Post(Guid personId) => PersonAddresses(personId);

            public static string Put(Guid personId, Guid contactDetailId) => $"{PersonAddresses(personId)}/{contactDetailId}";

            public static string Delete(Guid personId, Guid contactDetailId) => $"{PersonAddresses(personId)}/{contactDetailId}";
        }

        public static class MyContactDetailsController
        {
            private static string MyContactDetails() => $"{Base}/me/contactdetails";

            public static string Post() => MyContactDetails();

            public static string Put(Guid contactDetailId) => $"{MyContactDetails()}/{contactDetailId}";

            public static string Delete(Guid contactDetailId) => $"{MyContactDetails()}/{contactDetailId}";
        }

        public static class MyProjectsController
        {
            private static string MyProjects() => $"{Base}/me/projects";
            public static string Get(int? offset = null, int? limit = null, bool includeCompleted = false) =>
                $"{MyProjects()}?offset={offset}&limit={limit}&includeCompleted={includeCompleted}";

            public static string SetParticipation(Guid projectId) =>
                $"{MyProjects()}/{projectId}/participation";
        }
    }
}
