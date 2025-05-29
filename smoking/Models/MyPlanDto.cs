using System;
using System.Collections.Generic;
using smoking.Models;

public class MyPlanDto
{
    public int PlanId { get; set; }
    public DateTime QuitSmokingDate { get; set; }
    public int MaxCigarettes { get; set; }
    public int CigarettesQuit { get; set; }
    public decimal SaveMoney { get; set; }
    public int DaysSinceStart { get; set; }
    public int MaxCigarettesToday { get; set; }
    public List<PhaseDto> Phases { get; set; }
}