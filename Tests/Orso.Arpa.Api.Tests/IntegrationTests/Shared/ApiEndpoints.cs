using System;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Domain.Enums;

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

        public static class UsersController
        {
            private static string Users => $"{Base}/users";

            public static string Get() => Users;

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
        }

        public static class SelectValuesController
        {
            public static string Get(string tableName, string propertyName) => $"{Base}/tables/{tableName}/properties/{propertyName}";
        }

        public static class SectionsController
        {
            private static string Sections => $"{Base}/sections";

            public static string Get() => Sections;

            public static string GetInstruments() => $"{Sections}?instrumentsOnly=true";

            public static string GetTree(int? maxLevel) => $"{Sections}/tree?maxLevel={maxLevel}";
            public static string GetDoublingInstruments(Guid id) => $"{Sections}/{id}/doublinginstruments";
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

        public static class UrlsController
        {
            private static string Urls => $"{Base}/urls";

            public static string Get(Guid id) => $"{Urls}/{id}";

            public static string Put(Guid id) => $"{Urls}/{id}";

            public static string Delete(Guid id) => $"{Urls}/{id}";

            public static string AddRole(Guid id) => $"{Urls}/{id}/roles";

            public static string RemoveRole(Guid id, Guid roleId) => $"{Urls}/{id}/roles/{roleId}";
        }

        public static class MeController
        {
            private static string Me => $"{Base}/me";

            public static string GetUserProfile() => $"{Me}/profiles/user";

            public static string GetAppointments(int? limit = null, int? offset = null) => $"{Me}/appointments?limit={limit}&offset={offset}";

            public static string PutUserProfile() => $"{Me}/profiles/user";

            public static string SetAppointmentParticipationPrediction(Guid appointmentId, Guid predictionId)
                => $"{Me}/appointments/{appointmentId}/participation/prediction/{predictionId}";

            public static string SendQrCode() => $"{Me}/qrcode";
        }

        public static class MyMusicianProfilesController
        {
            private static string MyMusicianProfiles => $"{Base}/me/profiles/musician";

            public static string Post() => $"{MyMusicianProfiles}";

            public static string Get(bool includeDeactivated) => $"{MyMusicianProfiles}?includeDeactivated={includeDeactivated}";

            public static string GetById(Guid id) => $"{MyMusicianProfiles}/{id}";

            public static string SetProjectParticipation(Guid musicianProfileId, Guid projectId) => $"{MyMusicianProfiles}/{musicianProfileId}/projects/{projectId}/participation";
        }

        public static class RegionsController
        {
            private static string Regions => $"{Base}/Regions";

            public static string Get() => Regions;

            public static string Post() => Regions;

            public static string Put(Guid id) => $"{Regions}/{id}";

            public static string Get(Guid id) => $"{Regions}/{id}";

            public static string Delete(Guid id) => $"{Regions}/{id}";
        }

        public static class AppointmentsController
        {
            private static string Appointments => $"{Base}/Appointments";

            public static string Get(DateTime? date, DateRange range) => $"{Appointments}?date={date.Value.ToIsoString()}&range={range}";

            public static string Post() => Appointments;

            public static string Put(Guid id) => $"{Appointments}/{id}";

            public static string Get(Guid id) => $"{Appointments}/{id}";

            public static string Room(Guid id, Guid roomId) => $"{Appointments}/{id}/rooms/{roomId}";

            public static string Section(Guid id, Guid sectionId) => $"{Appointments}/{id}/sections/{sectionId}";

            public static string Project(Guid id, Guid projectId) => $"{Appointments}/{id}/projects/{projectId}";

            public static string SetVenue(Guid id, Guid? venueId) => $"{Appointments}/{id}/venue/set/{venueId}";

            public static string SetDates(Guid id) => $"{Appointments}/{id}/dates/set";

            public static string Delete(Guid id) => $"{Appointments}/{id}";

            public static string SetParticipationResult(Guid id, Guid personId, Guid resultId) =>
                $"{Appointments}/{id}/participations/{personId}/result/{resultId}";
        }
        public static class AuditLogsController
        {
            private static string AuditLogs => $"{Base}/auditLogs";

            public static string Get(Guid? entityId, int? skip, int? take) => $"{AuditLogs}?entityid={entityId}&skip={skip}&take={take}";
        }
        public static class MusicianProfilesController
        {
            private static string MusicianProfiles => $"{Base}/profiles/musicians";

            public static string Put(Guid id, bool active) => $"{MusicianProfiles}/{id}?active={active}";

            public static string Get(Guid id) => $"{MusicianProfiles}/{id}";

            public static string Delete(Guid id) => $"{MusicianProfiles}/{id}";
            public static string GetProjectParticipations(Guid id, bool includeCompleted) => $"{MusicianProfiles}/{id}/projectparticipations?includeCompleted={includeCompleted}";
        }
        public static class PersonsController
        {
            private static string Persons => $"{Base}/Persons";

            public static string Get() => Persons;

            public static string Post() => Persons;

            public static string Put(Guid id) => $"{Persons}/{id}";

            public static string Get(Guid id) => $"{Persons}/{id}";

            public static string Delete(Guid id) => $"{Persons}/{id}";

            public static string AddMusicianProfile(Guid id) => $"{Persons}/{id}/profiles/musician";
            public static string GetMusicianProfiles(Guid id, bool includeDeactivated) => $"{Persons}/{id}/profiles/musician?includeDeactivated={includeDeactivated}";
        }

        public static class TranslationController
        {
            private static string Translations = $"{Base}/translation";

            public static string Get(string culture) => $"{Translations}/?culture={culture}";

            public static string Put(string culture) => $"{Translations}?culture={culture}";
        }
    }
}