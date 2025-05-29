public class CreatePlanDto
{
    public int MemberId { get; set; }
    public DateTime QuitSmokingDate { get; set; }
    public int GoalTime { get; set; } // 180, 270, 365
    public int MaxCigarettes { get; set; }
} 