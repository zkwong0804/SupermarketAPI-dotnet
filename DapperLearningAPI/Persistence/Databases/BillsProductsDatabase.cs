using Dapper;
using DapperLearningAPI.Helpers;
using DapperLearningAPI.Models;
using DapperLearningAPI.Persistence.Commands.Factory;
using DapperLearningAPI.Persistence.Commands.Sql;

namespace DapperLearningAPI.Persistence.Databases
{
    public class BillsProductsDatabase : DefaultSqlDatabase
    {
        public BillsProductsDatabase(ICommandFactory factory, SqlHelper helper) : base(factory, helper)
        { 
        }

        public virtual async Task<IEnumerable<BillsProducts>> GetBPByBillAsync(int id)
        {
            var command = _factory.GetCommand<BillsProducts>();
            var result = command.GetQuery(id);
            using (var conn = _sqlHelper.GetConnection())
            {
                return await conn.QueryAsync<BillsProducts>(result.Query, result.Parameters);
            }
        }

        public virtual async Task RemoveBPByBillProductAsync(BillsProducts bp)
        {
            var command = _factory.GetCommand<BillsProducts>() as BillsProductsSqlCommand;
            if (command is null)
            {
                throw new InvalidCastException();
            }

            var result = command.RemoveQueryByBillProduct(bp);
            using (var conn = _sqlHelper.GetConnection())
            {
                await conn.ExecuteAsync(result.Query, result.Parameters);
            }
        }

    }
}
