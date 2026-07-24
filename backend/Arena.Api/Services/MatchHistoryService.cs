using Arena.Api.Data;
using Arena.Api.Dtos;
using Arena.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Arena.Api.Services;

public class MatchHistoryService : IMatchHistoryService
{
    private readonly ArenaDbContext _dbContext;
    private readonly ParticipantFilterService _participantFilter;

    public MatchHistoryService(ArenaDbContext dbContext, ParticipantFilterService participantFilter)
    {
        _dbContext = dbContext;
        _participantFilter = participantFilter;
    }

    public async Task<List<MatchHistoryDto>> GetMatchHistoryAsync(
        string puuid,
        StatsFilter? filter)
    {
        filter ??= new StatsFilter();

        var query = _dbContext.Participants
            .Where(p => p.Puuid == puuid);

        query = _participantFilter.ApplyFilter(query, filter);

        return await query
            .OrderByDescending(p => p.Match.GameCreation)
            .Select(p => new MatchHistoryDto
            {
                GameCreation = p.Match.GameCreation,
                GameDuration = p.Match.GameDuration,

                ChampionName = p.ChampionName,
                Placement = p.Placement,

                Kills = p.Kills,
                Deaths = p.Deaths,
                Assists = p.Assists
            })
            .ToListAsync();
    }
}