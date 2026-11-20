using Deposita.CashOperations.Domain.Common;

namespace Deposita.CashOperations.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T>
        where T : BaseEntity
    {
        /// <summary>Retrieves all entities.</summary>
        Task<IEnumerable<T>> GetAllAsync();
        /// <summary>Retrieves an entity by identifier.</summary>
        /// <param name="id">The entity identifier.</param>
        Task<T?> GetByIdAsync(int id);
        /// <summary>Adds an entity to the data context.</summary>
        /// <param name="entity">The entity to add.</param>
        Task AddAsync(T entity);
        /// <summary>Marks an entity as modified.</summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);
        /// <summary>Marks an entity for deletion.</summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);
        /// <summary>Persists pending changes.</summary>
        Task SaveChangesAsync();
    }
}
