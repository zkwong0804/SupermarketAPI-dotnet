using DapperLearningAPI.Services.Data;

namespace DapperLearningAPI.Services.Factory
{
    public interface IDataServiceFactory
    {
        IDataService GetDataService<T>();
    }
}