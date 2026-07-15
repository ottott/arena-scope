using Microsoft.EntityFrameworkCore;
using Arena.Api.Models;

namespace Arena.Api.Data;

public class ArenaDbContext : DbContext
{
    public ArenaDbContext(DbContextOptions<ArenaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players => Set<Player>();

    public DbSet<Match> Matches => Set<Match>();

    public DbSet<Participant> Participants => Set<Participant>();
}