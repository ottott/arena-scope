namespace Arena.Api.Models.Riot;

public class RiotMatchInfo
{
    public required string GameVersion { get; set; }

    public long GameCreation { get; set; }

    public required string GameMode { get; set; }

    public int QueueId { get; set; }

    public int GameDuration { get; set; }

    public required List<RiotParticipant> Participants { get; set; }
}