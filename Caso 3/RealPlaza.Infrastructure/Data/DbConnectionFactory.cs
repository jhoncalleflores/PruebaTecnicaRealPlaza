using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace RealPlaza.Infrastructure.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("RealPlazaDB")!;
        }

        public IDbConnection GetConnection()
            => new NpgsqlConnection(_connectionString);
    }
}
