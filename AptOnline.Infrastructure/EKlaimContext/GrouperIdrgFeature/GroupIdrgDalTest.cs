using AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;
using AptOnline.Domain.SepContext.SepFeature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Nuna.Lib.TransactionHelper;
using Nuna.Lib.ValidationHelper;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext.GrouperIdrgFeature;

public class GroupIdrgDalTest
{
    private readonly GrouperIdrgDal _sut;
    public GroupIdrgDalTest() 
        => _sut = new GrouperIdrgDal(FakeAppSetting.GetDatabaseOptions());
    private static GrouperIdrgModel Faker() 
        => new(
            "A", 
            new DateTime(2025,6,30), 
            new SepRefference("B", "C", new DateTime(2025,6,29)),
            new GrouperIdrgResultType(
                "C", 
                "D", 
                new MdcType("E", "F"), 
                new DrgType("G", "H"), 
                "I"),
            GroupingPhaseEnum.BelumGrouping, 
            new DateTime(2025,6,28),
            new List<GrouperIdrgDiagnosaType>(),
            new List<GrouperIdrgProsedurType>());            
    
    [Fact]
    public void UT1_InsertTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(new GrouperIdrgDto(Faker()));
    }
    
    [Fact]
    public void UT2_UpdateTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Update(new GrouperIdrgDto(Faker()));
    }
    
    [Fact]
    public void UT3_DeleteTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Delete(Faker());
    }
    
    [Fact]
    public void UT4_GetDataTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(new GrouperIdrgDto(Faker()));
        var actual = _sut.GetData(Faker());
        actual.Value.Should().BeEquivalentTo(new GrouperIdrgDto(Faker()),
            opt => opt
                .Excluding(x => x.SepId)
                .Excluding(x => x.SepDate));
    }

    [Fact]
    public void UT5_ListDataTest()
    {
        using var trans = TransHelper.NewScope();
        var faker = Faker();
        _sut.Insert(new GrouperIdrgDto(faker));
        var actual = _sut.ListData(new Periode(new DateTime(2025,6,30)));
        actual.Value.Should().ContainEquivalentOf(new GrouperIdrgDto(Faker()),
            opt => opt
                .Excluding(x => x.SepId)
                .Excluding(x => x.SepDate));
    }
}