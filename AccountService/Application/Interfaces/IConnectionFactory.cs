using Microsoft.Data.SqlClient;
using System.Data;

namespace AccountService.Application.Interfaces
{
    public interface IConnectionFactory
    {
        public SqlConnection GetMyDBConnection();
    }
}
