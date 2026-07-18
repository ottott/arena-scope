using Arena.Api.Models;
using Arena.Api.Data;
using Microsoft.EntityFrameworkCore;
using Arena.Api.Dtos;

namespace Arena.Api.Services;

public class PlayerService : IPlayerService
{
    private readonly RiotApiClient _riotApiClient;

    private readonly ArenaDbContext _context;
    private readonly IConfiguration _configuration;

    public PlayerService(
        RiotApiClient riotApiClient,
        ArenaDbContext context,
        IConfiguration configuration)
    {
        _riotApiClient = riotApiClient;
        _context = context;
        _configuration = configuration;
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

        var successfulPlacement = _configuration.GetValue<int>("Arena:SuccessfulPlacement");

        var successfulPlacements = await _context.Participants.CountAsync(
            p => p.Puuid == player.Puuid &&
                p.Placement <= successfulPlacement);
        
        

        var championStats = await _context.Participants
            .Where(p => p.Puuid == player.Puuid)
            .GroupBy(p => p.ChampionName)
            .Select(g => new ChampionStatsDto
            {
                ChampionName = g.Key,
                Games = g.Count(),
                Wins = g.Count(p => p.Placement == 1),
                AveragePlacement = g.Average(p => p.Placement),
                WinRate = (double)g.Count(p => p.Placement == 1) / g.Count() * 100,

                SuccessfulPlacements = g.Count(
                    p => p.Placement <= successfulPlacement),

                SuccessfulPlacementRate =
                    (double)g.Count(
                        p => p.Placement <= successfulPlacement)
                    / g.Count() * 100,
            })
            .OrderByDescending(c => c.Games)
            .ToListAsync();
        

        var duoStats = await (
            from me in _context.Participants

            join teammate in _context.Participants
                on new
                {
                    me.MatchId,
                    me.PlayerSubteamId
                }
                equals new
                {
                    teammate.MatchId,
                    teammate.PlayerSubteamId
                }

            where me.Puuid == player.Puuid
                && teammate.Puuid != player.Puuid

            group me by new
            {
                teammate.Puuid,
                teammate.GameName,
                teammate.TagLine
            }
            into g

            select new DuoStatsDto
            {
                GameName = g.Key.GameName,
                TagLine = g.Key.TagLine,

                Games = g.Count(),

                Wins = g.Count(p => p.Placement == 1),

                SuccessfulPlacements = g.Count(
                    p => p.Placement <= successfulPlacement),

                AveragePlacement = g.Average(p => p.Placement),

                WinRate =
                    (double)g.Count(p => p.Placement == 1)
                    / g.Count() * 100,

                SuccessfulPlacementRate =
                    (double)g.Count(
                        p => p.Placement <= successfulPlacement)
                    / g.Count() * 100
            })
            .OrderByDescending(d => d.Games)
            .ToListAsync();


        return new PlayerStatsDto
        {
            GameName = player.GameName,
            TagLine = player.TagLine,
            Games = games,
            Wins = wins,

            SuccessfulPlacements = successfulPlacements,

            SuccessfulPlacementRate =
                games == 0
                    ? 0
                    : (double)successfulPlacements / games * 100,

            AveragePlacement = averagePlacement,
            DuoStats = duoStats,
            ChampionStats = championStats
        };
    }
}
