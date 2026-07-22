namespace Arena.Api.Dtos;

public class PlayerStatsDto : EntityStatsDto
{
    public required string GameName { get; set; }

    public required string TagLine { get; set; }

    public List<ChampionStatsDto> ChampionStats { get; set; } = [];

    public List<DuoStatsDto> DuoStats { get; set; } = [];

    public List<ItemStatsDto> ItemStats { get; set; } = [];

    public List<AugmentStatsDto> AugmentStats { get; set; } = [];
}