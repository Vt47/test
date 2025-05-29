using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Plan
{
    [Key]
    public int Plan_ID { get; set; }

    [Column("Member_ID")]
    public int Member_ID { get; set; }

    public DateTime QuitSmokingDate { get; set; }
    public decimal SaveMoney { get; set; }
    public DateTime? Clock { get; set; }
    public int CigarettesQuit { get; set; }
    public int MaxCigarettes { get; set; }
}