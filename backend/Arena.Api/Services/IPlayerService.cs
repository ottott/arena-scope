using Arena.Api.Models;
using Arena.Api.Dtos;

namespace Arena.Api.Services;

public interface IPlayerService
{
    Task<Player> GetPlayerAsync(string gameName, string tagLine);

    Task<PlayerStatsDto> GetPlayerStatsAsync(
        string gameName,
        string tagLine,
        StatsFilter? filter = null);
}