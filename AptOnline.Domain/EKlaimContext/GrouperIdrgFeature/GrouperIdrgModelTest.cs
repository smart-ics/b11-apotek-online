using AptOnline.Domain.EKlaimContext.IdrgFeature;
using AptOnline.Domain.SepContext.SepFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

public class GrouperIdrgModelTest
{
    private readonly GrouperIdrgModel _sut;
    public GrouperIdrgModelTest()
    {
        _sut = new GrouperIdrgModel(string.Empty, SepType.Default.ToRefference());
    }

    private static IdrgDiagnosaType FakeDiag1()
        => new IdrgDiagnosaType("A", "B", true, false, false);
    private static IdrgDiagnosaType FakeDiag2()
        => new IdrgDiagnosaType("C", "D", true, false, false);
    private static IdrgDiagnosaType FakeDiag3()
        => new IdrgDiagnosaType("E", "F", true, false, false);
    
    private static IdrgProsedurType FakePros1()
        => new IdrgProsedurType("U", "V", true);
    private static IdrgProsedurType FakePros2()
        => new IdrgProsedurType("W", "X", true);
    private static IdrgProsedurType FakePros3()
        => new IdrgProsedurType("Y", "Z", true);
    
    [Fact]
    public void UT01_GivenNewIdrg_WhenAddDiagnosa_ThenDiagnosaNoUrut1()
    {
        _sut.Add(FakeDiag1());
        _sut.ListDiagnosa.Count().Should().Be(1);
        _sut.ListDiagnosa.First().Idrg.Should().BeEquivalentTo(FakeDiag1().ToRefference());
        _sut.ListDiagnosa.First().NoUrut.Should().Be(1);
    }

    [Fact]
    public void UT02_Given3Diag_WhenSetNoUrutMengecil_ThenReArrange()
    {
        _sut.Add(FakeDiag1());
        _sut.Add(FakeDiag2());
        _sut.Add(FakeDiag3());
        _sut.UbahNoUrutDiagnosa(FakeDiag2(), 1);
        
        _sut.ListDiagnosa.Count().Should().Be(3);
        _sut.ListDiagnosa.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakeDiag2().ToRefference());
        _sut.ListDiagnosa.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakeDiag1().ToRefference());
        _sut.ListDiagnosa.First(x => x.NoUrut == 3).Idrg.Should().BeEquivalentTo(FakeDiag3().ToRefference());
    }
    
    [Fact]
    public void UT03_Given3Diag_WhenSetNoUrutMembesar_ThenReArrange()
    {
        _sut.Add(FakeDiag1());
        _sut.Add(FakeDiag2());
        _sut.Add(FakeDiag3());
        _sut.UbahNoUrutDiagnosa(FakeDiag2(), 3);
        
        _sut.ListDiagnosa.Count().Should().Be(3);
        _sut.ListDiagnosa.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakeDiag1().ToRefference());
        _sut.ListDiagnosa.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakeDiag3().ToRefference());
        _sut.ListDiagnosa.First(x => x.NoUrut == 3).Idrg.Should().BeEquivalentTo(FakeDiag2().ToRefference());
    }
    
    [Fact]
    public void UT04_GivenSameDiag_WhenAddDiagnosa_ThenNotAdded()
    {
        _sut.Add(FakeDiag1());
        _sut.Add(FakeDiag2());
        _sut.Add(FakeDiag2());
        
        _sut.ListDiagnosa.Count().Should().Be(2);
    }
    
    [Fact]
    public void UT05_GivenExisting_WhenRemoveDiagnosa_ThenRemoved()
    {
        _sut.Add(FakeDiag1());
        _sut.Add(FakeDiag2());
        _sut.Add(FakeDiag3());
        
        _sut.Remove(FakeDiag2());
        _sut.ListDiagnosa.Count().Should().Be(2);
        _sut.ListDiagnosa.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakeDiag1().ToRefference());
        _sut.ListDiagnosa.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakeDiag3().ToRefference());
    }

    
    //----------
    [Fact]
    public void UT06_GivenNewIdrg_WhenAddProsedur_ThenProsedurNoUrut1()
    {
        _sut.Add(FakePros1());
        _sut.ListProsedur.Count().Should().Be(1);
        _sut.ListProsedur.First().Idrg.Should().BeEquivalentTo(FakePros1().ToRefference());
        _sut.ListProsedur.First().NoUrut.Should().Be(1);
    }

    [Fact]
    public void UT07_Given3Pros_WhenSetNoUrutMengecil_ThenReArrange()
    {
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        _sut.Add(FakePros3());
        _sut.UbahNoUrutProsedur(FakePros2(), 1);
        
        _sut.ListProsedur.Count().Should().Be(3);
        _sut.ListProsedur.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakePros2().ToRefference());
        _sut.ListProsedur.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakePros1().ToRefference());
        _sut.ListProsedur.First(x => x.NoUrut == 3).Idrg.Should().BeEquivalentTo(FakePros3().ToRefference());
    }
    
    [Fact]
    public void UT08_Given3Pros_WhenSetNoUrutMembesar_ThenReArrange()
    {
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        _sut.Add(FakePros3());
        _sut.UbahNoUrutProsedur(FakePros2(), 3);
        
        _sut.ListProsedur.Count().Should().Be(3);
        _sut.ListProsedur.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakePros1().ToRefference());
        _sut.ListProsedur.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakePros3().ToRefference());
        _sut.ListProsedur.First(x => x.NoUrut == 3).Idrg.Should().BeEquivalentTo(FakePros2().ToRefference());
    }
    
    [Fact]
    public void UT09_GivenSamePros_WhenAddProsedur_ThenNotAdded()
    {
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        _sut.Add(FakePros2());
        
        _sut.ListProsedur.Count().Should().Be(2);
    }
    
    [Fact]
    public void UT10_GivenExisting_WhenRemoveProsedur_ThenRemoved()
    {
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        _sut.Add(FakePros3());
        
        _sut.Remove(FakePros2());
        _sut.ListProsedur.Count().Should().Be(2);
        _sut.ListProsedur.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakePros1().ToRefference());
        _sut.ListProsedur.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakePros3().ToRefference());
    }

    [Fact]
    public void UT11_GivenExisting_WhenMultiplicity_ThenMultiplicityChanged()
    {
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        _sut.ChangeMulitiplicity(FakePros2(), 3);

        var actual = _sut.ListProsedur.First(x => x.NoUrut == 2);
        actual.Multiplicity.Should().Be(3);
    }

    [Fact]
    public void UT12_GivenNotExistProsedur_WhenMultiplicity_ThenThrowEx()
    {
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        var actual = () =>_sut.ChangeMulitiplicity(FakePros3(), 3);

        actual.Should().Throw<KeyNotFoundException>();
    }
    
    [Fact]
    public void UT13_GivenExisting_WhenSetting_ThenSettingChanged()
    {
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        _sut.ChangeSetting(FakePros1(), 2);

        var actual = _sut.ListProsedur.First(x => x.NoUrut == 1);
        actual.Setting.Should().Be(2);
    }

    [Fact]
    public void UT14_GivenNotExistProsedur_WhenSetting_ThenThrowEx()
    {
        _sut.Add(FakePros1());
        _sut.Add(FakePros3());
        var actual = () =>_sut.ChangeSetting(FakePros2(), 3);

        actual.Should().Throw<KeyNotFoundException>();
    }    

}