using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PizzaAPI.Helpers;
using PizzaAPI.Interfaces;
using PizzaAPI.Models;
using PizzaData.Models;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("order-details")]
    public class OrdersDetailsController : Controller
    {
        private readonly IRepository<OrderDetails> _orderDetailsRepository;
        public OrdersDetailsController(IRepository<OrderDetails> orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        // GET: /order-details
        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetailsAsync([FromHeader]PagingParameter pagingParameter)
        {
            var result = await _orderDetailsRepository.GetAllAsync();
            if (result.Equals(null))
            {
                return NotFound("No Order Details available.");
            }
            var resultObject = PaginationHelper.GetPagedData(result, pagingParameter);
            
            return Ok(resultObject);
        }

        // GET /order-details/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailsByIdAsync(string id)
        {
            if (int.TryParse(id, out int resultId))
            {
                var result = await _orderDetailsRepository.GetByIdAsync(resultId.ToString());
                if (result == null)
                {
                    return NotFound("Order Details not found.");
                }
                return Ok(result);
            }
            else
            {
                return NotFound("Id is not valid");
            }
        }

        // POST: /order-details
        [HttpPost]
        public async Task<IActionResult> CreateOrderDetailsAsync(OrderDetails body)
        {
            var result = await _orderDetailsRepository.AddAsync(body);
            return Ok(result);

        }

        // PUT /order-details/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetailsAsync(string id, OrderDetails orderDetails)
        {
            var result = await _orderDetailsRepository.UpdateAsync(id, orderDetails);
            if (result.Equals(false))
            {
                return NotFound("Order Details not found.");
            }
            return Ok($"Order Details with ID:{id} successfully updated");
        }


        // DELETE /order-details/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetailsAsync(string id)
        {
            var response = await _orderDetailsRepository.DeleteAsync(id);
            if (response.Equals(false))
            {
                return NotFound("Order Details not found.");
            }
            return Ok($"Order Details with ID:{id} successfully deleted");
        }
    }
}
