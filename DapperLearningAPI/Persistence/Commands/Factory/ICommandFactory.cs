using DapperLearningAPI.Models;

namespace DapperLearningAPI.Persistence.Commands.Factory
{
    public interface ICommandFactory
    {
        ICommand GetCommand<T>() where T : BaseModel;
    }
}