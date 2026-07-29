using Arena.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Arena.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetadataController : ControllerBase
{
    private readonly ArenaItemService _arenaItemService;

    public MetadataController(ArenaItemService arenaItemService)
    {
        _arenaItemService = arenaItemService;
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetItems()
    {
        return Ok(await _arenaItemService.GetArenaItemsAsync());
    }
}