﻿namespace SMS.Shared.DTOs
{
    public class PlayerSummaryDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public bool IsActivePlayer { get; set; }
    }
}
