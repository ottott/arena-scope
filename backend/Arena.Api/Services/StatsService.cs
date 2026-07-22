using Arena.Api.Data;
using Arena.Api.Models;
using Arena.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Arena.Api.Services;

public class StatsService
{
    private readonly ArenaDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ItemLookupService _itemLookup;
    private readonly AugmentLookupService _augmentLookup;

    public StatsService(
        ArenaDbContext context,
        IConfiguration configuration,
        ItemLookupService itemLookup,
        AugmentLookupService augmentLookup)
    {
        _context = context;
        _configuration = configuration;
        _itemLookup = itemLookup;
        _augmentLookup = augmentLookup;
    }

    private record ItemParticipant(
        Participant Participant,
        int ItemId);

    private record AugmentParticipant(
        Participant Participant,
        int AugmentId);

    private IEnumerable<ItemParticipant> FlattenItems(
        IEnumerable<Participant> participants)
    {
        foreach (var participant in participants)
        {
            if (participant.Item0Id != 0)
                yield return new ItemParticipant(participant, participant.Item0Id);

            if (participant.Item1Id != 0)
                yield return new ItemParticipant(participant, participant.Item1Id);

            if (participant.Item2Id != 0)
                yield return new ItemParticipant(participant, participant.Item2Id);

            if (participant.Item3Id != 0)
                yield return new ItemParticipant(participant, participant.Item3Id);

            if (participant.Item4Id != 0)
                yield return new ItemParticipant(participant, participant.Item4Id);

            if (participant.Item5Id != 0)
                yield return new ItemParticipant(participant, participant.Item5Id);
        }
    }

    private IEnumerable<AugmentParticipant> FlattenAugments(
        IEnumerable<Participant> participants)
    {
        foreach (var participant in participants)
        {
            if (participant.Augment1Id != 0)
                yield return new(participant, participant.Augment1Id);

            if (participant.Augment2Id != 0)
                yield return new(participant, participant.Augment2Id);

            if (participant.Augment3Id != 0)
                yield return new(participant, participant.Augment3Id);

            if (participant.Augment4Id != 0)
                yield return new(participant, participant.Augment4Id);

            if (participant.Augment5Id != 0)
                yield return new(participant, participant.Augment5Id);

            if (participant.Augment6Id != 0)
                yield return new(participant, participant.Augment6Id);
        }
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
            .Where(x => x.Games >= 5)
            .OrderBy(x => x.AveragePlacement)
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
            .Where(x => x.Games >= 5)
            .OrderBy(x => x.AveragePlacement)
            .ToList();
    }

    public async Task<List<ItemStatsDto>> GetItemStatsAsync(string puuid)
    {
        var successfulPlacement =
            _configuration.GetValue<int>("Arena:SuccessfulPlacement");

        var participants =
            await GetPlayerParticipants(puuid).ToListAsync();

        var groups = FlattenItems(participants)
            .GroupBy(x => x.ItemId);

        return groups
            .Select(g =>
            {
                var stats = BuildEntityStats(
                    g.Select(x => x.Participant),
                    successfulPlacement);

                return new ItemStatsDto
                {
                    ItemId = g.Key,
                    ItemName = _itemLookup.GetName(g.Key),

                    Games = stats.Games,
                    Wins = stats.Wins,
                    WinRate = stats.WinRate,
                    SuccessfulPlacements = stats.SuccessfulPlacements,
                    SuccessfulPlacementRate = stats.SuccessfulPlacementRate,
                    AveragePlacement = stats.AveragePlacement
                };
            })
            .Where(x => x.Games >= 5)
            .OrderBy(x => x.AveragePlacement)
            .ToList();

    }

