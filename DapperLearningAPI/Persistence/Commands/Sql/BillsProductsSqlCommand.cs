using Dapper;
using DapperLearningAPI.Helpers;
using DapperLearningAPI.Models;

namespace DapperLearningAPI.Persistence.Commands.Sql
{
    public class BillsProductsSqlCommand : ICommand
    {
        private const string selectAllQuery = $"SELECT * FROM {SqlHelper.TableName.BillsProducts}";
        public CommandResult GetAllQuery()
        {
            return new CommandResult() { Query = selectAllQuery };
        }

        // by bill ID
        public CommandResult GetQuery(int id)
        {
            return GetQueryByBill(id);
        }

        public CommandResult GetQueryByBill(int id)
        {
            string query = $"{selectAllQuery} WHERE BillID=@ID";
            return GenerateQuery(query, id);
        }

        public CommandResult GetQueryByProduct(int id)
        {
            string query = $"{selectAllQuery} WHERE BillID=@ID";
            return GenerateQuery(query, id);
        }

        public CommandResult InsertQuery<T>(T model) where T : BaseModel
        {
            var bp = CastHelper.Cast<T, BillsProducts>(model);
            string query = $"INSERT INTO {SqlHelper.TableName.BillsProducts} OUTPUT INSERTED.* VALUES (@BillID, @ProductID)";
            var queryParams = new DynamicParameters();
            queryParams.Add("@BillID", bp.BillID);
            queryParams.Add("@ProductID", bp.ProductID);

            return new CommandResult() { Query = query, Parameters = queryParams };
        }

        public CommandResult RemoveQuery(int id)
        {
            string query = $"DELETE FROM {SqlHelper.TableName.BillsProducts} WHERE BillID=@BillID";
            var queryParams = new DynamicParameters();
            queryParams.Add("@BillID", id);
            return new CommandResult() { Query = query, Parameters = queryParams };
        }

        public CommandResult RemoveQueryByBillProduct(BillsProducts bp)
        {
            string query = $"DELETE FROM {SqlHelper.TableName.BillsProducts} WHERE BillID=@BillID AND ProductID=@ProductID";
            var queryParams = new DynamicParameters();
            queryParams.Add("@BillID", bp.BillID);
            queryParams.Add("@ProductID", bp.ProductID);
            return new CommandResult() { Query = query, Parameters = queryParams };
        }

        public CommandResult UpdateQuery<T>(int id, T model) where T : BaseModel
        {
            throw new NotImplementedException();
        }

        private CommandResult GenerateQuery(string query, int id)
        {
            var queryParams = new DynamicParameters();
            queryParams.Add("@ID", id);

            return new CommandResult() { Query = query, Parameters = queryParams };
        }
    }
}
