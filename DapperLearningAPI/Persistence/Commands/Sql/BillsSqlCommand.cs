using Dapper;
using DapperLearningAPI.Helpers;
using DapperLearningAPI.Models;

namespace DapperLearningAPI.Persistence.Commands.Sql
{
    public class BillsSqlCommand : ICommand
    {

        private const string selectAllQuery = $"SELECT * FROM {SqlHelper.TableName.Bills}";
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
            var bill = CastHelper.Cast<T, Bill>(model);
            bill.Date = DateTime.UtcNow;
            string query = $"INSERT INTO {SqlHelper.TableName.Bills} OUTPUT INSERTED.* VALUES (@Date)";
            var queryParams = new DynamicParameters();
            queryParams.Add("@Date", bill.Date);

            return new CommandResult() { Query = query, Parameters = queryParams };
        }

        public CommandResult UpdateQuery<T>(int id, T model) where T : BaseModel
        {
            var bill = CastHelper.Cast<T, Bill>(model);
            var queryParams = new DynamicParameters();
            var queryBuilder = new List<string>
            {
                $"UPDATE {SqlHelper.TableName.Bills}"
            };
            if (!bill.Date.Equals(default(DateTime)))
            {
                queryBuilder.Add("SET Date=@Date");
                queryParams.Add("@Date", bill.Date);
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
            string query = $"DELETE FROM {SqlHelper.TableName.Bills} WHERE ID=@ID";
            var queryParams = new DynamicParameters();
            queryParams.Add("@ID", id);
            return new CommandResult() { Query = query, Parameters = queryParams };
        }
    }
}
