using System;

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
        }

        public static class UsersController
        {
            private static string Users => $"{Base}/users";

            public static string Get() => Users;

            public static string Delete(string username) => $"{Users}/{username}";

            public static string GetProfileOfCurrentUser() => $"{Users}/me/profile";
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
    }
}
