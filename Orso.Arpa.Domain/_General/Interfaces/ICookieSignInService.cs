using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Application.AuthApplication.Interfaces
{
    public interface ICookieSignInService
    {
        public Task RefreshSignIn(string token);

        public Task SignInUserWithClaims(User user, string token);
        public Task<bool> IsCookieSignInPossible(User user, string password);
    }
}