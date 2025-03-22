using System.Data;
using System.Data.SqlClient;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;

namespace AptOnline.Infrastructure.AptolMidwareContext.ResepRsAgg
{
    public interface IResepItemDal
    {
        bool Insert(ResepItemModel resepItem);
        bool Delete(string resepId, string itemId);
    }
    public class ResepItemDal : IResepItemDal
    {
        private readonly DatabaseOptions _options;
        public ResepItemDal(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
        }

        public bool Delete(string resepId, string itemId)
        {
            throw new NotImplementedException();
        }

        public bool Insert(ResepItemModel item)
        {
            const string sql = @"
                INSERT INTO APTOL_ResepItem
                    (PenjualanId, OrderNo, IsRacik, RacikId, BarangId, 
                     DphoId, DphoName, Signa1, Signa2, Jho, Qty, Note)
                VALUES
                    (@PenjualanId, @OrderNo, @IsRacik, @RacikId, @BarangId, 
                     @DphoId, @DphoName, @Signa1, @Signa2, @Jho, @Qty, @Note)";
            var dp = new DynamicParameters();
            dp.AddParam("@PenjualanId", item.PenjualanId, SqlDbType.VarChar);
            dp.AddParam("@OrderNo", item.OrderNo, SqlDbType.Int);
            dp.AddParam("@IsRacik", item.IsRacik, SqlDbType.Bit);
            dp.AddParam("@RacikId", item.RacikId, SqlDbType.VarChar);
            dp.AddParam("@BarangId", item.BarangId, SqlDbType.VarChar);
            dp.AddParam("@DphoId", item.DphoId, SqlDbType.VarChar);
            dp.AddParam("@DphoName", item.DphoName, SqlDbType.VarChar);
            dp.AddParam("@Signa1", item.Signa1, SqlDbType.Int);
            dp.AddParam("@Signa2", item.Signa2, SqlDbType.Int);
            dp.AddParam("@Jho", item.Jho, SqlDbType.Int);
            dp.AddParam("@Qty", item.Qty, SqlDbType.Int);
            dp.AddParam("@Note", item.Note, SqlDbType.VarChar);

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
