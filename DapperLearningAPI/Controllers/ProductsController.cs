using DapperLearningAPI.Models;
using DapperLearningAPI.Services.Data;
using DapperLearningAPI.Services.Factory;
using Microsoft.AspNetCore.Mvc;

namespace DapperLearningAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ApiController
    {
        public ProductsController(IDataServiceFactory factory) : base(factory)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return await base.GetAllAsync<Product>();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return await base.GetAsync<Product>(id);
        }
        [HttpPost]
        public async Task<IActionResult> InsertAsync(Product product)
        {
            return await base.InsertAsync<Product>(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Product product)
        {
            return await base.UpdateAsync<Product>(id, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return await base.RemoveAsync<Product>(id);
        }
    }
}
