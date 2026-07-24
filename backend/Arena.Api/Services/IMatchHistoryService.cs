using Arena.Api.Dtos;

namespace Arena.Api.Services;

public interface IMatchHistoryService
{
    Task<List<MatchHistoryDto>> GetMatchHistoryAsync(string puuid);
}