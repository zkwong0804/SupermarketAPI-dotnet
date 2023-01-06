using DapperLearningAPI.Models;

namespace DapperLearningAPI.Persistence.Databases
{
    public interface IDatabase
    {
        Task<T> GetAsync<T>(int id) where T : BaseModel;
        Task<IEnumerable<T>> GetAllAsync<T>() where T : BaseModel;
        Task<T> InsertAsync<T>(T model) where T : BaseModel;
        Task<int> RemoveAsync<T>(int id) where T : BaseModel;
        Task<int> UpdateAsync<T>(int id, T model) where T : BaseModel;
    }
}