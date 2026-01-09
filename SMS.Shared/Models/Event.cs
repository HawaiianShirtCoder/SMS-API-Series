namespace SMS.Shared.Models;

public class Event
{
    public int ID { get; set; }
    public string Name { get; set; } = "";
    public string DateOfEvent { get; set; } = "";
    public string StartTime { get; set; } = "";
    public string Details { get; set; } = "";
    public string Organiser { get; set; } = "";
}
