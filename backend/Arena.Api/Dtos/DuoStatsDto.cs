namespace Arena.Api.Dtos;

public class DuoStatsDto : EntityStatsDto
{
    public required string GameName { get; set; }

    public required string TagLine { get; set; }
}