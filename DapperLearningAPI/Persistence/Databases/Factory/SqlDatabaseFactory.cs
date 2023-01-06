using DapperLearningAPI.Helpers;
using DapperLearningAPI.Models;
using DapperLearningAPI.Persistence.Commands.Factory;

namespace DapperLearningAPI.Persistence.Databases.Factory
{
    public class SqlDatabaseFactory : IDatabaseFactory
    {
        private readonly Dictionary<Type, IDatabase> _databases = new();
        private readonly ICommandFactory _commandFactory;
        private readonly SqlHelper _sqlHelper;

        public SqlDatabaseFactory(ICommandFactory factory, SqlHelper helper)
        {
            _commandFactory = factory;
            _sqlHelper = helper;
            _databases.Add(typeof(BillsProducts), new BillsProductsDatabase(_commandFactory, _sqlHelper));
        }

        public IDatabase GetDatabase<T>() where T : BaseModel
        {
            if (_databases.TryGetValue(typeof(T), out var database))
            {
                return database;
            }

            return new DefaultSqlDatabase(_commandFactory, _sqlHelper);
        }
    }
}
