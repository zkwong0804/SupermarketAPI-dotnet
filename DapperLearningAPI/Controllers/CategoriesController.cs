using DapperLearningAPI.Models;
using DapperLearningAPI.Services.Data;
using DapperLearningAPI.Services.Factory;
using Microsoft.AspNetCore.Mvc;

namespace DapperLearningAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CategoriesController : ApiController
    {
        public CategoriesController(IDataServiceFactory factory) : base(factory)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return await base.GetAllAsync<Category>();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return await base.GetAsync<Category>(id);
        }
        [HttpPost]
        public async Task<IActionResult> InsertAsync(Category category)
        {
            return await base.InsertAsync<Category>(category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Category category)
        {
            return await base.UpdateAsync<Category>(id, category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return await base.RemoveAsync<Category>(id);
        }
    }
}
