using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Shared.Models;

public class Fixture
{
    public string Opponent { get; set; }
    public DateTime DateOfFixture { get; set; }
    public string Venue { get; set; }
    public string StartTime { get; set; }
    public string Postcode { get; set; }
    public int NumberOfPlayersRequired { get; set; }
}
