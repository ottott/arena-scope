using Microsoft.AspNetCore.Mvc;
using Arena.Api.Services;

namespace Arena.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
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
}