using DapperLearningAPI.Helpers;
using DapperLearningAPI.Models;
using DapperLearningAPI.Persistence.Databases;
using DapperLearningAPI.Persistence.Databases.Factory;

namespace DapperLearningAPI.Services.Data
{
    public class BillDataService : DataService
    {
        public BillDataService(IDatabaseFactory factory, LogHelper log) : base(factory, log)
        {
        }

        public override async Task<T> InsertAsync<T>(T model)
        {
            var bill = CastHelper.Cast<T, Bill>(model);
            var insertedBill = await _factory.GetDatabase<Bill>().InsertAsync<Bill>(bill);
            var bpDb = _factory.GetDatabase<BillsProducts>();
            List<Task> insertProductsTask = new();
            foreach(var p in bill.Products)
            {
                var task = bpDb.InsertAsync<BillsProducts>(new BillsProducts() 
                { 
                    BillID = insertedBill.Id, 
                    ProductID = p.Id 
                });
                insertProductsTask.Add(task);
            }

            await Task.WhenAll(insertProductsTask);
            return CastHelper.Cast<Bill, T>(insertedBill);
        }

        public override async Task UpdateAsync<T>(int id, T model)
        {
            var bill = CastHelper.Cast<T, Bill>(model);
            var bpdb = _factory.GetDatabase<BillsProducts>() as BillsProductsDatabase;
            if (bpdb is null)
            {
                throw new InvalidCastException();
            }

            var oldProducts = await bpdb.GetBPByBillAsync(id);
            var newProducts = bill.Products.Select(x => new BillsProducts() 
            { 
                BillID = id, 
                ProductID = x.Id 
            });

            var intersect = oldProducts.Intersect(newProducts);
            var toRemove = oldProducts.Except(intersect);
            var toAdd = newProducts.Except(intersect);

            var tasks = new List<Task>();

            foreach(var bp in toRemove)
            {
                tasks.Add(bpdb.RemoveBPByBillProductAsync(bp));
            }

            foreach(var bp in toAdd)
            {
                tasks.Add(bpdb.InsertAsync(bp));
            }

            await Task.WhenAll(tasks);
        }
    }
}
