using Microsoft.EntityFrameworkCore;
using PizzaAPI.Interfaces;
using PizzaAPI.Models;
using PizzaData.Models;


namespace PizzaAPI.Services
{
    public class PizzaService : IRepository<Pizzas>
    {
        private readonly PizzaDbContext _dbContext;

        public PizzaService(PizzaDbContext context)
        {
            _dbContext = context;
        }

        // Get all Pizza
        public async Task<List<Pizzas>> GetAllAsync()
        {
            var result = await _dbContext.Pizzas.ToListAsync();
            if (result == null)
            {
                return new List<Pizzas>();
            }
            return result;
        }

        // Get Pizza by ID
        public async Task<Pizzas?> GetByIdAsync(string id)
        {
            var result = await _dbContext.Pizzas.FindAsync(id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        // Add Pizza
        public async Task<Pizzas> AddAsync(Pizzas pizza)
        {
            var data = new Pizzas
            {
                pizza_id = pizza.pizza_id,
                pizza_type_id = pizza.pizza_type_id,
                size = pizza.size,
                price = pizza.price
            };

            _dbContext.Pizzas.Add(data);
            await _dbContext.SaveChangesAsync();
            return data;
        }

        // Update Pizza details
        public async Task<bool> UpdateAsync(string id, Pizzas pizza)
        {
            if (id != pizza.pizza_id)
            {
                return false;
            }

            try
            {
                _dbContext.Entry(pizza).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        // Delete Pizza by ID
        public async Task<bool> DeleteAsync(string id)
        {
            var result = await this.GetByIdAsync(id);
            if (result?.pizza_id == null)
            {
                return false;
            }
            _dbContext.Pizzas.Remove(result);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // Get Best Seller Pizza
        public List<PizzaStatistics> GetBestSellerPizza()
        {
            var topPizzas = _dbContext.OrderDetails
            .GroupBy(od => od.pizza_id)
            .Select(g => new PizzaStatistics
            {
                PizzaId = g.Key,
                TotalSold = g.Sum(x => x.quantity)
            })
            .OrderByDescending(x => x.TotalSold)
            .Take(10)
            .ToList();

            return topPizzas.ToList();
        }

        // Get Least Popular Pizza
        public List<PizzaStatistics> GetLeastPopularPizza()
        {
            var topPizzas = _dbContext.OrderDetails
            .GroupBy(od => od.pizza_id)
            .Select(g => new PizzaStatistics
            {
                PizzaId = g.Key,
                TotalSold = g.Sum(x => x.quantity)
            })
            .OrderBy(x => x.TotalSold)
            .Take(10)
            .ToList();

            return topPizzas.ToList();
        }
    }
}
