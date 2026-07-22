namespace Arena.Api.Models.Riot;

public class RiotParticipant
{
    public required string Puuid { get; set; }

    public required string ChampionName { get; set; }

    public int Placement { get; set; }

    public int PlayerSubteamId { get; set; }

    public string RiotIdGameName { get; set; } = "";

    public string RiotIdTagline { get; set; } = "";

    public int Kills { get; set; }
    public int Deaths { get; set; }
    public int Assists { get; set; }

    public int TotalDamageDealtToChampions { get; set; }
    public int TotalDamageTaken { get; set; }
    public int TotalHeal { get; set; }
    public int TotalDamageShieldedOnTeammates { get; set; }

    public int Item0 { get; set; }
    public int Item1 { get; set; }
    public int Item2 { get; set; }
    public int Item3 { get; set; }
    public int Item4 { get; set; }
    public int Item5 { get; set; }

    public int PlayerAugment1 { get; set; }
    public int PlayerAugment2 { get; set; }
    public int PlayerAugment3 { get; set; }
    public int PlayerAugment4 { get; set; }
    public int PlayerAugment5 { get; set; }
    public int PlayerAugment6 { get; set; }
}