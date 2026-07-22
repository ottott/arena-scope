using System.Text.Json.Serialization;

namespace Arena.Api.Models.CommunityDragon;

public class CommunityDragonAugmentResponse
{
    [JsonPropertyName("augments")]
    public List<CommunityDragonAugment> Augments { get; set; } = [];
}

public class CommunityDragonAugment
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
}