using Arena.Api.Models;

namespace Arena.Api.Services;

public class ParticipantFilterService
{
    public IQueryable<Participant> ApplyFilter(
        IQueryable<Participant> query,
        StatsFilter filter)
    {
        if (filter.ChampionName != null &&
            filter.ChampionName.Length > 0)
        {
            query = query.Where(p =>
                filter.ChampionName.Contains(p.ChampionName));
        }


        if (filter.ItemIds != null &&
            filter.ItemIds.Length > 0)
        {
            foreach (var itemId in filter.ItemIds)
            {
                query = query.Where(p =>
                    p.Item0Id == itemId ||
                    p.Item1Id == itemId ||
                    p.Item2Id == itemId ||
                    p.Item3Id == itemId ||
                    p.Item4Id == itemId ||
                    p.Item5Id == itemId);
            }
        }


        if (filter.AugmentIds != null &&
            filter.AugmentIds.Length > 0)
        {
            foreach (var augmentId in filter.AugmentIds)
            {
                query = query.Where(p =>
                    p.Augment1Id == augmentId ||
                    p.Augment2Id == augmentId ||
                    p.Augment3Id == augmentId ||
                    p.Augment4Id == augmentId ||
                    p.Augment5Id == augmentId ||
                    p.Augment6Id == augmentId);
            }
        }


        return query;
    }
}