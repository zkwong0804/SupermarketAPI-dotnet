using Microsoft.Data.SqlClient;

namespace DapperLearningAPI.Helpers
{
    public class SqlHelper
    {
        private readonly string connectionString;
        public SqlHelper(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("db");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static class TableName
        {
            public const string Products = "Products";
            public const string Categories = "Categories";
            public const string Bills = "Bills";
            public const string BillsProducts = "Bills_Products";
        }
    }
}
