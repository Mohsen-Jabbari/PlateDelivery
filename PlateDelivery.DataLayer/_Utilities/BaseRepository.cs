using Microsoft.EntityFrameworkCore;
using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Repository;
using PlateDelivery.DataLayer.Context;
using System.Linq.Expressions;

namespace PlateDelivery.DataLayer._Utilities;
public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly PlateDeliveryDBContext Context;

    public BaseRepository(PlateDeliveryDBContext context)
    {
        Context = context;
    }

    void IBaseRepository<T>.Add(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public async Task AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
    }

    public async Task AddRange(ICollection<T> entities)
    {
        await Context.Set<T>().AddRangeAsync(entities);
    }

    public bool Exists(Expression<Func<T, bool>> expression)
    {
        return Context.Set<T>().Any(expression);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
    {
        return await Context.Set<T>().AnyAsync(expression);
    }

    public T? Get(long id)
    {
        return Context.Set<T>().FirstOrDefault(t => t.Id.Equals(id));
    }

    public virtual async Task<List<T>?> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public virtual List<T> GetAll()
    {
        return Context.Set<T>().ToList();
    }

    public virtual async Task<T?> GetAsync(long id)
    {
        return await Context.Set<T>().FirstOrDefaultAsync(t => t.Id.Equals(id));
    }

    public async Task<T?> GetTracking(long id)
    {
        return await Context.Set<T>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
    }

    public T? GetTrackingSync(long id)
    {
        return Context.Set<T>().AsTracking().FirstOrDefault(t => t.Id.Equals(id));
    }

    public async Task<int> Save()
    {
        return await Context.SaveChangesAsync();
    }

    public int SaveSync()
    {
        return Context.SaveChanges();
    }

    public void Update(T entity)
    {
        Context.Update(entity);
    }
}