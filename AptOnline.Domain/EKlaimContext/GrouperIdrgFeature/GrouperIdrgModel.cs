using AptOnline.Domain.EKlaimContext.IdrgFeature;
using AptOnline.Domain.SepContext.SepFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

public class GrouperIdrgModel
{
    private readonly List<GrouperIdrgDiagnosaType> _listDiagnosa;
    private readonly List<GrouperIdrgProsedurType> _listProsedur;

    public GrouperIdrgModel(string eKlaimId, SepRefference sep)
    {
        EKlaimId = eKlaimId;
        Sep = sep;
        _listDiagnosa = new List<GrouperIdrgDiagnosaType>();
        _listProsedur = new List<GrouperIdrgProsedurType>();
    }
    public string EKlaimId { get; init; }
    public SepRefference Sep { get; init; }
    public IEnumerable<GrouperIdrgDiagnosaType> ListDiagnosa  => _listDiagnosa;
    public IEnumerable<GrouperIdrgProsedurType> ListProsedur => _listProsedur;

    #region DIAGNOSA-RELATED METHOD
    public void Add(IdrgDiagnosaType diagnosa)
    {
        if (_listDiagnosa.Any(x => x.Idrg == diagnosa.ToRefference())) return;
        
        var noUrut = _listDiagnosa
            .DefaultIfEmpty(new GrouperIdrgDiagnosaType(0, IdrgRefferenceType.Default))
            .Max(x => x.NoUrut) + 1;
        var idrg = diagnosa.ToRefference();
        _listDiagnosa.Add(new GrouperIdrgDiagnosaType(noUrut, idrg));
    }
    public void Remove(IdrgDiagnosaType diagnosa)
    {
        _listDiagnosa.RemoveAll(x => x.Idrg == diagnosa.ToRefference());
        ReArrangeNoUrut();
    }
    public void UbahNoUrutDiagnosa(IdrgDiagnosaType idrg, int targetNoUrut)
    {
        //  rapihkan dahulu no urut (jangna ada yang lompat nomor)
        ReArrangeNoUrut();
        //  start shifting item
        targetNoUrut = Math.Max(1, Math.Min(targetNoUrut, _listDiagnosa.Count));
        var currentItem = _listDiagnosa.First(x => x.Idrg == idrg.ToRefference());
        _listDiagnosa.Remove(currentItem);
        _listDiagnosa.Insert(targetNoUrut - 1, currentItem);
        for (var i = 0; i < _listDiagnosa.Count; i++)
            _listDiagnosa[i].SetNoUrut(i + 1);
    }
    #endregion
    
    #region PROSEDUR-RELATED METHOD
    public void Add(IdrgProsedurType prosedur)
    {
        if (_listProsedur.Any(x => x.Idrg == prosedur.ToRefference())) return;
        
        var noUrut = _listProsedur
            .DefaultIfEmpty(new GrouperIdrgProsedurType(0, IdrgRefferenceType.Default,0,0))
            .Max(x => x.NoUrut) + 1;
        var idrg = prosedur.ToRefference();
        _listProsedur.Add(new GrouperIdrgProsedurType(noUrut, idrg, 0,0));
    }
    public void Remove(IdrgProsedurType prosedur)
    {
        _listProsedur.RemoveAll(x => x.Idrg == prosedur.ToRefference());
        ReArrangeNoUrut();
    }
    public void UbahNoUrutProsedur(IdrgProsedurType idrg, int targetNoUrut)
    {
        //  rapihkan dahulu no urut (jangna ada yang lompat nomor)
        ReArrangeNoUrut();
        //  start shifting item
        targetNoUrut = Math.Max(1, Math.Min(targetNoUrut, _listProsedur.Count));
        var currentItem = _listProsedur.First(x => x.Idrg == idrg.ToRefference());
        _listProsedur.Remove(currentItem);
        _listProsedur.Insert(targetNoUrut - 1, currentItem);
        for (var i = 0; i < _listProsedur.Count; i++)
            _listProsedur[i].SetNoUrut(i + 1);
    }
    #endregion
    
    private void ReArrangeNoUrut()
    {
        var i = 1;
        foreach (var item in _listDiagnosa.OrderBy(x => x.NoUrut))
        {
            item.SetNoUrut(_listDiagnosa.IndexOf(item) + 1);
            i++;
        }

        i = 1;
        foreach(var item in _listProsedur.OrderBy(x => x.NoUrut))
        {
            item.SetNoUrut(_listProsedur.IndexOf(item) + 1);
            i++;
        }
    }

}

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
}