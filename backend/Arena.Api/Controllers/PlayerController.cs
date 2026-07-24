using Microsoft.AspNetCore.Mvc;
using Arena.Api.Services;

namespace Arena.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly IMatchService _matchService;
    private readonly IMatchHistoryService _matchHistoryService;

    public PlayerController(
        IPlayerService playerService,
        IMatchService matchService,
        IMatchHistoryService matchHistoryService)
    {
        _playerService = playerService;
        _matchService = matchService;
        _matchHistoryService = matchHistoryService;
    }
    [HttpGet]
    public async Task<IActionResult> GetPlayer(
        [FromQuery] string gameName,
        [FromQuery] string tagLine)
    {
        try
        {
            var player = await _playerService.GetPlayerAsync(gameName, tagLine);

            return Ok(player);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                error = ex.Message
            });
        }
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncPlayer(
        [FromQuery] string gameName,
        [FromQuery] string tagLine)
    {
        try
        {
            var player = await _playerService.GetPlayerAsync(
                gameName,
                tagLine);

            await _matchService.SyncPlayerMatchesAsync(player);

            return Ok(new
            {
                message = "Player synchronized successfully."
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                error = ex.Message
            });
        }
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetPlayerStats(
        [FromQuery] string gameName,
        [FromQuery] string tagLine)
    {
        try
        {
            var stats = await _playerService.GetPlayerStatsAsync(
                gameName,
                tagLine,
                new Models.StatsFilter());

            return Ok(stats);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                error = ex.Message
            });
        }
    }

    [HttpGet("match-history")]
    public async Task<IActionResult> GetMatchHistory(
    [FromQuery] string gameName,
    [FromQuery] string tagLine)
    {
        try
        {
            var player = await _playerService.GetPlayerAsync(
                gameName,
                tagLine);

            var history = await _matchHistoryService.GetMatchHistoryAsync(
                player.Puuid);

            return Ok(history);
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