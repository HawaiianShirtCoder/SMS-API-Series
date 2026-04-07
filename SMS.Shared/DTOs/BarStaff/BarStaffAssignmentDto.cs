namespace SMS.Shared.DTOs.BarStaff;

public class BarStaffAssignmentDto
{
    public int Id { get; set; }
    public int FixtureId { get; set; }
    public string StaffName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime AssignedDate { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool IsConfirmed { get; set; }
    public string? Notes { get; set; }
}
