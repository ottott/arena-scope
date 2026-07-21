using Arena.Api.Models.DataDragon;

namespace Arena.Api.Services;

public class ItemLookupService
{
    private readonly HttpClient _httpClient;

    private readonly Dictionary<int, string> _items = [];

    public string GetName(int itemId)
    {
        return _items.TryGetValue(itemId, out var name)
            ? name
            : itemId.ToString();
    }

    public async Task LoadAsync()
    {
        var response =
            await _httpClient.GetFromJsonAsync<DataDragonItemResponse>(
                "https://ddragon.leagueoflegends.com/cdn/15.15.1/data/en_US/item.json");

        if (response == null)
        {
            return;
        }

        foreach (var item in response.Data)
        {
            _items[int.Parse(item.Key)] = item.Value.Name;
        }

    }

    public ItemLookupService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}