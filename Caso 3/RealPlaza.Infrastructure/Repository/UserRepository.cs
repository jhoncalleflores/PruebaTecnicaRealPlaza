using Dapper;
using RealPlaza.Domain.Entities;
using RealPlaza.Domain.Interfaces;
using RealPlaza.Infrastructure.Data;

namespace RealPlaza.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _factory;

        public UserRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var conn = _factory.GetConnection();

            string sql = "SELECT * FROM users ORDER BY id";

            return await conn.QueryAsync<User>(sql);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            using var conn = _factory.GetConnection();

            string sql = "SELECT * FROM users WHERE id = @id";

            return await conn.QueryFirstOrDefaultAsync<User>(sql, new { id });
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            using var conn = _factory.GetConnection();

            string sql = "SELECT * FROM users WHERE username = @username";

            return await conn.QueryFirstOrDefaultAsync<User>(sql, new { username });
        }

        public async Task<int> CreateAsync(User user)
        {
            using var conn = _factory.GetConnection();

            string sql = @" INSERT INTO users(username,passwordhash,email,birthdate,isactive)
                    VALUES (@Username,@PasswordHash,@Email,@BirthDate,@IsActive)
                    RETURNING id;";

            return await conn.ExecuteScalarAsync<int>(sql, user);
        }

        public async Task UpdateAsync(User user)
        {
            using var conn = _factory.GetConnection();

            string sql = @"
                UPDATE users
                SET username = @Username,
                    email = @Email,
                    birthdate = @BirthDate,
                    isactive = @IsActive
                WHERE id = @Id;
            ";

            await conn.ExecuteAsync(sql, user);
        }

        public async Task DeleteAsync(int id)
        {
            using var conn = _factory.GetConnection();

            string sql = "DELETE FROM users WHERE id = @id";

            await conn.ExecuteAsync(sql, new { id });
        }


    }
}
