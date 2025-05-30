using System.ComponentModel.DataAnnotations;
using smoking12.Models;


public class Ranking
{

    [Key]
    public int Ranking_ID { get; set; }
    public int Member_ID { get; set; }
    public string Badge { get; set; }
    public int Total_score { get; set; }


    
    public virtual Member Member { get; set; }
}
