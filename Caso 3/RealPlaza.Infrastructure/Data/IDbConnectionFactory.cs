using System.Data;

namespace RealPlaza.Infrastructure.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
