using Microsoft.EntityFrameworkCore;
using PizzaAPI.Interfaces;
using PizzaData.Models;

namespace PizzaAPI.Services
{
    public class OrderDetailsService : IRepository<OrderDetails>
    {
        private readonly PizzaDbContext _dbContext;

        public OrderDetailsService(PizzaDbContext context)
        {
            _dbContext = context;
        }

        // Get all Order Details
        public async Task<List<OrderDetails>> GetAllAsync()
        {
            var result = await _dbContext.OrderDetails.ToListAsync();
            if (result == null)
            {
                return new List<OrderDetails>();
            }
            return result;
        }

        // Get Order Details by ID
        public async Task<OrderDetails?> GetByIdAsync(string id)
        {
            var result = await _dbContext.OrderDetails.FindAsync(int.Parse(id));
            if (result == null)
            {
                return null;
            }
            return result;
        }

        // Add Order Details
        public async Task<OrderDetails> AddAsync(OrderDetails orderDetails)
        {
            var data = new OrderDetails
            {
                order_details_id = orderDetails.order_details_id,
                order_id = orderDetails.order_id,
                pizza_id = orderDetails.pizza_id,
                quantity = orderDetails.quantity
            };

            _dbContext.OrderDetails.Add(data);
            await _dbContext.SaveChangesAsync();
            return data;
        }

        // Update Orders details
        public async Task<bool> UpdateAsync(string id, OrderDetails orderDetails)
        {
            if (id != orderDetails.order_id.ToString())
            {
                return false;
            }

            try
            {
                _dbContext.Entry(orderDetails).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        // Delete Order Details by ID
        public async Task<bool> DeleteAsync(string id)
        {
            var result = await this.GetByIdAsync(id);
            if (result?.order_details_id == null)
            {
                return false;
            }
            _dbContext.OrderDetails.Remove(result);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // Get Avarage Statistics
        public void GetAverageStatistics()
        {
            var average = _dbContext.OrderDetails.Average(x => x.quantity);
            Console.WriteLine($"Average Quantity: {average}");
        }
    }
}
