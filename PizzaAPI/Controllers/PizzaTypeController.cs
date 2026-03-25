using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Helpers;
using PizzaAPI.Interfaces;
using PizzaAPI.Models;
using PizzaData.Models;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("pizza-type")]
    public class PizzaTypeController : Controller
    {
        private readonly IRepository<PizzaType> _pizzaTypeRepository;
        public PizzaTypeController(IRepository<PizzaType> pizzaTypeRepository)
        {
            _pizzaTypeRepository = pizzaTypeRepository;
        }

        // GET: /pizza-type
        [HttpGet()]
        public async Task<IActionResult> GetAllPizzaTypeAsync([FromHeader] PagingParameter pagingParameter)
        {
            var result = await _pizzaTypeRepository.GetAllAsync();
            if (result.Equals(null))
            {
                return NotFound("No Pizza Type available.");
            }
            var resultObject = PaginationHelper.GetPagedData(result, pagingParameter);

            return Ok(resultObject);
        }

        // GET /pizza-type/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPizzaTypeByIdAsync(string id)
        {
            if (Guid.TryParse(id, out Guid resultId))
            {
                var result = await _pizzaTypeRepository.GetByIdAsync(id);
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

        // POST: /pizza-type
        [HttpPost]
        public async Task<IActionResult> CreatePizzaTypeAsync(PizzaType body)
        {
            var result = await _pizzaTypeRepository.AddAsync(body);
            return Ok(result);

        }

        // PUT /pizza-type/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePizzaTypeAsync(string id, PizzaType pizza)
        {
            var result = await _pizzaTypeRepository.UpdateAsync(id, pizza);
            if (result.Equals(false))
            {
                return NotFound("Pizza type not found.");
            }
            return Ok($"Pizza type with ID:{id} successfully updated");
        }


        // DELETE /pizza-type/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizzaAsync(string id)
        {
            var response = await _pizzaTypeRepository.DeleteAsync(id);
            if (response.Equals(false))
            {
                return NotFound("Pizza type not found.");
            }
            return Ok($"Pizza type with ID:{id} successfully deleted");
        }
    }
}
