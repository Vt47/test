namespace smoking.Models
{
    public class FeedbackDto
    {
        public int MemberId { get; set; }
        public string FeedbackContent { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public int? FeedbackRating { get; set; }
    }
}