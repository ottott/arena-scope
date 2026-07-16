using System.Net;
using System.Net.Http.Json;
using Arena.Api.Models;
using Arena.Api.Models.Riot;


namespace Arena.Api.Services;

public class RiotApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public RiotApiClient(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    private async Task<HttpResponseMessage> GetWithRetryAsync(string url)
    {
        while (true)
        {
            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode != HttpStatusCode.TooManyRequests)
            {
                return response;
            }

            var retryAfter = response.Headers.RetryAfter?.Delta
                            ?? TimeSpan.FromSeconds(5);

            await Task.Delay(retryAfter);
        }
}

    public async Task<RiotAccountResponse> GetAccountAsync(
        string gameName,
        string tagLine)
    {
        var apiKey = _configuration["Riot:ApiKey"];

        _httpClient.DefaultRequestHeaders.Clear();

        _httpClient.DefaultRequestHeaders.Add(
            "X-Riot-Token",
            apiKey);

        var url =
            $"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}";

        var response = await _httpClient.GetAsync(url);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new Exception("Player not found.");
        }

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new Exception("Invalid Riot API key.");
        }

        if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            throw new Exception("Riot API rate limit exceeded.");
        }
        

        response.EnsureSuccessStatusCode();

        var account = await response.Content.ReadFromJsonAsync<RiotAccountResponse>();

        return account!;
    }

    public async Task<List<string>> GetMatchIdsAsync(
        string puuid,
        int start,
        int count,
        long? startTime)
    {
        var apiKey = _configuration["Riot:ApiKey"];

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);

        var url =
            $"https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids?start={start}&count={count}";

        
        if (startTime.HasValue)
        {
            url += $"&startTime={startTime}";
        }

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            throw new Exception(
                $"Riot API failed: {(int)response.StatusCode} - {error}");
        }

        var matches = await response.Content
            .ReadFromJsonAsync<List<string>>();

        return matches!;
    }

    public async Task<RiotMatchResponse> GetMatchAsync(string matchId)
    {
        var apiKey = _configuration["Riot:ApiKey"];

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);

        var url =
            $"https://europe.api.riotgames.com/lol/match/v5/matches/{matchId}";

        var response = await GetWithRetryAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            throw new Exception(
                $"Riot API failed: {(int)response.StatusCode} - {error}");
        }

        var match = await response.Content
            .ReadFromJsonAsync<RiotMatchResponse>();

        return match!;
    }

}