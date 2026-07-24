public class MatchHistoryDto
{
    public DateTime GameCreation { get; set; }

    public int GameDuration { get; set; }

    public string ChampionName { get; set; } = null!;

    public int Placement { get; set; }

    public int Kills { get; set; }

    public int Deaths { get; set; }

    public int Assists { get; set; }
}