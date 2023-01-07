namespace SMS.Shared.Models;

public class Fixture
{
    public int Id { get; set; }
    public string Opponent { get; set; } = string.Empty;
    public DateTime DateOfFixture { get; set; }
    public string Venue { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string Postcode { get; set; } = string.Empty;
    public int NumberOfPlayersRequired { get; set; }
}
