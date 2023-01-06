using DapperLearningAPI.Models;
using DapperLearningAPI.Services.Data;
using DapperLearningAPI.Services.Factory;
using Microsoft.AspNetCore.Mvc;

namespace DapperLearningAPI.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        private readonly IDataServiceFactory _factory;
        protected ApiController(IDataServiceFactory factory)
        {
            _factory = factory;
        }
        [HttpGet]
        protected async Task<IActionResult> GetAllAsync<T>() where T : BaseModel
        {
            try
            {
                var results = await _factory.GetDataService<T>().GetAllAsync<T>();
                return Ok(results);
            }
            catch (InvalidCastException cex)
            {
                return BadRequest(cex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Unexpected error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        protected async Task<IActionResult> GetAsync<T>(int id) where T : BaseModel
        {
            try
            {
                var result = await _factory.GetDataService<T>().GetAsync<T>(id);
                if (result is null)
                {
                    return NotFound($"Unable to find {nameof(T)} with ID {id}");
                }
                return Ok(result);
            }
            catch (InvalidCastException cex)
            {
                return BadRequest(cex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Unexpected error: {ex.Message}");
            }
        }
        [HttpPost]
        protected async Task<IActionResult> InsertAsync<T>(T model) where T : BaseModel
        {
            try
            {
                var inserted = await _factory.GetDataService<T>().InsertAsync<T>(model) as ModelWithID;
                return CreatedAtAction("Get", new { ID = inserted.Id }, inserted);
            }
            catch (InvalidCastException cex)
            {
                return BadRequest(cex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Unexpected error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        protected async Task<IActionResult> UpdateAsync<T>(int id, T model) where T : BaseModel
        {

            try
            {
                await _factory.GetDataService<T>().UpdateAsync<T>(id, model);
                return NoContent();
            }
            catch (InvalidCastException cex)
            {
                return BadRequest(cex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Unexpected error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        protected async Task<IActionResult> RemoveAsync<T>(int id) where T : BaseModel
        {

            try
            {
                await _factory.GetDataService<T>().RemoveAsync<T>(id);
                return NoContent();
            }
            catch (InvalidCastException cex)
            {
                return BadRequest(cex.Message);
            }
            catch (Exception ex)
            {
                return Problem($"Unexpected error: {ex.Message}");
            }
        }
    }

}
