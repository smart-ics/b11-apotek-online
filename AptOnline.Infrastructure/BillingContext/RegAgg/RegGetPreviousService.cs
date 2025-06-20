using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.BillingContext.RegAgg;

public class RegGetPreviousService : IRegGetPreviousService
{
    private readonly IDbConnection _conn;
    private readonly IRegGetService _regGetService;

    public RegGetPreviousService(IOptions<DatabaseOptions> opt, 
        IRegGetService regGetService)
    {
        _regGetService = regGetService;
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }

    public MayBe<RegType> Execute(IRegKey req)
    {
        const string sql = @"
            SELECT fs_kd_reg_hut
            FROM ta_registrasi6
            WHERE fs_kd_reg = @fs_kd_reg";

        var dp = new DynamicParameters();
        dp.AddParam("@fs_kd_reg", req.RegId, SqlDbType.VarChar);

        var regHut = _conn.QuerySingle<string>(sql, dp);
        if (regHut == null) 
            return MayBe<RegType>.None;
        
        var resut = _regGetService.Execute(RegType.Key(regHut));
        return MayBe.From(resut);
    }
}

public class RegGetPreviousServiceTest
{
    private readonly RegGetPreviousService _sut;
    private readonly Mock<IRegGetService> _regGetService;
    private readonly IDbConnection _conn;

    public RegGetPreviousServiceTest()
    {
        _regGetService = new Mock<IRegGetService>();
        var opt = FakeAppSetting.GetDatabaseOptions();
        _sut = new RegGetPreviousService(opt, _regGetService.Object);
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }

    [Fact]
    public void UT1_GivenValidRegID_WhenExecute_ThenReturnAsExpected()
    {
        //      ARRANGE
        //      - insert reg hutang
        using var tras = TransHelper.NewScope();
        const string sql = @"INSERT INTO ta_registrasi6(fs_kd_reg, fs_kd_reg_hut)
            VALUES(@fs_kd_reg, @fs_kd_reg_hut)";
        var dp = new DynamicParameters();
        dp.AddParam("@fs_kd_reg", "RG002", SqlDbType.VarChar);
        dp.AddParam("@fs_kd_reg_hut", "RG001", SqlDbType.VarChar);
        _conn.Execute(sql, dp);
        //      - mock reg hutang   
        var regHut = new RegType(
            "RG001", DateTime.Parse("2024-06-05"), DateTime.Parse("2024-06-05"),
            PasienType.Default, JenisRegEnum.RawatDarurat, KelasRawatType.Default,
            LayananType.Default.ToRefference());
        _regGetService.Setup(x => x.Execute(RegType.Key("RG001"))).Returns(regHut); 

        //      ACT
        var actual = _sut.Execute(RegType.Key("RG002"));
        
        //      ASSERT
        _regGetService.Verify(x => x.Execute(RegType.Key("RG001")), Times.Once);
        actual.Value.Should().BeEquivalentTo(regHut);
    }
}