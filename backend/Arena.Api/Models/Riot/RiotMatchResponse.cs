namespace Arena.Api.Models.Riot;

public class RiotMatchResponse
{
    public required RiotMatchMetadata Metadata { get; set; }

    public required RiotMatchInfo Info { get; set; }
}