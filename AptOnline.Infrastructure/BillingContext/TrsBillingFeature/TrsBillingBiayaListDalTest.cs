using System.Data;
using System.Data.SqlClient;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.BillingContext.TrsBillingFeature;

public class TrsBillingBiayaListDalTest
{
    private readonly TrsBillingBiayaListDal _sut;
    private readonly IDbConnection _conn;
    public TrsBillingBiayaListDalTest()
    {
        var opt = FakeAppSetting.GetDatabaseOptions();
        _sut =  new TrsBillingBiayaListDal(opt);
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }

    [Fact]
    public void UT1_GivenValidRegID_WhenExecute_ThenReturnAsExpected()
    {
        var actual = _sut.ListData(RegType.Key("A"));
        actual.HasValue.Should().BeFalse();
    }
    
    [Fact]
    public void UT2_GivenValidRegID_WhenExecute_ThenReturnAsExpected()
    {
        using var trans = TransHelper.NewScope();
        const string sql = @"
                INSERT INTO ta_trs_billing (fs_kd_trs, fs_kd_reg, fs_kd_ref_biaya, fn_total, fn_modul)
                VALUES('TU1', 'RG1', 'A', 100, 0), 
                      ('DU2', 'RG1', 'B', 200, 1),
                      ('BE3', 'RG1', 'C', 400, 0)";
        _conn.Execute(sql);
        
        var actual = _sut.ListData(RegType.Key("RG1"));
        actual.HasValue.Should().BeTrue();
        actual.Value.Should().HaveCount(3);
        actual.Value.Should().ContainEquivalentOf(new TrsBillingBiayaDto("TU1", "A", 100, 0));
        actual.Value.Should().ContainEquivalentOf(new TrsBillingBiayaDto("DU2", "B", 200, 1));
        actual.Value.Should().ContainEquivalentOf(new TrsBillingBiayaDto("BE3", "C", 400, 2));
    }
}