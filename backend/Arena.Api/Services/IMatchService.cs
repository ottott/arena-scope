using Arena.Api.Models;

namespace Arena.Api.Services;

public interface IMatchService
{
    Task<List<string>> GetMatchIdsAsync(string puuid, int start, int count, long? startTime);
    Task<ImportMatchResult> ImportMatchAsync(string matchId);
    Task SyncPlayerMatchesAsync(Player player);
}