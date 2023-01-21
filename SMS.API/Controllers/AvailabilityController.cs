using Microsoft.AspNetCore.Mvc;
using SMS.Shared.BLL;
using SMS.Shared.DTOs.Availability;

namespace SMS.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly ISMSLogic _businessLogic;

        public AvailabilityController(ISMSLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        [Route("PlayersAvailableForFixture/{fixtureId}")]
        [HttpGet]
        public async Task<ActionResult> GetPlayersAvailableForFixture(int fixtureId)
        {
            var details = await _businessLogic.GetPlayersAvailableForFixture(fixtureId);
            if (details == null)
            {
                return NotFound("No Availability details found for fixture");
            }
            return Ok(details);
        }

        [Route("PlayerAvailabilitySummary/{playerId}")]
        [HttpGet]
        public async Task<ActionResult> GetPlayerAvailabilitySummary(int playerId)
        {
            var details = await _businessLogic.GetPlayerAvailabilitySummary(playerId);
            if (details is null)
            {
                return NotFound("No availability for player found.");
            }
            return Ok(details);
        }

        [Route("FixtureAvailabilityCounts")]
        [HttpGet]
        public async Task<ActionResult> FixtureAvailabilityCounts()
        {
            var counts = await _businessLogic.FixtureAvailabilityCounts();
            return Ok(counts);
        }

        [HttpPost]
        public async Task<ActionResult> PostAvailability(AddAvailabilityDto availabilityDataEntry)
        {
            var response = await _businessLogic.SaveAvailability(availabilityDataEntry);
            if (response.ExecutionStatus != Shared.Enums.ExecuteCommandEnum.Ok)
            {
                return StatusCode((int)response.ExecutionStatus, response.ErrorMessage);
            }
            return NoContent();

        }

    }
}

