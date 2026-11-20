using Deposita.CashOperations.Application.Interfaces.Repositories;
using Deposita.CashOperations.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace Deposita.CashOperations.Persistence.Repositories
{
    public class GenericRepository<T>
        : IGenericRepository<T>
        where T : Domain.Common.BaseEntity
            {
        protected readonly ApplicationDbContext Context;

        protected readonly DbSet<T> DbSet;

        public GenericRepository(
            ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        /// <summary>
        /// Retrieves all entities of the configured type.
        /// </summary>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }
        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        public async Task<T?> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
        /// <summary>
        /// Adds an entity to the data context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }
        /// <summary>
        /// Marks an entity as modified in the data context.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
        /// <summary>
        /// Marks an entity for deletion from the data context.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }
        /// <summary>
        /// Persists pending changes to the database.
        /// </summary>
        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
