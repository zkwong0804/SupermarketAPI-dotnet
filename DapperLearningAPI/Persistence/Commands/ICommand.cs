using DapperLearningAPI.Models;

namespace DapperLearningAPI.Persistence.Commands
{
    public interface ICommand
    {
        CommandResult GetAllQuery();
        CommandResult GetQuery(int id);
        CommandResult InsertQuery<T>(T model) where T : BaseModel;
        CommandResult RemoveQuery(int id);
        CommandResult UpdateQuery<T>(int id, T model) where T : BaseModel;
    }
}