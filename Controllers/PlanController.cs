using Microsoft.AspNetCore.Mvc;
using smoking.Models;
using smoking.Services;

[ApiController]
[Route("api/[controller]")]
public class PlanController : ControllerBase
{
    private readonly PlanService _planService;

    public PlanController(PlanService planService)
    {
        _planService = planService;
    }

    // POST: api/plan
    [HttpPost]
    public IActionResult CreatePlan([FromBody] CreatePlanDto dto)
    {
        _planService.CreatePlan(dto);
        return Ok(new { message = "Plan created successfully" });
    }

    // POST: api/plan/{planId}/today-cigarettes
    [HttpPost("{planId}/today-cigarettes")]
    public IActionResult UpdateTodayCigarettes(int planId, [FromBody] UpdateTodayCigarettesDto dto)
    {
        _planService.UpdateTodayCigarettes(planId, dto.TodayCigarettes, DateTime.Today);
        return Ok(new { message = "Today's cigarettes updated" });
    }

    // GET: api/plan/myplan/{accountId}
    [HttpGet("myplan/{accountId}")]
    public IActionResult GetMyPlan(int accountId)
    {
        var result = _planService.GetMyPlan(accountId);
        return Ok(result);
    }
} 