using AptOnline.Domain.EKlaimContext.IdrgFeature;
using AptOnline.Domain.SepContext.SepFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

public class GrouperIdrgModel
{
    private readonly List<GrouperIdrgDiagnosaType> _listDiagnosa;
    private readonly List<GrouperIdrgProsedurType> _listProsedur;

    public GrouperIdrgModel()
    {
        _listDiagnosa = new List<GrouperIdrgDiagnosaType>();
        _listProsedur = new List<GrouperIdrgProsedurType>();
    }
    public string EKlaimId { get; init; }
    public SepRefference Sep { get; init; }
    public IEnumerable<GrouperIdrgDiagnosaType> ListDiagnosa  => _listDiagnosa;
    public IEnumerable<GrouperIdrgProsedurType> ListProsedur => _listProsedur;

    //  TODO: Coba dibuat generic => bisa untuk add Diagnosa dan Prosedur
    public void AddDiagnosa(IdrgAbstract diagnosa)
    {
        if (diagnosa is not IdrgDiagnosaType dx) return;
        if (_listDiagnosa.Any(x => x.Idrg == dx.ToRefference())) return;
        var noUrut = _listDiagnosa
            .DefaultIfEmpty(new GrouperIdrgDiagnosaType(0, IdrgRefferenceType.Default))
            .Max(x => x.NoUrut) + 1;
        var idrg = dx.ToRefference();
        _listDiagnosa.Add(new GrouperIdrgDiagnosaType(noUrut, idrg));
    }
    public void UbahNoUrutDiagnosa(IdrgRefferenceType idrg, int targetNoUrut)
    {
        //  rapihkan dahulu no urut (jangna ada yang lompat nomor)
        var noUrut = 1;
        foreach (var item in _listDiagnosa.OrderBy(x => x.NoUrut))
        {
            item.SetNoUrut(noUrut);
            noUrut++;
        }
        //  start re-arrange
        targetNoUrut = Math.Max(1, Math.Min(targetNoUrut, _listDiagnosa.Count));
        var currentItem = _listDiagnosa.First(x => x.Idrg == idrg);
        _listDiagnosa.Remove(currentItem);
        _listDiagnosa.Insert(targetNoUrut - 1, currentItem);
        for (var i = 0; i < _listDiagnosa.Count; i++)
            _listDiagnosa[i].SetNoUrut(i + 1);
    }
}

public class GrouperIdrgModelTest
{
    private static IdrgDiagnosaType FakeDiag1()
        => new IdrgDiagnosaType("A", "B", true, false, false);
    private static IdrgDiagnosaType FakeDiag2()
        => new IdrgDiagnosaType("C", "D", true, false, false);
    private static IdrgDiagnosaType FakeDiag3()
        => new IdrgDiagnosaType("E", "F", true, false, false);
    
    [Fact]
    public static void UT1_GivenNewIdrg_WhenAddDiagnosa_ThenDiagnosaNoUrut1()
    {
        var sut = new GrouperIdrgModel();
        sut.AddDiagnosa(FakeDiag1());
        sut.ListDiagnosa.Count().Should().Be(1);
        sut.ListDiagnosa.First().Idrg.Should().BeEquivalentTo(FakeDiag1().ToRefference());
        sut.ListDiagnosa.First().NoUrut.Should().Be(1);
    }

    [Fact]
    public static void UT2_Given3Diag_WhenSetNoUrutMengecil_ThenReArrange()
    {
        var sut = new GrouperIdrgModel();
        sut.AddDiagnosa(FakeDiag1());
        sut.AddDiagnosa(FakeDiag2());
        sut.AddDiagnosa(FakeDiag3());
        sut.UbahNoUrutDiagnosa(FakeDiag2().ToRefference(), 1);
        
        sut.ListDiagnosa.Count().Should().Be(3);
        sut.ListDiagnosa.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakeDiag2().ToRefference());
        sut.ListDiagnosa.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakeDiag1().ToRefference());
        sut.ListDiagnosa.First(x => x.NoUrut == 3).Idrg.Should().BeEquivalentTo(FakeDiag3().ToRefference());
    }
    
    [Fact]
    public static void UT3_Given3Diag_WhenSetNoUrutMembesar_ThenReArrange()
    {
        var sut = new GrouperIdrgModel();
        sut.AddDiagnosa(FakeDiag1());
        sut.AddDiagnosa(FakeDiag2());
        sut.AddDiagnosa(FakeDiag3());
        sut.UbahNoUrutDiagnosa(FakeDiag2().ToRefference(), 3);
        
        sut.ListDiagnosa.Count().Should().Be(3);
        sut.ListDiagnosa.First(x => x.NoUrut == 1).Idrg.Should().BeEquivalentTo(FakeDiag1().ToRefference());
        sut.ListDiagnosa.First(x => x.NoUrut == 2).Idrg.Should().BeEquivalentTo(FakeDiag3().ToRefference());
        sut.ListDiagnosa.First(x => x.NoUrut == 3).Idrg.Should().BeEquivalentTo(FakeDiag2().ToRefference());
    }
    
    [Fact]
    public static void UT4_GivenSameDiag_WhenAddDiagnosa_ThenNotAdded()
    {
        var sut = new GrouperIdrgModel();
        sut.AddDiagnosa(FakeDiag1());
        sut.AddDiagnosa(FakeDiag2());
        sut.AddDiagnosa(FakeDiag2());
        
        sut.ListDiagnosa.Count().Should().Be(2);
    }    

    
}