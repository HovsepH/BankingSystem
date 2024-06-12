using AccountService.Application.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace AccountService.Infrastructure.Data
{
    public class ConnectionFactory: IConnectionFactory
    {
        private readonly DatabaseConfig _databaseConfig;

        public ConnectionFactory(IOptions<DatabaseConfig> databaseConfig)
        {
            _databaseConfig = databaseConfig.Value;
        }

        public SqlConnection GetMyDBConnection()
        {
            string connectionString = _databaseConfig.DefaultConnection;

            return new SqlConnection(connectionString);
        }

       
    }

}
