using SMS.Shared.Enums;

namespace SMS.Shared.DTOs.Fixtures;

public class FixtureSummary
{
    public int Id { get; set; }
    public string Opponent { get; set; } = string.Empty;
    public DateTime DateOfFixture { get; set; }
    //public string Venue { get; set; } = string.Empty;
    public VenueEnum Venue { get; set; }
    public string StartTime { get; set; } = string.Empty;
    public int NumberOfPlayersRequired { get; set; }
}