    public async Task<List<AugmentStatsDto>> GetAugmentStatsAsync(string puuid)
    {
        var successfulPlacement =
            _configuration.GetValue<int>("Arena:SuccessfulPlacement");

        var participants =
            await GetPlayerParticipants(puuid).ToListAsync();

        var groups = FlattenAugments(participants)
            .GroupBy(x => x.AugmentId);

        return groups
            .Select(g =>
            {
                var stats = BuildEntityStats(
                    g.Select(x => x.Participant),
                    successfulPlacement);

                return new AugmentStatsDto
                {
                    AugmentId = g.Key,
                    AugmentName = _augmentLookup.GetName(g.Key),

                    Games = stats.Games,
                    Wins = stats.Wins,
                    WinRate = stats.WinRate,
                    SuccessfulPlacements = stats.SuccessfulPlacements,
                    SuccessfulPlacementRate = stats.SuccessfulPlacementRate,
                    AveragePlacement = stats.AveragePlacement
                };
            })
            .Where(x => x.Games >= 5)
            .OrderBy(x => x.AveragePlacement)
            .ToList();
    }

    public async Task<List<PlacementDistributionDto>> GetPlacementDistributionAsync(string puuid)
    {
        return await GetPlayerParticipants(puuid)
            .GroupBy(p => p.Placement)
            .Select(g => new PlacementDistributionDto
            {
                Placement = g.Key,
                Games = g.Count()
            })
            .OrderBy(x => x.Placement)
            .ToListAsync();
    }


    public async Task<PerformanceStatsDto> GetPerformanceStatsAsync(string puuid)
    {
        var participants = await _context.Participants
            .Where(p => p.Puuid == puuid)
            .ToListAsync();

        if (participants.Count == 0)
        {
            return new PerformanceStatsDto();
        }

        var averageDeaths = participants.Average(p => p.Deaths);

        return new PerformanceStatsDto
        {
            AverageKills = participants.Average(p => p.Kills),

            AverageDeaths = averageDeaths,

            AverageAssists = participants.Average(p => p.Assists),

            AverageKda = averageDeaths == 0
                ? participants.Average(p => p.Kills + p.Assists)
                : participants.Average(p => (double)(p.Kills + p.Assists) / p.Deaths),

            AverageDamageDealt = participants.Average(p => p.DamageDealt),

            AverageDamageTaken = participants.Average(p => p.DamageTaken),

            AverageHealing = participants.Average(p => p.Healing),

            AverageShielding = participants.Average(p => p.Shielding)
        };
    }

    public async Task<List<TeamChampionStatsDto>> GetTeamChampionStatsAsync(string puuid)
    {
        var successfulPlacement =
            _configuration.GetValue<int>("Arena:SuccessfulPlacement");

        var stats = await _context.Participants

            // Find YOUR participant rows
            .Where(p => p.Puuid == puuid)

            // For each of your games...
            .SelectMany(player =>
                _context.Participants

                    // ...find your teammates
                    .Where(teammate =>
                        teammate.MatchId == player.MatchId &&
                        teammate.PlayerSubteamId == player.PlayerSubteamId &&
                        teammate.Puuid != player.Puuid)

                    // Project teammate champion + YOUR result
                    .Select(teammate => new
                    {
                        teammate.ChampionName,

                        player.Placement
                    }))

            // Group by teammate champion
            .GroupBy(x => x.ChampionName)

            .Select(g => new TeamChampionStatsDto
            {
                ChampionName = g.Key,

                Games = g.Count(),

                Wins = g.Count(x => x.Placement == 1),

                WinRate =
                    (double)g.Count(x => x.Placement == 1)
                    / g.Count() * 100,

                SuccessfulPlacements =
                    g.Count(x => x.Placement <= successfulPlacement),

                SuccessfulPlacementRate =
                    (double)g.Count(x => x.Placement <= successfulPlacement)
                    / g.Count() * 100,

                AveragePlacement =
                    g.Average(x => x.Placement)
            })

            .Where(x => x.Games >= 5)
            .OrderBy(x => x.AveragePlacement)

            .ToListAsync();

        return stats;
    }

}