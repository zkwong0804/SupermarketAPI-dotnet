using DapperLearningAPI.Models;

namespace DapperLearningAPI.Persistence.Databases.Factory
{
    public interface IDatabaseFactory
    {
        IDatabase GetDatabase<T>() where T : BaseModel;
    }
}