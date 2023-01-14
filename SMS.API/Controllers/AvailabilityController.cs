using Microsoft.AspNetCore.Mvc;
using SMS.API.Fake_Database;
using SMS.Shared.DTOs.Availability;
using SMS.Shared.Models;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        [Route("PlayersAvailableForFixture/{fixtureId}")]
        [HttpGet]
        public ActionResult GetPlayersAvailableForFixture(int fixtureId)
        {
            var fixtureDetails = (from f in InMemoryDatabase.Fixtures
                                  join a in InMemoryDatabase.Availabilities
                                       on f.Id equals a.FixtureId
                                  where a.FixtureId == fixtureId
                                  select new PlayersAvailableForFixtureDto
                                  {
                                      FixtureId = a.FixtureId,
                                      Opponents = f.Opponent,
                                      DateOfFixture = f.DateOfFixture,
                                      Venue = f.Venue,
                                      StartTime = f.StartTime,
                                  }).FirstOrDefault();
            if (fixtureDetails == null)
            {
                return NotFound("Could not find the fixture availability details requested");
            }


            var playersAvailable = (from p in InMemoryDatabase.Players
                                    join a in InMemoryDatabase.Availabilities
                                         on p.Id equals a.PlayerId
                                    where a.FixtureId == fixtureId
                                    select new PlayersAvailableDto
                                    {
                                        PlayerId = p.Id,
                                        Fullname = $"{p.Firstname} {p.Lastname}"
                                    }).ToList();
            fixtureDetails.AvailablePlayers = playersAvailable;

            return Ok(fixtureDetails);
        }

        [Route("PlayerAvailabilitySummary/{playerId}")]
        [HttpGet]
        public ActionResult GetPlayerAvailabilitySummary(int playerId)
        {
            var me = (from p in InMemoryDatabase.Players
                      where p.Id == playerId
                      select new MyAvailabilitySummaryDto
                      {
                          PlayerId = p.Id,
                          Fullname = $"{p.Firstname} {p.Lastname}"
                      }).FirstOrDefault();
            if (me == null)
            {
                return NotFound("Could not find the player availability requested");
            }
            var myFixture = (from f in InMemoryDatabase.Fixtures
                             join a in InMemoryDatabase.Availabilities
                                 on f.Id equals a.FixtureId
                             where a.PlayerId == playerId

                             select new MyFixturesDto
                             {
                                 FixtureId = f.Id,
                                 FixtureDetail = $"{f.Opponent} ({f.Venue}) - {f.StartTime}",
                                 DateOfFixture = f.DateOfFixture
                             }).ToList();
            me.MyFixtures = myFixture.OrderBy(f => f.DateOfFixture).ToList();
            return Ok(me);
        }

        [Route("FixtureAvailabilityCounts")]
        [HttpGet]
        public ActionResult FixtureAvailabilityCounts()
        {
            var counts = (from a in InMemoryDatabase.Availabilities
                          group a by a.FixtureId into FixtureCount
                          select new FixtureCountDto
                          {
                              FixtureId = FixtureCount.Key,
                              PlayersAvailableCount = FixtureCount.Count()
                          }).ToList();

            var countSummary = (from f in InMemoryDatabase.Fixtures
                                join c in counts
                                 on f.Id equals c.FixtureId
                                select new FixtureCountSummaryDto
                                {
                                    FixtureId = f.Id,
                                    FixtureDetail = $"{f.Opponent} ({f.Venue}) - {f.StartTime}",
                                    DateOfFixture = f.DateOfFixture,
                                    PlayersAvailable = c.PlayersAvailableCount,
                                    PlayersRequired = f.NumberOfPlayersRequired
                                }).ToList();
            return Ok(countSummary.OrderBy(c => c.DateOfFixture).ToList());
        }

        [HttpPost]
        public ActionResult PostAvailability(AddAvailabilityDto availabilityDataEntry)
        {
            var currentRecord = InMemoryDatabase.Availabilities
                .FirstOrDefault(x => x.PlayerId == availabilityDataEntry.PlayerId
                && x.FixtureId == availabilityDataEntry.FixtureId);
            if (currentRecord == null && availabilityDataEntry.IsAvailable)
            {
                // Need to add the new entry
                InMemoryDatabase.Availabilities.Add(new Availability
                {
                    Id = NextIdHelper(),
                    FixtureId = availabilityDataEntry.FixtureId,
                    PlayerId = availabilityDataEntry.PlayerId,
                });
                return Ok("Added availability for this fixture");
            }
            else
            {
                if (availabilityDataEntry.IsAvailable == false && currentRecord != null)
                {
                    InMemoryDatabase.Availabilities.Remove(currentRecord);
                    return NoContent();
                }
            }
            return Ok("No changes made");

        }


        private int NextIdHelper()
        {
            var lastId = InMemoryDatabase.Availabilities.Max(x => x.Id);
            return lastId + 1;
        }
    }
}

