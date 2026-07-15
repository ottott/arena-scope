namespace Arena.Api.Models;

public class Match
{
    public int Id { get; set; }

    public required string RiotMatchId { get; set; }

    public required string GameVersion { get; set; }

    public DateTime GameCreation { get; set; }

    public int GameDuration { get; set; }

    public required string GameMode { get; set; }

    public int QueueId { get; set; }
}