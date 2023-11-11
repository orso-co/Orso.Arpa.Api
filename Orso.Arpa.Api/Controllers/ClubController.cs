using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.ClubApplication.Interfaces;
using Orso.Arpa.Application.ClubApplication.Model;

namespace Orso.Arpa.Api.Controllers;

public class ClubController : BaseController
{
    private readonly IClubService _clubService;


    public ClubController(IClubService clubService)
    {
        _clubService = clubService;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ClubDto>> Get()
    {
        return await _clubService.GetAsync();
    }
}
