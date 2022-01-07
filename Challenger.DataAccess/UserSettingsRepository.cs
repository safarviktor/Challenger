using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Challenger.Models;
using Dapper;

namespace Challenger.DataAccess
{
    public class UserSettingsRepository : BaseRepository
    {
        public UserSettingsModel GetUserSettingsModel(string userId)
        {
            const string query = @"SELECT  
                                    UserId, 
                                    Salutation, 
                                    DefaultRepetitions 
                                FROM clg.[UserSettings] 
                                WHERE UserId = @userId";
            var p = new DynamicParameters();
            p.Add("@userId", userId, DbType.String);

            return WithConnectionSync(c =>
            {
                return c.Query<UserSettingsModel>(sql: query, param: p).FirstOrDefault();
            });
        }

        public async Task CreateUserSettings(string userId)
        {
            const string query = "INSERT INTO clg.[UserSettings] (UserId) SELECT @userId";
            var p = new DynamicParameters();
            p.Add("@userId", userId, DbType.String);

            await WithConnection(async c =>
            {
                await c.ExecuteAsync(sql: query, param: p);
            });
        }
    }
}