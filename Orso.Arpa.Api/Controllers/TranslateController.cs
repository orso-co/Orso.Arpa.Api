using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Localization;

namespace Orso.Arpa.Api.Controllers
{
    // TODO: Delete this controller after testing!
    public class TranslateController : BaseController
    {

        [HttpGet]
        [AllowAnonymous]
        public async Task<TranslateObject> Get()
        {
            return await Task.Run(() => new TranslateObject());
        }
    }
}
