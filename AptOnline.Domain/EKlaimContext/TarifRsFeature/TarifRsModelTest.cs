using AptOnline.Domain.BillingContext.RegAgg;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public class TarifRsModelTest
{
    private static ReffBiayaType TdkBedah1 => new ReffBiayaType("Bedah1", JenisReffBiayaEnum.Jasa);
    private static ReffBiayaType TdkBedah2 => new ReffBiayaType("Bedah2", JenisReffBiayaEnum.Jasa);
    private static ReffBiayaType JualObat1 => new ReffBiayaType("Obat1", JenisReffBiayaEnum.Obat);
    private static ReffBiayaType JualObat2 => new ReffBiayaType("Bedah2", JenisReffBiayaEnum.Obat);
    private static ReffBiayaType PakaiKamar1 => new ReffBiayaType("Kamar1", JenisReffBiayaEnum.Akomodasi);
    private static ReffBiayaType PakaiKamarTidakMap => new ReffBiayaType("Kamar2", JenisReffBiayaEnum.Akomodasi);
    private readonly Mock<IMapSkemaJknDal> _mapSkemaJknDalMock;

    public TarifRsModelTest()
    {
        _mapSkemaJknDalMock =  new Mock<IMapSkemaJknDal>();
    }

    private static TarifRsModel Reg1()
    {
        var result = new TarifRsModel(RegType.Default.ToRefference());
        result.AddReffBiaya("TU1", TdkBedah1, "Hecting", 100);
        result.AddReffBiaya("TU2", TdkBedah2, "Cabut Gigi", 200);
        result.AddReffBiaya("DU1", JualObat1, "Amoxan",300);
        result.AddReffBiaya("DU2", JualObat2, "Dipometan", 350);
        result.AddReffBiaya("BE1", PakaiKamar1, "ICU", 400);
        result.AddReffBiaya("BE2", PakaiKamarTidakMap, "Anggrek 3",500);
        return result;
    }

    private static MapSkemaJknType MapBedah1Jkn => new(TdkBedah1, SkemaJknType.ProsedurBedah);
    private static MapSkemaJknType MapBedah2Jkn => new (TdkBedah2, SkemaJknType.ProsedurBedah);
    private static MapSkemaJknType MapObat1Jkn => new (JualObat1, SkemaJknType.Obat);
    private static MapSkemaJknType MapObat2Jkn => new (JualObat2, SkemaJknType.Obat);
    private static MapSkemaJknType MapKamar1Jkn => new (PakaiKamar1, SkemaJknType.Kamar);

    [Fact]
    public void UT1_GivenValidTrsBilling_WhenCreate_ThenReturAsExpected()
    {
        var tarifRs = Reg1();
        
        _mapSkemaJknDalMock.Setup(x => x.GetData(TdkBedah1)).Returns(MayBe.From(MapBedah1Jkn));
        _mapSkemaJknDalMock.Setup(x => x.GetData(TdkBedah2)).Returns(MayBe.From(MapBedah2Jkn));
        _mapSkemaJknDalMock.Setup(x => x.GetData(JualObat1)).Returns(MayBe.From(MapObat1Jkn));
        _mapSkemaJknDalMock.Setup(x => x.GetData(JualObat2)).Returns(MayBe.From(MapObat2Jkn));
        _mapSkemaJknDalMock.Setup(x => x.GetData(PakaiKamar1)).Returns(MayBe.From(MapKamar1Jkn));   
        tarifRs.GenerateSkemaJkn(_mapSkemaJknDalMock.Object);

        tarifRs.ListSkema.Should().HaveCount(4);
        tarifRs.ListSkema.First(x => x.SkemaJkn == SkemaJknType.ProsedurBedah).Nilai.Should().Be(300);
        tarifRs.ListSkema.First(x => x.SkemaJkn == SkemaJknType.Obat).Nilai.Should().Be(650);
        tarifRs.ListSkema.First(x => x.SkemaJkn == SkemaJknType.Kamar).Nilai.Should().Be(400);
        tarifRs.ListSkema.First(x => x.SkemaJkn == SkemaJknType.Default).Nilai.Should().Be(500);
    }
    
}