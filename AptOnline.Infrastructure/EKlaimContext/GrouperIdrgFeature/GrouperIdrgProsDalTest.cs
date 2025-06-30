using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext.GrouperIdrgFeature;

public class GrouperIdrgProsDalTest
{
    private readonly GrouperIdrgProsDal _sut;

    private static GrouperIdrgProsDto Faker()
        => new GrouperIdrgProsDto
        {
            EKlaimId = "A",
            NoUrut = 1,
            IdrgId = "B",
            Im = true,
            IdrgName = "C",
            Multiplicity = 2,
            Setting = 3
        };
    
    public GrouperIdrgProsDalTest()
    {
        _sut = new GrouperIdrgProsDal(FakeAppSetting.GetDatabaseOptions());
    }
    
    [Fact]
    public void UT1_InsertTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(new List<GrouperIdrgProsDto> {Faker()});
    }

    [Fact]
    public void UT2_DeleteTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Delete(EKlaimModel.Key("A"));
    }
    
    [Fact]
    public void UT3_ListDataTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(new List<GrouperIdrgProsDto> {Faker()});
        var actual = _sut.ListData(EKlaimModel.Key("A"));
        actual.Value.Should().ContainEquivalentOf(Faker());
    }
    
    
}