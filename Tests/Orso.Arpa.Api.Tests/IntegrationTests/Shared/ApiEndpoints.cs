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
        }

        public static class UsersController
        {
            private static string Users => $"{Base}/users";
            public static string Delete(string username) => $"{Users}/{username}";
            public static string GetProfileOfCurrentUser() => $"{Users}/me/profile";
        }
    }
}
