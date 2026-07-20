namespace Arena.Api.Dtos;

public class PlayerStatsDto
{
    public required string GameName { get; set; }

    public required string TagLine { get; set; }

    public int Games { get; set; }

    public int Wins { get; set; }

    public double AveragePlacement { get; set; }

    public int SuccessfulPlacements { get; set; }

    public double SuccessfulPlacementRate { get; set; }

    public List<ChampionStatsDto> ChampionStats { get; set; } = [];

    public List<DuoStatsDto> DuoStats { get; set; } = [];

    public List<ItemStatsDto> ItemStats { get; set; } = [];

    public List<AugmentStatsDto> AugmentStats { get; set; } = [];
}