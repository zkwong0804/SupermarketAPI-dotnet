using DapperLearningAPI.Models;
using DapperLearningAPI.Persistence.Commands.Sql;

namespace DapperLearningAPI.Persistence.Commands.Factory
{
    public class SqlCommandFactory : ICommandFactory
    {
        private static Dictionary<Type, ICommand> _commands = new();
        static SqlCommandFactory()
        {
            _commands.Add(typeof(Product), new ProductsSqlCommand());
            _commands.Add(typeof(Category), new CategoriesSqlCommand());
            _commands.Add(typeof(Bill), new BillsSqlCommand());
            _commands.Add(typeof(BillsProducts), new BillsProductsSqlCommand());
        }

        public ICommand GetCommand<T>() where T : BaseModel
        {
            return _commands[typeof(T)];
        }
    }
}
