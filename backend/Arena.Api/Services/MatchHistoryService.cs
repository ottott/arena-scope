using Arena.Api.Data;
using Arena.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Arena.Api.Services;

public class MatchHistoryService : IMatchHistoryService
{
    private readonly ArenaDbContext _dbContext;

    public MatchHistoryService(ArenaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<MatchHistoryDto>> GetMatchHistoryAsync(string puuid)
    {
        return await _dbContext.Participants
            .Where(p => p.Puuid == puuid)
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