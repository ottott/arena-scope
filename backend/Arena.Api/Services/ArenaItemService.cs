using Arena.Api.Data;
using Arena.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Arena.Api.Services;

public class ArenaItemService
{
    private readonly ArenaDbContext _context;
    private readonly ItemLookupService _itemLookupService;

    public ArenaItemService(
        ArenaDbContext context,
        ItemLookupService itemLookupService)
    {
        _context = context;
        _itemLookupService = itemLookupService;
    }

    public async Task<List<ItemDto>> GetArenaItemsAsync()
    {
        var itemIds = await _context.Participants
            .Select(p => p.Item0Id)
            .Union(_context.Participants.Select(p => p.Item1Id))
            .Union(_context.Participants.Select(p => p.Item2Id))
            .Union(_context.Participants.Select(p => p.Item3Id))
            .Union(_context.Participants.Select(p => p.Item4Id))
            .Union(_context.Participants.Select(p => p.Item5Id))
            .Where(id => id != 0)
            .Distinct()
            .ToListAsync();

        return itemIds
            .Select(id => new ItemDto
            {
                Id = id,
                Name = _itemLookupService.GetName(id)
            })
            .OrderBy(i => i.Name)
            .ToList();
    }
}