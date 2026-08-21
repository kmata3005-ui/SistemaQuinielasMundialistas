using Microsoft.EntityFrameworkCore;
using SistemaQuinielaMundialistasV2.Data;

namespace SistemaQuinielaMundialistasV2.Repositories;

public sealed class EfRepository<T>(IDbContextFactory<AppDbContext> contextFactory) : IRepository<T>
    where T : class
{
    public async Task<List<T>> GetAllAsync()
    {
        await using AppDbContext context = await contextFactory.CreateDbContextAsync();
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        await using AppDbContext context = await contextFactory.CreateDbContextAsync();
        return await context.Set<T>().FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await using AppDbContext context = await contextFactory.CreateDbContextAsync();
        context.Set<T>().Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        await using AppDbContext context = await contextFactory.CreateDbContextAsync();
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        await using AppDbContext context = await contextFactory.CreateDbContextAsync();
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public Task SaveChangesAsync() => Task.CompletedTask;
}
