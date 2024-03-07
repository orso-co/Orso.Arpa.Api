using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orso.Arpa.Application.AuthApplication.Interfaces
{
    public interface ICookieSignInService
    {
        public Task RefreshSignIn(string token);
    }
}