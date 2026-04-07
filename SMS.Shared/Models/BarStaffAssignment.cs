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
}
