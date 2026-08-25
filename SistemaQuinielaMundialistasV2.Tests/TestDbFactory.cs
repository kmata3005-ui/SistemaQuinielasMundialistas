using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;

namespace SistemaQuinielaMundialistasV2.Tests;

internal sealed class TestDbFactory : IDbContextFactory<AppDbContext>
{
    private readonly DbContextOptions<AppDbContext> _options;
    public TestDbFactory(string? name = null)
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(name ?? Guid.NewGuid().ToString())
            .Options;
    }
    public AppDbContext CreateDbContext() => new(_options);
}
