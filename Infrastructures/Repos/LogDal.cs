using AptOnline.Api.Models;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Nuna.Lib.DataAccessHelper;
using AptOnline.Api.Helpers;
using AptOnline.Helpers;
using Microsoft.Extensions.Options;

namespace AptOnline.Api.Infrastructures.Repos
{
    public interface ILogDal
    {
        bool Insert(LogModel log);
    }
    public class LogDal : ILogDal
    {
        private readonly DatabaseOptions _options;
        public LogDal(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
        }
        public bool Insert(LogModel log)
        {
            const string sql = @"
                INSERT INTO SCHED_Log 
                            (LogDateTime, ResepId, ResepRequest, ResepResponse, ItemNonRacikCount, ItemRacikCount, Message)
                VALUES      (@LogDateTime, @ResepId, @ResepRequest, @ResepResponse, @ItemNonRacikCount, @ItemRacikCount, @Message)";
            var dp = new DynamicParameters();
            dp.AddParam("@LogDateTime", log.LogDateTime, SqlDbType.DateTime);
            dp.AddParam("@ResepId", log.ResepId, SqlDbType.VarChar);
            dp.AddParam("@ResepRequest", log.ResepRequest, SqlDbType.VarChar);
            dp.AddParam("@ResepResponse", log.ResepResponse, SqlDbType.VarChar);
            dp.AddParam("@ItemNonRacikCount", log.ItemNonRacikCount, SqlDbType.Int);
            dp.AddParam("@ItemRacikCount", log.ItemRacikCount, SqlDbType.Int);
            dp.AddParam("@Message", log.Message, SqlDbType.VarChar);

            // EXECUTE

            using var conn = new SqlConnection(ConnStringHelper.Get(_options));
            try
            {
                conn.Execute(sql, dp);
                return true;
            }
            catch { return false; }
        }
    }
}
