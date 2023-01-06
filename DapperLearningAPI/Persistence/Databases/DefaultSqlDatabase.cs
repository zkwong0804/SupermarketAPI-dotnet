using Dapper;
using DapperLearningAPI.Helpers;
using DapperLearningAPI.Models;
using DapperLearningAPI.Persistence.Commands.Factory;
using System.Reflection;

namespace DapperLearningAPI.Persistence.Databases
{
    public class DefaultSqlDatabase : IDatabase
    {
        protected readonly ICommandFactory _factory;
        protected readonly SqlHelper _sqlHelper;
        public DefaultSqlDatabase(ICommandFactory factory, SqlHelper helper)
        {
            _factory = factory;
            _sqlHelper = helper;
        }

        public virtual async Task<T> InsertAsync<T>(T model) where T : BaseModel
        {
            var command = _factory.GetCommand<T>();
            var commandResult = command.InsertQuery(model);
            using (var conn = _sqlHelper.GetConnection())
            {
                return await conn.QuerySingleAsync<T>
                    (commandResult.Query, commandResult.Parameters);
            }
        }

        public virtual async Task<T> GetAsync<T>(int id) where T : BaseModel
        {
            var command = _factory.GetCommand<T>();
            var commandResult = command.GetQuery(id);
            using (var conn = _sqlHelper.GetConnection())
            {
                return await conn.QuerySingleAsync<T>
                    (commandResult.Query, commandResult.Parameters);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync<T>() where T : BaseModel
        {
            var command = _factory.GetCommand<T>();
            var commandResult = command.GetAllQuery();
            using (var conn = _sqlHelper.GetConnection())
            {
                return await conn.QueryAsync<T>(commandResult.Query);
            }
        }

        public virtual async Task<int> UpdateAsync<T>(int id, T model) where T : BaseModel
        {
            var command = _factory.GetCommand<T>();
            var commandResult = command.UpdateQuery(id, model);
            using (var conn = _sqlHelper.GetConnection())
            {
                return await conn.ExecuteAsync
                    (commandResult.Query, commandResult.Parameters);
            }
        }

        public virtual async Task<int> RemoveAsync<T>(int id) where T : BaseModel
        {
            var command = _factory.GetCommand<T>();
            var commandResult = command.RemoveQuery(id);
            using (var conn = _sqlHelper.GetConnection())
            {
                return await conn.ExecuteAsync
                    (commandResult.Query, commandResult.Parameters);
            }
        }
    }
}
