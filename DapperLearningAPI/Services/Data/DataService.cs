using DapperLearningAPI.Helpers;
using DapperLearningAPI.Models;
using DapperLearningAPI.Persistence.Databases;
using DapperLearningAPI.Persistence.Databases.Factory;

namespace DapperLearningAPI.Services.Data
{
    public class DataService : IDataService
    {
        protected readonly IDatabaseFactory _factory;
        protected readonly LogHelper _log;
        public DataService(IDatabaseFactory factory, LogHelper log)
        {
            _factory = factory;
            _log = log;
        }
        public virtual async Task<T> InsertAsync<T>(T model) where T : BaseModel
        {

            return await _factory.GetDatabase<T>().InsertAsync<T>(model);
        }

        public virtual async Task<T> GetAsync<T>(int id) where T : BaseModel
        {
            return await _factory.GetDatabase<T>().GetAsync<T>(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync<T>() where T : BaseModel
        {
            return await _factory.GetDatabase<T>().GetAllAsync<T>();
        }

        public virtual async Task UpdateAsync<T>(int id, T model) where T : BaseModel
        {
            CheckAffectedRows(await _factory.GetDatabase<T>().UpdateAsync<T>(id, model));
        }

        public virtual async Task RemoveAsync<T>(int id) where T : BaseModel
        {
            CheckAffectedRows(await _factory.GetDatabase<T>().RemoveAsync<T>(id));
        }

        private void CheckAffectedRows(int rows)
        {
            if (rows.Equals(0))
            {
                _log.Info($"{this.GetType().Name}.{this} affected 0 rows");
            }
            else
            {
                _log.Info($"{this.GetType().Name}.{this} affected {rows} rows");
            }
        }
    }
}
