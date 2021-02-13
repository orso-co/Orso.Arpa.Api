using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.RoomApplication;
using Orso.Arpa.Application.VenueApplication;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class VenuesController : BaseController
    {
        private readonly IVenueService _venueService;

        public VenuesController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        /// <summary>
        /// Gets all venues
        /// </summary>
        /// <returns>A list of venues</returns>
        /// <response code="200"></response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VenueDto>>> Get()
        {
            return Ok(await _venueService.GetAsync());
        }

        /// <summary>
        /// Gets the rooms of an existing venue
        /// </summary>
        /// <returns>A list of rooms</returns>
        /// <response code="200"></response>
        /// <response code="404">If venue could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
        [HttpGet("{id}/rooms")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms([FromRoute] Guid id)
        {
            return Ok(await _venueService.GetRoomsAsync(id));
        }
    }
}
