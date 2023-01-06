using Dapper;
using DapperLearningAPI.Helpers;
using DapperLearningAPI.Models;

namespace DapperLearningAPI.Persistence.Commands.Sql
{
    public class CategoriesSqlCommand : ICommand
    {

        private const string selectAllQuery = $"SELECT * FROM {SqlHelper.TableName.Categories}";
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
            var category = CastHelper.Cast<T, Category>(model);
            string query = $"INSERT INTO {SqlHelper.TableName.Categories} OUTPUT INSERTED.* VALUES (@Name)";
            var queryParams = new DynamicParameters();
            queryParams.Add("@Name", category.Name);

            return new CommandResult() { Query = query, Parameters = queryParams };
        }
        public CommandResult UpdateQuery<T>(int id, T model) where T : BaseModel
        {
            var category = CastHelper.Cast<T, Category>(model);
            var queryParams = new DynamicParameters();
            var queryBuilder = new List<string>
            {
                $"UPDATE {SqlHelper.TableName.Categories}"
            };
            if (!string.IsNullOrEmpty(category.Name))
            {
                queryBuilder.Add("SET Name=@Name");
                queryParams.Add("@Name", category.Name);
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
            string query = $"DELETE FROM {SqlHelper.TableName.Categories} WHERE ID=@ID";
            var queryParams = new DynamicParameters();
            queryParams.Add("@ID", id);
            return new CommandResult() { Query = query, Parameters = queryParams };
        }
    }
}
