using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Helpers;
using PizzaAPI.Services;
using PizzaData.Models;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("statistics")]
    public class StatisticsController : Controller
    {
        PizzaService pizzaService = new PizzaService(new PizzaDbContext());

        // GET: /statistics/get-best-seller-pizza
        [Route("get-best-seller-pizza")]
        [HttpGet]
        public IActionResult GetBestSellerPizza()
        {
            var result =  pizzaService.GetBestSellerPizza();
            if (result.Equals(null))
            {
                return NotFound("No Best Seller Pizza available");
            }
            return Ok(result);
        }

        // GET: /statistics/get-lease-popular-pizza
        [Route("get-least-popular-pizza")]
        [HttpGet]
        public IActionResult GetLeastPopularPizza()
        {
            var result = pizzaService.GetLeastPopularPizza();
            if (result.Equals(null))
            {
                return NotFound("No Least Popular Pizza available");
            }
            return Ok(result);
        }
    }
}
