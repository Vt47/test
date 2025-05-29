using Microsoft.AspNetCore.Mvc;
using smoking12.Data;
using smoking12.Models;
using System.Linq;

namespace smoking12.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackageMembershipController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PackageMembershipController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPackages()
        {
            var packages = _context.PackageMemberships.ToList();
            return Ok(packages);
        }
    }
}