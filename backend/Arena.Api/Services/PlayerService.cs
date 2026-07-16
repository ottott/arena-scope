using Arena.Api.Models;
using Arena.Api.Data;
using Microsoft.EntityFrameworkCore;
using Arena.Api.Dtos;

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
    var account = await _riotApiClient.GetAccountAsync(gameName, tagLine);

    var player = await _context.Players
        .FirstOrDefaultAsync(p => p.Puuid == account.Puuid);

    if (player == null)
    {
        player = new Player
        {
            GameName = account.GameName,
            TagLine = account.TagLine,
            Puuid = account.Puuid
        };

        _context.Players.Add(player);
    }
    else
    {
        player.GameName = gameName;
        player.TagLine = tagLine;
    }

    await _context.SaveChangesAsync();

    return player;
    }

    public async Task<PlayerStatsDto> GetPlayerStatsAsync(string gameName, string tagLine)
    {
        var player = await GetPlayerAsync(gameName, tagLine);
        
        var games = await _context.Participants.CountAsync(p => p.Puuid == player.Puuid);

        var wins = await _context.Participants.CountAsync(p => p.Puuid == player.Puuid && p.Placement == 1);

        var averagePlacement = await _context.Participants.Where(p => p.Puuid == player.Puuid).AverageAsync(p => p.Placement);
        
        return new PlayerStatsDto
        {
            GameName = player.GameName,
            TagLine = player.TagLine,
            Games = games,
            Wins = wins,
            AveragePlacement = averagePlacement
        };
    }
}
