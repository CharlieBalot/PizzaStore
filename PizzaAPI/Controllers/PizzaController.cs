using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Helpers;
using PizzaAPI.Interfaces;
using PizzaAPI.Models;
using PizzaData.Models;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("pizza")]
    public class PizzaController : Controller
    {
        private readonly IRepository<Pizzas> _pizzaRepository;
        public PizzaController(IRepository<Pizzas> pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        // GET: /pizzas
        [HttpGet()]
        public async Task<IActionResult> GetAllPizzaAsync([FromHeader] PagingParameter pagingParameter)
        {
            var result = await _pizzaRepository.GetAllAsync();
            if (result.Equals(null))
            {
                return NotFound("No Order Details available.");
            }
            var resultObject = PaginationHelper.GetPagedData(result, pagingParameter);

            return Ok(resultObject);
        }

        // GET /pizza/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPizzaByIdAsync(string id)
        {
            if (Guid.TryParse(id, out Guid resultId))
            {
                var result = await _pizzaRepository.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound("Pizza not found.");
                }
                return Ok(result);
            }
            else
            {
                return NotFound("Id is not valid");
            }
        }

        // POST: /pizza
        [HttpPost]
        public async Task<IActionResult> CreatePizzaAsync(Pizzas body)
        {
            var result = await _pizzaRepository.AddAsync(body);
            return Ok(result);

        }

        // PUT /pizza/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePizzaAsync(string id, Pizzas pizza)
        {
            var result = await _pizzaRepository.UpdateAsync(id, pizza);
            if (result.Equals(false))
            {
                return NotFound("Pizza not found.");
            }
            return Ok($"Pizza with ID:{id} successfully updated");
        }


        // DELETE /pizza/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizzaAsync(string id)
        {
            var response = await _pizzaRepository.DeleteAsync(id);
            if (response.Equals(false))
            {
                return NotFound("Pizza not found.");
            }
            return Ok($"Pizza with ID:{id} successfully deleted");
        }
    }
}
