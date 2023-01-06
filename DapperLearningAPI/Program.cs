using DapperLearningAPI.Helpers;
using DapperLearningAPI.Persistence;
using DapperLearningAPI.Persistence.Commands.Factory;
using DapperLearningAPI.Persistence.Databases.Factory;
using DapperLearningAPI.Services.Data;
using DapperLearningAPI.Services.Factory;

namespace DapperLearningAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<SqlHelper>();
            builder.Services.AddSingleton<LogHelper>();
            builder.Services.AddSingleton<ICommandFactory, SqlCommandFactory>();
            builder.Services.AddSingleton<IDatabaseFactory, SqlDatabaseFactory>();
            builder.Services.AddSingleton<IDataServiceFactory, DataServiceFactory>();

            var app = builder.Build();


            app.UseHttpsRedirection();


            app.MapControllers();

            app.Run();
        }
    }
}