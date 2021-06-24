using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/profiles/musicians/{id}/doublinginstruments")]
    public class MusicianProfileDoublingInstrumentsController : BaseController
    {
        private readonly IDoublingInstrumentService _doublingInstrumentService;

        public MusicianProfileDoublingInstrumentsController(IDoublingInstrumentService doublingInstrumentService)
        {
            _doublingInstrumentService = doublingInstrumentService;
        }

        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost()]
        public async Task<ActionResult<DoublingInstrumentDto>> Post(DoublingInstrumentCreateDto doublingInstrumentCreateDto)
        {
            return Ok(await _doublingInstrumentService.CreateAsync(doublingInstrumentCreateDto));
        }
    }
}
