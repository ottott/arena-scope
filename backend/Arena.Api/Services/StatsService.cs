using Arena.Api.Data;
using Arena.Api.Models;
using Arena.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Arena.Api.Services;

public class StatsService
{
    private readonly ArenaDbContext _context;
    private readonly IConfiguration _configuration;

    public StatsService(
        ArenaDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }


    private IQueryable<Participant> GetPlayerParticipants(string puuid)
    {
        return _context.Participants
            .Where(p => p.Puuid == puuid);
    }

    private EntityStatsDto BuildEntityStats(
        IEnumerable<Participant> participants,
        int successfulPlacement)
    {
        var games = participants.Count();

        var wins = participants.Count(p => p.Placement == 1);

        var successfulPlacements = participants.Count(
            p => p.Placement <= successfulPlacement);

        return new EntityStatsDto
        {
            Games = games,

            Wins = wins,

            WinRate = (double)wins / games * 100,

            SuccessfulPlacements = successfulPlacements,

            SuccessfulPlacementRate =
                (double)successfulPlacements / games * 100,

            AveragePlacement =
                participants.Average(p => p.Placement)
        };
    }

    public async Task<EntityStatsDto> GetOverallStatsAsync(string puuid)
    {
        var successfulPlacement =
            _configuration.GetValue<int>("Arena:SuccessfulPlacement");

        var participants = await GetPlayerParticipants(puuid).ToListAsync();

        return BuildEntityStats(participants, successfulPlacement);
    }

    public async Task<List<ChampionStatsDto>> GetChampionStatsAsync(string puuid)
    {
        var successfulPlacement =
            _configuration.GetValue<int>("Arena:SuccessfulPlacement");

        var groups = await GetPlayerParticipants(puuid)
            .GroupBy(p => p.ChampionName)
            .ToListAsync();

        return groups
            .Select(g =>
            {
                var stats =
                    BuildEntityStats(g, successfulPlacement);

                return new ChampionStatsDto
                {
                    ChampionName = g.Key,

                    Games = stats.Games,
                    Wins = stats.Wins,
                    WinRate = stats.WinRate,
                    SuccessfulPlacements = stats.SuccessfulPlacements,
                    SuccessfulPlacementRate = stats.SuccessfulPlacementRate,
                    AveragePlacement = stats.AveragePlacement
                };
            })
            .OrderByDescending(c => c.Games)
            .ToList();
    }

    public async Task<List<DuoStatsDto>> GetDuoStatsAsync(string puuid)
    {

        var successfulPlacement =
                _configuration.GetValue<int>("Arena:SuccessfulPlacement");

        var groups = await (
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

            where me.Puuid == puuid
                && teammate.Puuid != puuid

            group me by new
            {
                teammate.Puuid,
                teammate.GameName,
                teammate.TagLine
            })
            .ToListAsync();

        return groups
            .Select(g =>
            {
                var stats = BuildEntityStats(g, successfulPlacement);

                return new DuoStatsDto
                {
                    GameName = g.Key.GameName,
                    TagLine = g.Key.TagLine,

                    Games = stats.Games,
                    Wins = stats.Wins,
                    WinRate = stats.WinRate,
                    SuccessfulPlacements = stats.SuccessfulPlacements,
                    SuccessfulPlacementRate = stats.SuccessfulPlacementRate,
                    AveragePlacement = stats.AveragePlacement
                };
            })
            .OrderByDescending(d => d.Games)
            .ToList();
    }
}