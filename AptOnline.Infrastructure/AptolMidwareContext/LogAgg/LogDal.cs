using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.AptolCloudContext.LogAgg;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;

namespace AptOnline.Infrastructure.AptolMidwareContext.LogAgg
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
                INSERT INTO APTOL_Log
                    (LogDateTime, ResepId, Request, Response, Message)
                VALUES
                    (@LogDateTime, @ResepId, @Request, @Response, @Message)";
            var dp = new DynamicParameters();
            dp.AddParam("@LogDateTime", log.LogDateTime, SqlDbType.DateTime);
            dp.AddParam("@ResepId", log.ResepId, SqlDbType.VarChar);
            dp.AddParam("@Request", log.ResepRequest, SqlDbType.VarChar);
            dp.AddParam("@Response", log.ResepResponse, SqlDbType.VarChar);
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
