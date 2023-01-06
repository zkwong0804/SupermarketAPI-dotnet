using Dapper;

namespace DapperLearningAPI.Persistence.Commands
{
    public class CommandResult
    {
        public string Query { get; set; } = string.Empty;
        public DynamicParameters Parameters { get; set; } = new DynamicParameters();
    }
}
