using System;

public class PhaseDto
{
    public int PhaseNumber { get; set; }
    public DateTime StartDatePhase { get; set; }
    public DateTime EndDatePhase { get; set; }
    public string StatusPhase { get; set; }
    public int FailDays { get; set; }
    public int TotalDays { get; set; }
} 