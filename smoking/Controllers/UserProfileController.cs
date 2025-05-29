using Microsoft.AspNetCore.Mvc;
using smoking.Data;
using smoking.Models;

[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly AppDbContext _context;
    public UserProfileController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{accountId}")]
    public ActionResult GetProfile(int accountId)
    {
        var user = _context.User.FirstOrDefault(u => u.Account_ID == accountId);
        var account = _context.Account.FirstOrDefault(a => a.Account_ID == accountId);
        if (user == null || account == null) return NotFound();

        return Ok(new
        {
            user.FullName,
            user.PhoneNumber,
            user.birthday,
            user.Sex,
            user.Role,
            account.Email
        });
    }

    [HttpPut("{accountId}")]
    public IActionResult UpdateProfile(int accountId, [FromBody] UpdateUserProfileDto dto)
    {
        var user = _context.User.FirstOrDefault(u => u.Account_ID == accountId);
        var account = _context.Account.FirstOrDefault(a => a.Account_ID == accountId);
        if (user == null || account == null) return NotFound();

        if (!string.IsNullOrWhiteSpace(dto.FullName) && dto.FullName != "string")
            user.FullName = dto.FullName;

        if (!string.IsNullOrWhiteSpace(dto.PhoneNumber) && dto.PhoneNumber != "string")
            user.PhoneNumber = dto.PhoneNumber;

        if (dto.Birthday != null && dto.Birthday.ToString() != "string")
            user.birthday = dto.Birthday;

        if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != "string")
            account.Email = dto.Email; 

        _context.SaveChanges();
        return NoContent();
    }
}