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
        }

        public static class ProjectsController
        {
            private static string Projects => $"{Base}/projects";

            public static string Get(bool includeCompleted = false) => $"{Projects}?includeCompleted={includeCompleted}";

            public static string Get(Guid id) => $"{Projects}/{id}";

            public static string Post() => Projects;

            public static string AddUrl(Guid id) => $"{Projects}/{id}/urls";

            public static string Put(Guid id) => $"{Projects}/{id}";

            public static string Delete(Guid id) => $"{Projects}/{id}";
        }

        public static class UrlsController
        {
            private static string Urls => $"{Base}/urls";

            public static string Get(Guid id) => $"{Urls}/{id}";

            public static string Put(Guid id) => $"{Urls}/{id}";

            public static string Delete(Guid id) => $"{Urls}/{id}";

            public static string AddRole(Guid id, Guid roleId) => $"{Urls}/{id}/roles/{roleId}";

            public static string RemoveRole(Guid id, Guid roleId) => $"{Urls}/{id}/roles/{roleId}";
        }

        public static class MeController
        {
            private static string Me => $"{Base}/users/me";

            public static string GetProfile() => $"{Me}/profile";

            public static string GetAppointments(int? limit, int? offset) => $"{Me}/appointments?limit={limit}&offset={offset}";

            public static string PutProfile() => $"{Me}/profile";

            public static string SetAppointmentParticipationPrediction(Guid appointmentId, Guid predictionId)
                => $"{Me}/appointments/{appointmentId}/participation/prediction/{predictionId}";

            public static string SendQrCode() => $"{Me}/qrcode";
        }

        public static class RegionsController
        {
            private static string Regions => $"{Base}/regions";

            public static string Get() => Regions;

            public static string Post() => Regions;

            public static string Put(Guid id) => $"{Regions}/{id}";

            public static string Get(Guid id) => $"{Regions}/{id}";

            public static string Delete(Guid id) => $"{Regions}/{id}";
        }

        public static class AppointmentsController
        {
            private static string Appointments => $"{Base}/appointments";

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
    }
}
