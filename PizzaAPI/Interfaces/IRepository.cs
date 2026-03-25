namespace PizzaAPI.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Get all entities
        Task<List<T>> GetAllAsync();
        // Get entity by ID
        Task<T?> GetByIdAsync(string id);
        // Add a new entity
        Task<T> AddAsync(T entity);
        // Update an existing entity
        Task<bool> UpdateAsync(string id, T entity);
        // Delete an entity by ID
        Task<bool> DeleteAsync(string id);
    }
}
