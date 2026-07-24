using Arena.Api.Models;

namespace Arena.Api.Services;

public class ParticipantFilterService
{
    public IQueryable<Participant> ApplyFilter(
        IQueryable<Participant> query,
        StatsFilter filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.ChampionName))
        {
            query = query.Where(p =>
                p.ChampionName == filter.ChampionName);
        }

        if (filter.ItemId.HasValue)
        {
            query = query.Where(p =>
                p.Item0Id == filter.ItemId ||
                p.Item1Id == filter.ItemId ||
                p.Item2Id == filter.ItemId ||
                p.Item3Id == filter.ItemId ||
                p.Item4Id == filter.ItemId ||
                p.Item5Id == filter.ItemId);
        }

        if (filter.AugmentId.HasValue)
        {
            query = query.Where(p =>
                p.Augment1Id == filter.AugmentId ||
                p.Augment2Id == filter.AugmentId ||
                p.Augment3Id == filter.AugmentId ||
                p.Augment4Id == filter.AugmentId ||
                p.Augment5Id == filter.AugmentId ||
                p.Augment6Id == filter.AugmentId);
        }

        return query;
    }
}