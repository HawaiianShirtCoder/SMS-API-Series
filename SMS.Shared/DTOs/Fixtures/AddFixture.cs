using SMS.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace SMS.Shared.DTOs.Fixtures;

public class AddFixture
{
    [Required]
    [MaxLength(100)]
    public string Opponent { get; set; } = string.Empty;
    [Required]
    public DateTime DateOfFixture { get; set; }
    [Required]
    public VenueEnum Venue { get; set; }
    [Required]
    public string StartTime { get; set; } = string.Empty;
    public string Postcode { get; set; } = "n/a";
    [Required]
    public int NumberOfPlayersRequired { get; set; }
}
