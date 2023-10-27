using PlateDelivery.Common.CommonClasses;
using System.Linq.Expressions;

namespace PlateDelivery.Common.Repository;
public interface IBaseRepository<T> where T : BaseEntity
{
    Task<List<T>?> GetAllAsync();
    Task<T?> GetAsync(long id);
    Task<T?> GetTracking(long id);

    Task AddAsync(T entity);
    void Add(T entity);
    Task AddRange(ICollection<T> entities);
    void Update(T entity);

    Task<int> Save();

    Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

    bool Exists(Expression<Func<T, bool>> expression);

    T? Get(long id);
}