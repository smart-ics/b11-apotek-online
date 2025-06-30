using System.Data;
using System.Data.SqlClient;
using AptOnline.Domain.EKlaimContext.IdrgFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext.IdrgFeature;

public class IdrgDalTest
{
    private readonly IdrgDal _sut;
    private readonly IDbConnection _conn;
    public IdrgDalTest()
    {
        var opt = FakeAppSetting.GetDatabaseOptions();
        _sut = new IdrgDal(opt);
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    private static IdrgDiagnosaType DiagnosaFake()
        => new IdrgDiagnosaType("A", "Curut", false, true, false);

    private static IdrgProsedurType ProsedurFake()
        => new IdrgProsedurType("A", "Cicak", false);
    
    [Fact]
    public void UT1_GetDataTest()
    {
        using var trans = TransHelper.NewScope();
        const string sql = @"
            INSERT INTO STD_Idrg (IdrgId, Code2, IdrgName, StdSystem, 
                    Kategori,ValidCode, Accpdx, Asterisk, Im)
            VALUES ('A', 'B', 'Curut', 'D', 0, 1, 'Y', 0, 0)";
        _conn.Execute(sql);
        var actual = _sut.GetData(IdrgAbstract.Key("A", false)).Value;
        actual.Should().BeEquivalentTo(DiagnosaFake()); 
    }
    
    [Fact]
    public void UT2_SearchProsedureTest()
    {
        using var trans = TransHelper.NewScope();
        const string sql = @"
            INSERT INTO STD_Idrg (IdrgId, Code2, IdrgName, StdSystem, 
                    Kategori, ValidCode, Accpdx, Asterisk, Im)
            VALUES ('A', 'B', 'Cicak', 'D', 1, 1, 'Y', 0, 0)";
        _conn.Execute(sql);
        var actual = _sut.SearchProsedur("Ci").Value;
        actual.Should().Contain(ProsedurFake()); 
    }
    
    [Fact]
    public void UT3_SearchDiagnosaTest()
    {
        using var trans = TransHelper.NewScope();
        const string sql = @"
            INSERT INTO STD_Idrg (IdrgId, Code2, IdrgName, StdSystem, 
                    Kategori, ValidCode, Accpdx, Asterisk, Im)
            VALUES ('A', 'B', 'Curut', 'D', 0, 1, 'Y', 0, 0)";
        _conn.Execute(sql);
        var actual = _sut.SearchDiagnosa("RUT").Value;
        actual.Should().Contain(DiagnosaFake()); 
    }    
}