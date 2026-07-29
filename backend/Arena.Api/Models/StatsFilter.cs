namespace Arena.Api.Models;

public class StatsFilter
{
    public string? ChampionName { get; set; }

    public int[]? ItemIds { get; set; }

    public int[]? AugmentIds { get; set; }
}