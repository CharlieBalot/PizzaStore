using Microsoft.EntityFrameworkCore;
using PizzaAPI.Interfaces;
using PizzaData.Models;

namespace PizzaAPI.Services
{
    public class PizzaTypeService : IRepository<PizzaType>
    {
        private readonly PizzaDbContext _dbContext;

        public PizzaTypeService(PizzaDbContext context)
        {
            _dbContext = context;
        }

        // Get all Pizza Type
        public async Task<List<PizzaType>> GetAllAsync()
        {
            var result = await _dbContext.PizzaTypes.ToListAsync();
            if (result == null)
            {
                return new List<PizzaType>();
            }
            return result;
        }

        // Get Pizza Type by ID
        public async Task<PizzaType?> GetByIdAsync(string id)
        {
            var result = await _dbContext.PizzaTypes.FindAsync(id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        // Add Pizza Type
        public async Task<PizzaType> AddAsync(PizzaType pizzaType)
        {
            var data = new PizzaType
            {
                pizza_type_id = pizzaType.pizza_type_id,
                name = pizzaType.name,
                category = pizzaType.category,
                ingredients = pizzaType.ingredients
            };

            _dbContext.PizzaTypes.Add(data);
            await _dbContext.SaveChangesAsync();
            return data;
        }

        // Update Pizza Type
        public async Task<bool> UpdateAsync(string id, PizzaType pizzaType)
        {
            if (id != pizzaType.pizza_type_id)
            {
                return false;
            }

            try
            {
                _dbContext.Entry(pizzaType).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        // Delete Pizza Type by ID
        public async Task<bool> DeleteAsync(string id)
        {
            var result = await this.GetByIdAsync(id);
            if (result?.pizza_type_id == null)
            {
                return false;
            }
            _dbContext.PizzaTypes.Remove(result);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
