namespace Arena.Api.Models;

public class Participant
{
    public int Id { get; set; }

    public int MatchId { get; set; }

    public Match Match { get; set; } = null!;

    public required string Puuid { get; set; }

    public required string GameName { get; set; }

    public required string TagLine { get; set; }

    public required string ChampionName { get; set; }

    public int Placement { get; set; }

    public int PlayerSubteamId { get; set; }

    public int Kills { get; set; }

    public int Deaths { get; set; }

    public int Assists { get; set; }

    public int DamageDealt { get; set; }

    public int DamageTaken { get; set; }

    public int Healing { get; set; }

    public int Shielding { get; set; }
    
    public int Item0Id { get; set; }
    public int Item1Id { get; set; }
    public int Item2Id { get; set; }
    public int Item3Id { get; set; }
    public int Item4Id { get; set; }
    public int Item5Id { get; set; }

    public int Augment1Id { get; set; }
    public int Augment2Id { get; set; }
    public int Augment3Id { get; set; }
    public int Augment4Id { get; set; }
    public int Augment5Id { get; set; }
    public int Augment6Id { get; set; }


}