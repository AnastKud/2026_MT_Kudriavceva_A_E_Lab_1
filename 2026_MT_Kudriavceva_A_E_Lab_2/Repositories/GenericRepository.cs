namespace Repositories;

using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

public class GenericRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
{
    private readonly ApplicationDbContext context;
    private readonly DbSet<TEntity> dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id) => await this.dbSet.FindAsync(id).ConfigureAwait(false);

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await this.dbSet.ToListAsync().ConfigureAwait(false);

    public async Task AddAsync(TEntity entity)
    {
        await this.dbSet.AddAsync(entity).ConfigureAwait(false);
        await this.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        this.dbSet.Update(entity);
        await this.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await this.GetByIdAsync(id).ConfigureAwait(false);
        if (entity != null)
        {
            this.dbSet.Remove(entity);
            await this.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    public async Task SaveChangesAsync() => await this.context.SaveChangesAsync().ConfigureAwait(false);
}