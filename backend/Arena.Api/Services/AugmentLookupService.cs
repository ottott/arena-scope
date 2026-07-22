using Arena.Api.Models.CommunityDragon;
using System.Net.Http.Json;

namespace Arena.Api.Services;

public class AugmentLookupService
{
    private readonly HttpClient _httpClient;

    private readonly Dictionary<int, string> _augments = [];

    public AugmentLookupService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public string GetName(int augmentId)
    {
        return _augments.TryGetValue(augmentId, out var name)
            ? name
            : augmentId.ToString();
    }

    public async Task LoadAsync()
    {
        var response =
            await _httpClient.GetFromJsonAsync<CommunityDragonAugmentResponse>(
                "https://raw.communitydragon.org/latest/cdragon/arena/en_us.json");

        if (response == null)
        {
            return;
        }

        foreach (var augment in response.Augments)
        {
            _augments[augment.Id] = augment.Name;
        }
    }
}