using System.Text.Json.Serialization;

namespace Arena.Api.Models.DataDragon;

public class DataDragonItemResponse
{
    [JsonPropertyName("data")]
    public Dictionary<string, DataDragonItem> Data { get; set; } = [];
}

public class DataDragonItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
}