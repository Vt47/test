using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smoking12.Models
{
    [Table("Package_membership")]
    public class PackageMembership
    {

        [Key] 
        public int Package_membership_ID { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
    }
}