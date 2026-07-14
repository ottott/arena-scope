using Arena.Api.Models;

namespace Arena.Api.Services;

public class PlayerService : IPlayerService
{
    private readonly RiotApiClient _riotApiClient;

    public PlayerService(RiotApiClient riotApiClient)
    {
        _riotApiClient = riotApiClient;
    }

    public async Task<Player> GetPlayerAsync(
        string gameName,
        string tagLine)
    {
        var account = await _riotApiClient.GetAccountAsync(gameName, tagLine);

        return new Player
        {
            GameName = gameName,
            TagLine = tagLine,
            Puuid = account.Puuid
        };
    }
}
