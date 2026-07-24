using Arena.Api.Dtos;
using Arena.Api.Models;

namespace Arena.Api.Services;

public interface IMatchHistoryService
{
    Task<List<MatchHistoryDto>> GetMatchHistoryAsync(string puuid, StatsFilter? filter = null);
}