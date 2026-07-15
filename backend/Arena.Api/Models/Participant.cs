namespace Arena.Api.Models;

public class Participant
{
    public int Id { get; set; }

    public int MatchId { get; set; }

    public Match Match { get; set; } = null!;

    public required string Puuid { get; set; }

    public required string ChampionName { get; set; }

    public int Placement { get; set; }

    public int PlayerSubteamId { get; set; }
}