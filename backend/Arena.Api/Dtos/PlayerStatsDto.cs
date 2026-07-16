namespace Arena.Api.Dtos;

public class PlayerStatsDto
{
    public required string GameName { get; set; }

    public required string TagLine { get; set; }

    public int Games { get; set; }

    public int Wins { get; set; }

    public double AveragePlacement { get; set; }
}