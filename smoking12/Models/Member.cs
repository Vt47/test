using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smoking12.Models
{
    [Table("Member")]
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        public int AccountId { get; set; }

        public int? CigarettesPerDay { get; set; }
        public string? SmokingTime { get; set; }
        public int? GoalTime { get; set; }
        public string? Reason { get; set; }
        public decimal? CostPerCigarette { get; set; }
        public string? MedicalHistory { get; set; }
        public string? MostSmokingTime { get; set; }

        public string? FeedbackContent { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public byte? FeedbackRating { get; set; }

        public string? StatusProcess { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }
        public virtual Ranking Ranking { get; set; }
    }
}
