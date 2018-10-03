namespace SampleApp.Connection.Interfaces
{
    using System.Data;

    public interface IDbConnectionFactory
    {
        IDbConnection CreateTenantDbConnection(string connectionString);
    }
}
