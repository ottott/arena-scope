using Arena.Api.Data;
using Arena.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Arena.Api.Services;

public class MatchService : IMatchService
{
    private readonly RiotApiClient _riotApiClient;
    private readonly ArenaDbContext _dbContext;

    public MatchService(RiotApiClient riotApiClient, ArenaDbContext dbContext)
    {
        _riotApiClient = riotApiClient;
        _dbContext = dbContext;
    }

    public async Task<List<string>> GetMatchIdsAsync(
        string puuid,
        int count)
    {
        return await _riotApiClient.GetMatchIdsAsync(
            puuid,
            count);
    }

    public async Task ImportMatchAsync(string matchId)
    {
        var riotMatch = await _riotApiClient.GetMatchAsync(matchId);

        if (riotMatch.Info.QueueId != 1750)
        {
            throw new Exception("Only Arena matches can be imported.");
        }

        var exists = await _dbContext.Matches.AnyAsync(m =>
            m.RiotMatchId == riotMatch.Metadata.MatchId);

        if (exists)
        {
            return;
        }

        var match = new Match
        {
            RiotMatchId = riotMatch.Metadata.MatchId,
            GameVersion = riotMatch.Info.GameVersion,
            GameCreation = DateTimeOffset
                .FromUnixTimeMilliseconds(riotMatch.Info.GameCreation)
                .UtcDateTime,
            GameDuration = riotMatch.Info.GameDuration,
            GameMode = riotMatch.Info.GameMode,
            QueueId = riotMatch.Info.QueueId
        };

        _dbContext.Matches.Add(match);

        await _dbContext.SaveChangesAsync();

        foreach (var riotParticipant in riotMatch.Info.Participants)
        {
            var participant = new Participant
            {
                MatchId = match.Id,
                Puuid = riotParticipant.Puuid,
                ChampionName = riotParticipant.ChampionName,
                Placement = riotParticipant.Placement,
                PlayerSubteamId = riotParticipant.PlayerSubteamId
            };

            _dbContext.Participants.Add(participant);
        }

        await _dbContext.SaveChangesAsync();
    }
}