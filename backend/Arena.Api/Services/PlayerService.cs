using Arena.Api.Models;
using Arena.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Arena.Api.Services;

public class PlayerService : IPlayerService
{
    private readonly RiotApiClient _riotApiClient;

    private readonly ArenaDbContext _context;

    public PlayerService(
        RiotApiClient riotApiClient,
        ArenaDbContext context)
    {
        _riotApiClient = riotApiClient;
        _context = context;
    }
    public async Task<Player> GetPlayerAsync(
        string gameName,
        string tagLine)
    {

        var existingPlayer = await _context.Players
            .FirstOrDefaultAsync(p =>
                p.GameName == gameName &&
                p.TagLine == tagLine);

        if (existingPlayer is not null)
        {
            return existingPlayer;
        }

        var account = await _riotApiClient.GetAccountAsync(gameName, tagLine);

        var player = new Player
        {
            GameName = account.GameName,
            TagLine = account.TagLine,
            Puuid = account.Puuid
        };

        _context.Players.Add(player);

        await _context.SaveChangesAsync();

        return player;
    }
}
