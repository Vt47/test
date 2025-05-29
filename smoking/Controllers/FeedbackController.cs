using smoking.Data;
using smoking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace smoking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly AppDbContext _context;
        public FeedbackController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetFeedbacks()
        {
            var feedbacks = _context.Member
                .Where(m => m.Feedback_content != null)
                .Select(m => new FeedbackDto
                {
                    MemberId = m.Member_ID,
                    FeedbackContent = m.Feedback_content,
                    FeedbackDate = m.Feedback_date,
                    FeedbackRating = m.Feedback_rating
                })
                .ToList();

            return Ok(feedbacks);
        }
    }
}