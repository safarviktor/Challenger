using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Challenger.Models;

namespace Challenger.DataAccess
{
    public class UserRepository : BaseRepository
    {
        public async Task<bool> EmailExists(string email)
        {
            const string query = "SELECT 1" +
                                 "FROM clg.[User]" +
                                 "WHERE Email = @email";
            var p = new DynamicParameters();
            p.Add("@email", email, DbType.String);

            return await WithConnection(async c =>
            {
                return (await c.QueryAsync<int>(sql: query, param: p)).FirstOrDefault() > 0;
            });
        }

        public async Task<int> CreateUser(string name, string email, string passwordHash, string passwordSalt)
        {
            const string query = "INSERT INTO clg.[User] " +
                                 "(Name, Email, PasswordHash, PasswordSalt) " +
                                 "SELECT @name, @email, @passwordHash, @passwordSalt " + 
                                 "SELECT SCOPE_IDENTITY()";

            var p = new DynamicParameters();
            p.Add("@name", name, DbType.String);
            p.Add("@email", email, DbType.String);
            p.Add("@passwordHash", passwordHash, DbType.String);
            p.Add("@passwordSalt", passwordSalt, DbType.String);

            return await WithConnection(async c =>
            {
                return (await c.QueryAsync<int>(sql: query, param: p)).FirstOrDefault();
            });
        }

        public async Task<CompleteUser> GetCompleteUserByEmail(string email)
        {
            const string query = "SELECT " +
                                 "    Id" +
                                 "  , Name" +
                                 "  , Email" +
                                 "  , PasswordHash" +
                                 "  , PasswordSalt" +
                                 "FROM clg.[User]" +
                                 "WHERE Email = @email";
            var p = new DynamicParameters();
            p.Add("@email", email, DbType.String);

            return await WithConnection(async c =>
            {
                return (await c.QueryAsync<CompleteUser>(sql: query, param: p)).FirstOrDefault();
            });
        }
    }
}