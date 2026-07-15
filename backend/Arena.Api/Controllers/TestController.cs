using Arena.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Arena.Api.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly IMatchService _matchService;

    public TestController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpPost("import/{matchId}")]
    public async Task<IActionResult> ImportMatch(string matchId)
    {
        await _matchService.ImportMatchAsync(matchId);

        return Ok("Match imported successfully.");
    }
}