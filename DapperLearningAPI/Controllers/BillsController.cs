using DapperLearningAPI.DTO;
using DapperLearningAPI.Models;
using DapperLearningAPI.Services.Data;
using DapperLearningAPI.Services.Factory;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace DapperLearningAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillsController : ApiController
    {
        public BillsController(IDataServiceFactory factory) : base(factory)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return await base.GetAllAsync<Bill>();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return await base.GetAsync<Bill>(id);
        }
        [HttpPost]
        public async Task<IActionResult> InsertAsync(BillRequest request)
        {
            return await base.InsertAsync<Bill>(MapRequestToBill(request));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, BillRequest request)
        {
            return await base.UpdateAsync<Bill>(id, MapRequestToBill(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return await base.RemoveAsync<Bill>(id);
        }

        private Bill MapRequestToBill(BillRequest request)
        {
            var bill = new Bill() { Date = DateTime.Now };
            bill.Products = request.Products.Select(x => new Product() { Id = x }).ToList();

            return bill;
        }
    }
}
