using System;

namespace Orso.Arpa.Domain.General.Configuration
{
    public class SeedConfiguration
    {
        public bool SeedInitialAdmin { get; set; }
        public bool SeedTestPerformer { get; set; }
        public bool SeedTestNews { get; set; }
        public InitialAdminConfiguration InitialAdmin { get; set; }
    }

    public class InitialAdminConfiguration
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public Guid GenderId { get; set; }
    }
}
