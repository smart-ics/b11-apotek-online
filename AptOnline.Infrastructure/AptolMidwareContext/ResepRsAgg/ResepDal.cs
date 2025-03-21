using AptOnline.Api.Models;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Nuna.Lib.DataAccessHelper;
using AptOnline.Api.Helpers;
using AptOnline.Helpers;
using Microsoft.Extensions.Options;
using MassTransit;
using System.Collections.Generic;

namespace AptOnline.Infrastructure.LocalContext.ResepRsAgg
{
    public interface IResepDal
    {
        bool Insert(ResepModel resep);
        bool Update(ResepModel resep);
        bool Delete(string resepId);
        ResepModel GetData(string resepId);
    }
    public class ResepDal : IResepDal
    {
        private readonly DatabaseOptions _options;
        public ResepDal(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
        }

        public bool Delete(string resepId)
        {
            throw new NotImplementedException();
        }

        public ResepModel GetData(string resepId)
        {
            const string sql = @"
            SELECT 
                PenjualanId, ReffId, SepId, ResepDate, NoPeserta, JenisObatId, EntryDate, FaskesAsal 
            FROM
                APTOL_Resep
            WHERE
                PenjualanId = @PenjualanId";
            var dp = new DynamicParameters();
            dp.AddParam("@PenjualanId", resepId, SqlDbType.VarChar);
            using var conn = new SqlConnection(ConnStringHelper.Get(_options));
            return conn.ReadSingle<ResepModel>(sql, dp);
        }

        public bool Insert(ResepModel resep)
        {
            const string sql = @"
                INSERT INTO APTOL_Resep
                    (PenjualanId, ReffId, SepId, ResepDate,	NoPeserta, JenisObatId, EntryDate, FaskesAsal)
                VALUES
                    (@PenjualanId, @ReffId, @SepId, @ResepDate, @NoPeserta, @JenisObatId, @EntryDate, @FaskesAsal)";
            var dp = new DynamicParameters();
            dp.AddParam("@PenjualanId", resep.PenjualanId, SqlDbType.VarChar);
            dp.AddParam("@ReffId", resep.ReffId, SqlDbType.VarChar);
            dp.AddParam("@SepId", resep.SepId, SqlDbType.VarChar);
            dp.AddParam("@ResepDate", resep.ResepDate, SqlDbType.DateTime);
            dp.AddParam("@NoPeserta", resep.NoPeserta, SqlDbType.VarChar);
            dp.AddParam("@JenisObatId", resep.JenisObatId, SqlDbType.VarChar);
            dp.AddParam("@EntryDate", resep.EntryDate, SqlDbType.DateTime);
            dp.AddParam("@FaskesAsal", resep.FaskesAsal, SqlDbType.VarChar);

            // EXECUTE
            using var conn = new SqlConnection(ConnStringHelper.Get(_options));
            try
            {
                conn.Execute(sql, dp);
                return true;
            }
            catch { return false; }
        }

        public bool Update(ResepModel resep)
        {
            throw new NotImplementedException();
        }
    }
}
