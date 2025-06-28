using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EmrContext.AssesmentFeature;
using AptOnline.Domain.SepContext.KelasJknFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using FluentAssertions;
using Moq;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.EmrContext.AssesmentFeature;

public class AssesmentGetServiceTest
{
    private readonly AssesmentGetService _sut;
    private readonly Mock<IRegGetService> _regGetService;
    private readonly IDbConnection _conn;
    public AssesmentGetServiceTest()
    {
        var opt = FakeAppSetting.GetDatabaseOptions();
        _regGetService = new Mock<IRegGetService>();
        _sut = new AssesmentGetService(opt, _regGetService.Object);
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }

    [Fact]
    public void UT1_GivenValidRegID_WhenExecute_ThenReturnAsExpected()
    {
        using var trans = TransHelper.NewScope();
        var regKey = RegType.Key("RG007");
        var reg = new RegType("RG007", DateTime.Now, DateTime.Now,
            PasienType.Default, JenisRegEnum.RawatJalan, KelasJknType.Default,
            LayananType.Default.ToRefference());
        _regGetService.Setup(x => x.Execute(It.Is<IRegKey>(r => r.RegId == regKey.RegId))).Returns(reg);
        
        var sql = @"
                INSERT INTO SMASS_AssesmentConcept (
                    AssesmentId, ConceptId, Prompt, AssValue)
                VALUES (
                    'ASS001', 'CC001', 'Prompt', 'A')";
        _conn.Execute(sql);
        sql = @"INSERT INTO SMASS_Assesment (
                    AssesmentId, AssesmentDate, RegId)
                VALUES (
                    'ASS001', '2025-06-19', 'RG007')";
        _conn.Execute(sql);
        sql = @"INSERT INTO SMASS_Concept (
                    ConceptId, ConceptName)
                VALUES (
                    'CC001', 'ConceptName')";
        _conn.Execute(sql);
        
        var result = _sut.Execute(reg);

        result.HasValue.Should().BeTrue();
        result.Value.Reg.Should().BeEquivalentTo(reg.ToRefference());
        result.Value.ListAssesmentConcept.Should().HaveCount(1);
        result.Value.ListAssesmentConcept.First().AssesmentId.Should().Be("ASS001");
        result.Value.ListAssesmentConcept.First().AssesmentDate.Should().Be(DateTime.Parse("2025-06-19"));
        result.Value.ListAssesmentConcept.First().Concept.Should().Be(new ConceptType("CC001", "ConceptName", "Prompt"));
        result.Value.ListAssesmentConcept.First().AssValue.Should().Be("A");
    }
}