namespace Arena.Api.Services;

public interface IMatchService
{
    Task<List<string>> GetMatchIdsAsync(string puuid, int count);

    Task ImportMatchAsync(string matchId);
}