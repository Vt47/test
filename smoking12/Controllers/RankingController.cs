using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smoking12.Data;
using smoking12.Models;
using System.Security.Claims;

namespace smoking12.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RankingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RankingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("top")]
        [AllowAnonymous] // Cho phép cả người chưa đăng nhập truy cập
        public async Task<ActionResult<TopRankingResult>> GetTopRanking()
        {
            // 1. Lấy memberId từ JWT token nếu có
            int? memberId = null;
            var claim = User.Claims.FirstOrDefault(c => c.Type == "memberId");
            if (claim != null && int.TryParse(claim.Value, out int parsedId))
            {
                memberId = parsedId;
            }

            // 2. Truy vấn danh sách xếp hạng có đầy đủ thông tin
            var rankings = await _context.Ranking
                .Include(r => r.Member)
                    .ThenInclude(m => m.Account)
                        .ThenInclude(a => a.User)
                .OrderByDescending(r => r.Total_score)
                .ToListAsync();

            // 3. Tạo danh sách DTO và gán xếp hạng (Rank)
            var rankingDTOs = rankings
                .Select((r, index) => new RankingDTO
                {
                    Rank = index + 1,
                    MemberID = r.Member_ID,
                    FullName = r.Member.Account.User.FullName,
                    Badge = r.Badge,
                    TotalScore = r.Total_score
                })
                .ToList();

            // 4. Tìm xếp hạng của chính người dùng (nếu có)
            RankingDTO? myRanking = null;
            if (memberId.HasValue)
            {
                myRanking = rankingDTOs.FirstOrDefault(r => r.MemberID == memberId.Value);
            }

            // 5. Trả kết quả
            return Ok(new TopRankingResult
            {
                Rankings = rankingDTOs,
                MyRanking = myRanking
            });
        }
    }
}
