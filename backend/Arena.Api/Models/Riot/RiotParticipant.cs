namespace Arena.Api.Models.Riot;

public class RiotParticipant
{
    public required string Puuid { get; set; }

    public required string ChampionName { get; set; }

    public int Placement { get; set; }

    public int PlayerSubteamId { get; set; }

    public string RiotIdGameName { get; set; } = "";

    public string RiotIdTagline { get; set; } = "";
}