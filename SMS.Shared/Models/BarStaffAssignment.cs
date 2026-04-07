using System.ComponentModel.DataAnnotations;

namespace SMS.Shared.Models;

public class BarStaffAssignment
{
    public int Id { get; set; }

    [Required]
    public int FixtureId { get; set; }

    [Required]
    [MaxLength(100)]
    public string StaffName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = string.Empty; // e.g., "Bartender", "Server", "Manager"

    [Required]
    public DateTime AssignedDate { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool IsConfirmed { get; set; } = false;

    public string? Notes { get; set; }
}
