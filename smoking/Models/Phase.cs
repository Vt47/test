using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Phase
{
    [Key]
    public int PhaseID { get; set; }

    [Column("Plan_ID")]
    public int Plan_ID { get; set; }

    public int PhaseNumber { get; set; }
    public DateTime StartDatePhase { get; set; }
    public DateTime EndDatePhase { get; set; }
    public string StatusPhase { get; set; }
}