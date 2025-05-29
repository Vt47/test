using System.ComponentModel.DataAnnotations;

namespace smoking.Models
{
    public class Member
    {
        [Key]
        public int Member_ID { get; set; }
        public int Account_ID { get; set; }
        // ... các trường khác ...

        // Thêm các trường feedback dưới đây:
        public string Feedback_content { get; set; }
        public DateTime? Feedback_date { get; set; }
        public int? Feedback_rating { get; set; }
        public decimal CostPerCigarette { get; internal set; }
        public int CigarettesPerDay { get; internal set; }
    }
}