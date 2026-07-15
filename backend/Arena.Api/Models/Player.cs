namespace Arena.Api.Models;

public class Player
{
    public int Id { get; set; }

    public required string GameName { get; set; }

    public required string TagLine { get; set; }

    public required string Puuid { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}