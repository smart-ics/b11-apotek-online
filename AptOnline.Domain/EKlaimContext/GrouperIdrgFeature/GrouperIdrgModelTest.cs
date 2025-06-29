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
        _sut = GrouperIdrgModel.Default;
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
    public void UT01_GivenEmptyDiagnosaList_WhenAddingFirstDiagnosa_ThenItShouldHaveNoUrut1()
    {
        //      ACT
        _sut.Add(FakeDiag1());
        _sut.ListDiagnosa.Count().Should().Be(1);
        _sut.ListDiagnosa.First().Idrg.Should().BeEquivalentTo(FakeDiag1().ToRefference());
        _sut.ListDiagnosa.First().NoUrut.Should().Be(1);
    }

    [Fact]
    public void UT02_GivenThreeDiagnosa_WhenChangingSecondToFirst_ThenOthersAreShiftedCorrectly()
    {
        //      ARRANGE
        _sut.Add(FakeDiag1());
        _sut.Add(FakeDiag2());
        _sut.Add(FakeDiag3());
        //      ACT
        _sut.UbahNoUrutDiagnosa(FakeDiag2(), 1);
        //      ASSERT
        _sut.ListDiagnosa.Count().Should().Be(3);
        _sut.ListDiagnosa.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakeDiag2().ToRefference());
        _sut.ListDiagnosa.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakeDiag1().ToRefference());
        _sut.ListDiagnosa.First(x => x.NoUrut == 3).Idrg.Should().BeEquivalentTo(FakeDiag3().ToRefference());
    }
    
    [Fact]
    public void UT03_GivenThreeDiagnosa_WhenChangingSecondToThird_ThenOthersAreShiftedCorrectly()
    {
        //      ARRANGE
        _sut.Add(FakeDiag1());
        _sut.Add(FakeDiag2());
        _sut.Add(FakeDiag3());
        //      ACT
        _sut.UbahNoUrutDiagnosa(FakeDiag2(), 3);
        //      ASSERT
        _sut.ListDiagnosa.Count().Should().Be(3);
        _sut.ListDiagnosa.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakeDiag1().ToRefference());
        _sut.ListDiagnosa.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakeDiag3().ToRefference());
        _sut.ListDiagnosa.First(x => x.NoUrut == 3).Idrg.Should().BeEquivalentTo(FakeDiag2().ToRefference());
    }
    
    [Fact]
    public void UT04_GivenExistingDiagnosa_WhenAddingSameDiagnosa_ThenItShouldNotBeAddedAgain()
    {
        //      ARRANGE
        _sut.Add(FakeDiag1());
        _sut.Add(FakeDiag2());
        //      ACT
        _sut.Add(FakeDiag2());
        //      ASSERT
        _sut.ListDiagnosa.Count().Should().Be(2);
    }
    
    [Fact]
    public void UT05_GivenDiagnosaList_WhenRemovingDiagnosa_ThenShouldRemoveItemAndReorderRemaining()
    {
        //      ARRANGE
        _sut.Add(FakeDiag1());
        _sut.Add(FakeDiag2());
        _sut.Add(FakeDiag3());
        //      ACT
        _sut.Remove(FakeDiag2());
        //      ASSERT
        _sut.ListDiagnosa.Count().Should().Be(2);
        _sut.ListDiagnosa.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakeDiag1().ToRefference());
        _sut.ListDiagnosa.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakeDiag3().ToRefference());
    }

    [Fact]
    public void UT06_GivenEmptyProsedurList_WhenAddingFirstProsedur_ThenItShouldHaveNoUrut1()
    {
        //      ACT
        _sut.Add(FakePros1());
        //      ASSERT
        _sut.ListProsedur.Count().Should().Be(1);
        _sut.ListProsedur.First().Idrg.Should().BeEquivalentTo(FakePros1().ToRefference());
        _sut.ListProsedur.First().NoUrut.Should().Be(1);
    }

    [Fact]
    public void UT07_GivenThreeProsedur_WhenChangingOrderToEarlier_ThenListShouldBeReorderedCorrectly()
    {
        //      ARRANGE
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        _sut.Add(FakePros3());
        //      ACT
        _sut.UbahNoUrutProsedur(FakePros2(), 1);
        //      ASSERT
        _sut.ListProsedur.Count().Should().Be(3);
        _sut.ListProsedur.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakePros2().ToRefference());
        _sut.ListProsedur.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakePros1().ToRefference());
        _sut.ListProsedur.First(x => x.NoUrut == 3).Idrg.Should().BeEquivalentTo(FakePros3().ToRefference());
    }
    
    [Fact]
    public void UT08_GivenThreeProsedur_WhenChangingOrderToLater_ThenListShouldBeReorderedCorrectly()
    {
        //      ARRANGE
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        _sut.Add(FakePros3());
        //      ACT
        _sut.UbahNoUrutProsedur(FakePros2(), 3);
        //      ASSERT
        _sut.ListProsedur.Count().Should().Be(3);
        _sut.ListProsedur.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakePros1().ToRefference());
        _sut.ListProsedur.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakePros3().ToRefference());
        _sut.ListProsedur.First(x => x.NoUrut == 3).Idrg.Should().BeEquivalentTo(FakePros2().ToRefference());
    }
    
    [Fact]
    public void UT09_GivenExistingProsedur_WhenAddingSameProsedur_ThenItShouldNotBeAddedAgain()
    {
        //      ARRANGE
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        //      ACT
        _sut.Add(FakePros2());
        //      ASSERT
        _sut.ListProsedur.Count().Should().Be(2);
    }
    
    [Fact]
    public void UT10_GivenProsedurList_WhenRemovingProsedur_ThenShouldRemoveItemAndReorderRemaining()
    {
        //      ARRANGE
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        _sut.Add(FakePros3());
        //      ACT
        _sut.Remove(FakePros2());
        //      ASSERT
        _sut.ListProsedur.Count().Should().Be(2);
        _sut.ListProsedur.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakePros1().ToRefference());
        _sut.ListProsedur.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakePros3().ToRefference());
    }

    [Fact]
    public void UT11_GivenExistingProsedur_WhenChangingMultiplicity_ThenValueShouldBeUpdated()
    {
        //      ARRANGE
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        //      ACT
        _sut.ChangeMulitiplicity(FakePros2(), 3);
        //      ASSERT
        var actual = _sut.ListProsedur.First(x => x.NoUrut == 2);
        actual.Multiplicity.Should().Be(3);
    }

    [Fact]
    public void UT12_GivenNonExistingProsedur_WhenChangingMultiplicity_ThenShouldThrowEx()
    {
        //      ARRANGE
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        //      ACT
        var actual = () =>_sut.ChangeMulitiplicity(FakePros3(), 3);
        //      ASSERT
        actual.Should().Throw<KeyNotFoundException>();
    }
    
    [Fact]
    public void UT13_GivenExistingProsedur_WhenChangingSetting_ThenSettingShouldBeUpdated()
    {
        //      ARRANGE
        _sut.Add(FakePros1());
        _sut.Add(FakePros2());
        //      ACT
        _sut.ChangeSetting(FakePros1(), 2);
        //      ASSERT
        var actual = _sut.ListProsedur.First(x => x.NoUrut == 1);
        actual.Setting.Should().Be(2);
    }

    [Fact]
    public void UT14_GivenNonExistingProsedur_WhenChangingSetting_ThenShouldThrowEx()
    {
        //      ARRANGE
        _sut.Add(FakePros1());
        _sut.Add(FakePros3());
        //      ACT
        var actual = () =>_sut.ChangeSetting(FakePros2(), 3);
        //      ASSERT
        actual.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void UT15_GivenNewGrouper_ThenPhaseShouldBeBelumGrouping()
    {
        //      ASSERT
        _sut.Phase.Should().Be(GroupingPhaseEnum.BelumGrouping);
    }

    [Fact]
    public void UT16_GivenDefaultMdc_WhenGrouping_ThenPhaseShouldBeGroupingTapiGagal()
    {
        //      ARRANGE
        var groupingResult = new GrouperIdrgResultType("A", "B", 
            MdcType.Default, 
            new DrgType("D1", "D2"), "E");
        //      ACT
        _sut.Grouping(groupingResult);
        //      ASSERT
        _sut.Phase.Should().Be(GroupingPhaseEnum.GroupingTapiGagal);
    }

    [Fact]
    public void UT17_GivenDefaultDrg_WhenGrouping_ThenPhaseShouldBeGroupingTapiGagal()
    {
        //      ARRANGE
        var groupingResult = new GrouperIdrgResultType("A", "B", 
            new MdcType("C1", "C2"), 
            DrgType.Default, "E");
        //      ACT
        _sut.Grouping(groupingResult);
        //      ASSERT
        _sut.Phase.Should().Be(GroupingPhaseEnum.GroupingTapiGagal);
    }

    [Fact]
    public void UT18_GivenValidDrgAndMdc_WhenGrouping_ThenPhaseShouldBeGroupingDanBerhasil()
    {
        //      ARRANGE
        var groupingResult = new GrouperIdrgResultType("A", "B", 
            new MdcType("C1", "C2"), 
            new DrgType("D1", "D2"), "E");
        //      ACT
        _sut.Grouping(groupingResult);
        //      ASSERT
        _sut.Phase.Should().Be(GroupingPhaseEnum.GroupingDanBerhasil);
    }

    [Fact]
    public void UT19_GivenPhaseGroupingDanGagal_WhenFinal_ThenShouldThrowEx()
    {
        //      ARRANGE
        var groupingResult = GrouperIdrgResultType.Default;
        _sut.Grouping(groupingResult);
        _sut.Phase.Should().Be(GroupingPhaseEnum.GroupingTapiGagal);
        //      ACT
        var actual = () => _sut.Final();
        //      ASSERT
        actual.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void UT20_GivenPhaseGroupingDanBerhasil_WhenFinal_ThenPhaseShouldBeFinal()
    {
        //      ARRANGE
        var groupingResult = new GrouperIdrgResultType("A", "B", 
            new MdcType("C1", "C2"), 
            new DrgType("D1", "D2"), "E");
        _sut.Grouping(groupingResult);
        _sut.Phase.Should().Be(GroupingPhaseEnum.GroupingDanBerhasil);
        //      ACT
        _sut.Final();
        //      ASSERT
        _sut.Phase.Should().Be(GroupingPhaseEnum.Final);
    }

    [Fact]
    public void UT21_GivenPhaseFinal_WhenDoingAnything_ThenShouldThrowEx()
    {
        //      ARRANGE
        var groupingResult = new GrouperIdrgResultType("A", "B",
            new MdcType("C1", "C2"),
            new DrgType("D1", "D2"), "E");
        _sut.Grouping(groupingResult);
        _sut.Final();
        //      ACT
        var listActions = new Action[]
        {
            () => _sut.Add(FakeDiag1()),
            () => _sut.Remove(FakeDiag1()),
            () => _sut.UbahNoUrutDiagnosa(FakeDiag1(), 1),
            () => _sut.Add(FakePros1()),
            () => _sut.Remove(FakePros1()),
            () => _sut.UbahNoUrutProsedur(FakePros1(), 1),
            () => _sut.ChangeSetting(FakePros1(), 1),
            () => _sut.ChangeMulitiplicity(FakePros1(), 1),
            () => _sut.Grouping(groupingResult)
        };
        //      ASSERT
        foreach (var item in listActions)
            item.Should().Throw<InvalidOperationException>();
    }
}