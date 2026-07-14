using Arena.Api.Models;

namespace Arena.Api.Services;

public interface IPlayerService
{
    Task<Player> GetPlayerAsync(string gameName, string tagLine);
}