namespace Arena.Api.Dtos;

public class PerformanceStatsDto
{
    public double AverageKills { get; set; }

    public double AverageDeaths { get; set; }

    public double AverageAssists { get; set; }

    public double AverageKda { get; set; }

    public double AverageDamageDealt { get; set; }

    public double AverageDamageTaken { get; set; }

    public double AverageHealing { get; set; }

    public double AverageShielding { get; set; }
}