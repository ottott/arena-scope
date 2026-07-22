namespace Arena.Api.Dtos;

public class AugmentStatsDto : EntityStatsDto
{
    public int AugmentId { get; set; }
        
    public string AugmentName { get; set; } = "";
}