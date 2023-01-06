using Dapper;
using DapperLearningAPI.Helpers;
using DapperLearningAPI.Models;
using System.Text;

namespace DapperLearningAPI.Persistence.Commands.Sql
{
    public class ProductsSqlCommand : ICommand
    {
        
        private const string selectAllQuery = $"SELECT * FROM {SqlHelper.TableName.Products}";
        public CommandResult GetQuery(int id)
        {
            string query = $"{selectAllQuery} WHERE ID=@ID";
            var queryParams = new DynamicParameters();
            queryParams.Add("@ID", id);

            return new CommandResult() { Query = query, Parameters = queryParams };
        }
        public CommandResult GetAllQuery()
        {
            return new CommandResult() { Query = selectAllQuery };
        }
        public CommandResult InsertQuery<T>(T model) where T : BaseModel
        {
            var product = CastHelper.Cast<T, Product>(model);
            string query = $"INSERT INTO {SqlHelper.TableName.Products} OUTPUT INSERTED.* VALUES (@Name, @Price, @CategoryID)";
            var queryParams = new DynamicParameters();
            queryParams.Add("@Name", product.Name);
            queryParams.Add("@Price", product.Price);
            queryParams.Add("@CategoryID", product.CategoryID);

            return new CommandResult() { Query = query, Parameters = queryParams };
        }
        public CommandResult UpdateQuery<T>(int id, T model) where T : BaseModel
        {
            var product = CastHelper.Cast<T, Product>(model);
            var queryParams = new DynamicParameters();
            var queryBuilder = new List<string>();

            queryBuilder.Add($"UPDATE {SqlHelper.TableName.Products}");
            if (!string.IsNullOrEmpty(product.Name))
            {
                queryBuilder.Add("SET Name=@Name,");
                queryParams.Add("@Name", product.Name);
            }

            if (!product.Price.Equals(decimal.MinusOne))
            {
                queryBuilder.Add("Price=@Price,");
                queryParams.Add("@Price", product.Price);
            }

            if (!product.CategoryID.Equals(default(int)))
            {
                queryBuilder.Add("CategoryID=@CategoryID");
                queryParams.Add("@CategoryID", product.CategoryID);
            }

            queryBuilder.Add("WHERE ID=@ID");
            queryParams.Add("@ID", id);

            return new CommandResult()
            {
                Query = string.Join(" ", queryBuilder),
                Parameters = queryParams
            };
        }
        public CommandResult RemoveQuery(int id)
        {
            string query = $"DELETE FROM {SqlHelper.TableName.Products} WHERE ID=@ID";
            var queryParams = new DynamicParameters();
            queryParams.Add("@ID", id);
            return new CommandResult() { Query = query, Parameters = queryParams };
        }
    }
}
