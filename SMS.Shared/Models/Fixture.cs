namespace SMS.Shared.Models;

public class Fixture
{
    public int Id { get; set; }
    public string Opponent { get; set; }
    public DateTime DateOfFixture { get; set; }
    public string Venue { get; set; }
    public string StartTime { get; set; }
    public string Postcode { get; set; }
    public int NumberOfPlayersRequired { get; set; }
}
