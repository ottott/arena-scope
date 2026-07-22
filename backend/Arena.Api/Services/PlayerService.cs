using Arena.Api.Models;
using Arena.Api.Data;
using Microsoft.EntityFrameworkCore;
using Arena.Api.Dtos;

namespace Arena.Api.Services;

public class PlayerService : IPlayerService
{
    private readonly RiotApiClient _riotApiClient;

    private readonly ArenaDbContext _context;
    private readonly StatsService _statsService;

    public PlayerService(
        RiotApiClient riotApiClient,
        ArenaDbContext context,
        StatsService statsService)
    {
        _riotApiClient = riotApiClient;
        _context = context;
        _statsService = statsService;
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
               
        var overallStats = await _statsService.GetOverallStatsAsync(player.Puuid);
        
        var championStats = await _statsService.GetChampionStatsAsync(player.Puuid);
        
        var duoStats = await _statsService.GetDuoStatsAsync(player.Puuid);

        var itemStats = await _statsService.GetItemStatsAsync(player.Puuid);

        var augmentStats = await _statsService.GetAugmentStatsAsync(player.Puuid);

        var placementDistribution = await _statsService.GetPlacementDistributionAsync(player.Puuid);



        return new PlayerStatsDto
        {
            GameName = player.GameName,
            TagLine = player.TagLine,
            Games = overallStats.Games,
            Wins = overallStats.Wins,
            SuccessfulPlacements = overallStats.SuccessfulPlacements,
            SuccessfulPlacementRate = overallStats.SuccessfulPlacementRate,
            WinRate = overallStats.WinRate,
            AveragePlacement = overallStats.AveragePlacement,
            DuoStats = duoStats,
            ChampionStats = championStats,
            ItemStats = itemStats,
            AugmentStats = augmentStats,
            PlacementDistribution = placementDistribution
        };
    }
}
