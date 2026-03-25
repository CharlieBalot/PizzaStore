using Microsoft.EntityFrameworkCore;
using PizzaAPI.Interfaces;
using PizzaData.Models;

namespace PizzaAPI.Services
{
    public class OrdersService : IRepository<Orders>
    {
        private readonly PizzaDbContext _dbContext;

        public OrdersService(PizzaDbContext context)
        {
            _dbContext = context;
        }

        // Get all Orders
        public async Task<List<Orders>> GetAllAsync()
        {
            var result = await _dbContext.Orders.ToListAsync();
            if (result == null)
            {
                return new List<Orders>();
            }
            return result;
        }

        // Get Orders by ID
        public async Task<Orders?> GetByIdAsync(string id)
        {
            var result = await _dbContext.Orders.FindAsync(int.Parse(id));
            if (result == null)
            {
                return null;
            }
            return result;
        }

        // Add Orders
        public async Task<Orders> AddAsync(Orders order)
        {
            var data = new Orders
            {
                order_id = order.order_id,
                date = order.date,
                time = order.time
            };

            _dbContext.Orders.Add(data);
            await _dbContext.SaveChangesAsync();
            return data;
        }

        // Update Orders details
        public async Task<bool> UpdateAsync(string id, Orders order)
        {
            if (id != order.order_id.ToString())
            {
                return false;
            }

            try
            {
                _dbContext.Entry(order).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        // Delete Orders by ID
        public async Task<bool> DeleteAsync(string id)
        {
            var result = await this.GetByIdAsync(id);
            if (result?.order_id == null)
            {
                return false;
            }
            _dbContext.Orders.Remove(result);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
