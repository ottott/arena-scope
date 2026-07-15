using Microsoft.AspNetCore.Mvc;
using Arena.Api.Services;

namespace Arena.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly IMatchService _matchService;

    public PlayerController(
        IPlayerService playerService,
        IMatchService matchService)
    {
        _playerService = playerService;
        _matchService = matchService;
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

            await _matchService.SyncPlayerMatchesAsync(player.Puuid);

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
}