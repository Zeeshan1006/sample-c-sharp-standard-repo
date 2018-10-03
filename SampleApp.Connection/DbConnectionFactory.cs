using System.Data;
using System.Data.SqlClient;
using SampleApp.Connection.Interfaces;

namespace SampleApp.Connection
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection CreateTenantDbConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = AppSettingReader.GetAppSettingReader().DbConnection;
            }

            return new SqlConnection(connectionString);
        }
    }
}
