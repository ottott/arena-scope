using Arena.Api.Data;
using Arena.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Arena.Api.Services;

public class MatchService : IMatchService
{
    private readonly RiotApiClient _riotApiClient;
    private readonly ArenaDbContext _dbContext;
    private readonly IConfiguration _configuration;
    public MatchService(RiotApiClient riotApiClient, ArenaDbContext dbContext, IConfiguration configuration)
    {
        _riotApiClient = riotApiClient;
        _dbContext = dbContext;
        _configuration = configuration;
        
    }

    public async Task<List<string>> GetMatchIdsAsync(
        string puuid,
        int start,
        int count,
        long? startTime)
    {
        return await _riotApiClient.GetMatchIdsAsync(
            puuid,
            start,
            count,
            startTime);
    }

    public async Task<ImportMatchResult> ImportMatchAsync(string matchId)
    {
        var riotMatch = await _riotApiClient.GetMatchAsync(matchId);

        var currentSeasonStart =
            _configuration.GetValue<DateTime>("Arena:CurrentSeasonStart");

        var gameCreation =
            DateTimeOffset
                .FromUnixTimeMilliseconds(riotMatch.Info.GameCreation)
                .UtcDateTime;

        if (gameCreation < currentSeasonStart)
        {
            return ImportMatchResult.TooOld;
        }

        if (riotMatch.Info.QueueId != 1750)
        {
            return ImportMatchResult.NotArena;
        }


        var match = new Match
        {
            RiotMatchId = riotMatch.Metadata.MatchId,
            GameVersion = riotMatch.Info.GameVersion,
            GameCreation = gameCreation,
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
                GameName = riotParticipant.RiotIdGameName,
                TagLine = riotParticipant.RiotIdTagline,

                ChampionName = riotParticipant.ChampionName,
                Placement = riotParticipant.Placement,
                PlayerSubteamId = riotParticipant.PlayerSubteamId
            };

            _dbContext.Participants.Add(participant);
        }

        await _dbContext.SaveChangesAsync();
                
        return ImportMatchResult.Imported;
    }

    public async Task SyncPlayerMatchesAsync(Player player)
    {
        var start = 0;
        const int count = 100;

        var syncStartedAt = DateTime.UtcNow;

        var currentSeasonStart =
            _configuration.GetValue<DateTime>("Arena:CurrentSeasonStart");

        DateTime? startDate = null;

        if (player.LastSyncedAt.HasValue)
        {
            startDate = player.LastSyncedAt.Value.AddMinutes(-40);

            if (startDate < currentSeasonStart)
            {
                startDate = currentSeasonStart;
            }
        }

        long? startTime = startDate == null
            ? null
            : new DateTimeOffset(startDate.Value).ToUnixTimeSeconds();

        while (true)
        {
            var matchIds = await GetMatchIdsAsync(
                player.Puuid,
                start,
                count,
                startTime);

            if (matchIds.Count == 0)
            {
                break;
            }

            foreach (var matchId in matchIds)
            {
                var exists = await _dbContext.Matches.AnyAsync(
                    m => m.RiotMatchId == matchId);

                if (exists)
                {
                    continue;
                }

                var result = await ImportMatchAsync(matchId);

                if (result == ImportMatchResult.TooOld)
                {
                    player.LastSyncedAt = syncStartedAt;

                    await _dbContext.SaveChangesAsync();

                    return;
                }

            }

            if (matchIds.Count < count)
            {
                break;
            }

            start += count;
        }
        
        player.LastSyncedAt = syncStartedAt;

        await _dbContext.SaveChangesAsync();

    }

}