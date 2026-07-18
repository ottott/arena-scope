namespace Arena.Api.Dtos;

public class DuoStatsDto
{
    public required string GameName { get; set; }

    public required string TagLine { get; set; }

    public int Games { get; set; }

    public int Wins { get; set; }

    public double WinRate { get; set; }

    public int SuccessfulPlacements { get; set; }

    public double SuccessfulPlacementRate { get; set; }

    public double AveragePlacement { get; set; }
}