using System.Data;
using System.Data.SqlClient;
using AptOnline.Domain.BillingContext.ParamSistemFeature;
using AptOnline.Domain.BillingContext.PegFeature;
using AptOnline.Infrastructure.BillingContext.PegFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.BillingContext.ParamSIstemFeature;

public class ParamSistemDalTest
{
    private readonly ParamSistemDal _sut;
    private readonly IDbConnection _conn;

    public ParamSistemDalTest()
    {
        var opt = FakeAppSetting.GetDatabaseOptions();
        _sut = new ParamSistemDal(opt);
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }

    [Fact]
    public void UT4_GetDataTest()
    {
        using var trans = TransHelper.NewScope();
        var expected = new ParamSistemType("A", "B", "C");
        const string sql = @"
            INSERT INTO tz_parameter_sistem
                (fs_kd_parameter, fs_nm_parameter, fs_value) 
            VALUES
                ('A', 'B', 'C')";
        _conn.Execute(sql);
        var actual = _sut.GetData(ParamSistemType.Key("A"));
        actual.Value.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void UT5_ListDataTest()
    {
        using var trans = TransHelper.NewScope();
        var expected = new ParamSistemType("A", "B", "C");
        const string sql = @"
            INSERT INTO tz_parameter_sistem
                (fs_kd_parameter, fs_nm_parameter, fs_value) 
            VALUES
                ('A', 'B', 'C')";
        _conn.Execute(sql);
        var actual = _sut.ListData();
        actual.Value.Should().Contain(new ParamSistemType("A", "B", "C"));  
    }
    
}