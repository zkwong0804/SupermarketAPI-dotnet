using DapperLearningAPI.Models;

namespace DapperLearningAPI.Services.Data
{
    public interface IDataService
    {
        Task<IEnumerable<T>> GetAllAsync<T>() where T : BaseModel;
        Task<T> GetAsync<T>(int id) where T : BaseModel;
        Task<T> InsertAsync<T>(T model) where T : BaseModel;
        Task RemoveAsync<T>(int id) where T : BaseModel;
        Task UpdateAsync<T>(int id, T model) where T : BaseModel;
    }
}