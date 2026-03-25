using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Helpers;
using PizzaAPI.Interfaces;
using PizzaAPI.Models;
using PizzaData.Models;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : Controller
    {
        private readonly IRepository<Orders> _ordersRepository;
        public OrdersController(IRepository<Orders> ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        // GET: /orders
        [HttpGet()]
        public async Task<IActionResult> GetAllOrdersAsync([FromHeader] PagingParameter pagingParameter)
        {
            var result = await _ordersRepository.GetAllAsync();
            if (result.Equals(null))
            {
                return NotFound("No Orders available.");
            }
            var resultObject = PaginationHelper.GetPagedData(result, pagingParameter);

            return Ok(resultObject);
        }

        // GET /orders/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdersByIdAsync(string id)
        {
            if (int.TryParse(id, out int resultId))
            {
                var result = await _ordersRepository.GetByIdAsync(resultId.ToString());
                if (result == null)
                {
                    return NotFound("Orders not found.");
                }
                return Ok(result);
            }
            else
            {
                return NotFound("Id is not valid");
            }
        }

        // POST: /orders
        [HttpPost]
        public async Task<IActionResult> CreateOrdersAsync(Orders body)
        {
            var result = await _ordersRepository.AddAsync(body);
            return Ok(result);

        }

        // PUT /orders/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrdersAsync(string id, Orders orders)
        {
            var result = await _ordersRepository.UpdateAsync(id, orders);
            if (result.Equals(false))
            {
                return NotFound("Orders not found.");
            }
            return Ok($"Orders with ID:{id} successfully updated");
        }


        // DELETE /orders/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdersAsync(string id)
        {
            var response = await _ordersRepository.DeleteAsync(id);
            if (response.Equals(false))
            {
                return NotFound("Orders not found.");
            }
            return Ok($"Orders with ID:{id} successfully deleted");
        }
    }
}
