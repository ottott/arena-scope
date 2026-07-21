public class ArenaDataService
{
    private readonly HttpClient _httpClient;

    private readonly Dictionary<int, string> _items = [];

    private readonly Dictionary<int, string> _augments = [];

    public ArenaDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string GetItemName(int id)
    {
        return _items.TryGetValue(id, out var name)
            ? name
            : id.ToString();
    }

    public string GetAugmentName(int id)
    {
        return _augments.TryGetValue(id, out var name)
            ? name
            : id.ToString();
    }

}