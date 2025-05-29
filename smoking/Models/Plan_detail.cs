using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Plan_detail
{
    [Key]
    public int Plan_detail_ID { get; set; }

    [Column("Plan_ID")]
    public int Plan_ID { get; set; }

    public int TodayCigarettes { get; set; }
    public int MaxCigarettes { get; set; }
    public DateTime Date { get; set; }
    public bool IsSuccess { get; set; }
}