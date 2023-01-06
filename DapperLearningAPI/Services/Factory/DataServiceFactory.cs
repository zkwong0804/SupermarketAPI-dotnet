using DapperLearningAPI.Helpers;
using DapperLearningAPI.Models;
using DapperLearningAPI.Persistence.Databases.Factory;
using DapperLearningAPI.Services.Data;

namespace DapperLearningAPI.Services.Factory
{
    public class DataServiceFactory : IDataServiceFactory
    {
        private readonly Dictionary<Type, IDataService> _dataservices = new();
        private readonly IDatabaseFactory _factory;
        private readonly LogHelper _logger;
        public DataServiceFactory(IDatabaseFactory factory, LogHelper logger)
        {
            _factory = factory;
            _logger = logger;
            _dataservices.Add(typeof(Bill), new BillDataService(factory, logger));
        }

        public IDataService GetDataService<T>()
        {
            if (_dataservices.TryGetValue(typeof(T), out var ds))
            {
                return ds;
            }

            return new DataService(_factory, _logger);
        }
    }
}
