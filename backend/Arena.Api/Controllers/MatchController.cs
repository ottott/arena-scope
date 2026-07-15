using Arena.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Arena.Api.Controllers;

[ApiController]
[Route("api/players/{puuid}/matches")]
public class MatchController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMatches(
        string puuid,
        [FromQuery] int count = 20)
    {
        try
        {
            var matches = await _matchService.GetMatchIdsAsync(
                puuid,
                count);

            return Ok(matches);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                error = ex.Message
            });
        }
    }
}

