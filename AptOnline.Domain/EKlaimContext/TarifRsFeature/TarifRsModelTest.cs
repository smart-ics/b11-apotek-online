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
        result.AddReffBiaya("TU1", TdkBedah1, 100, JenisReffBiayaEnum.Jasa);
        result.AddReffBiaya("TU2", TdkBedah2, 200, JenisReffBiayaEnum.Jasa);
        result.AddReffBiaya("DU1", JualObat1, 300, JenisReffBiayaEnum.Obat);
        result.AddReffBiaya("DU2", JualObat2, 350, JenisReffBiayaEnum.Obat);
        result.AddReffBiaya("BE1", PakaiKamar1, 400, JenisReffBiayaEnum.Akomodasi);
        result.AddReffBiaya("BE2", PakaiKamarTidakMap, 500, JenisReffBiayaEnum.Akomodasi);
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
        var tarifRs = new TarifRsModel(RegType.Default.ToRefference());
        tarifRs.AddReffBiaya("TU1", TdkBedah1, 100,  JenisReffBiayaEnum.Jasa);
        tarifRs.AddReffBiaya("TU2", TdkBedah2, 200,  JenisReffBiayaEnum.Jasa);
        tarifRs.AddReffBiaya("DU1", JualObat1, 300,  JenisReffBiayaEnum.Obat);
        tarifRs.AddReffBiaya("DU2", JualObat2, 350,  JenisReffBiayaEnum.Obat);
        tarifRs.AddReffBiaya("BE1", PakaiKamar1, 400,  JenisReffBiayaEnum.Akomodasi);
        tarifRs.AddReffBiaya("BE2", PakaiKamarTidakMap, 500, JenisReffBiayaEnum.Akomodasi);
        
        _mapSkemaJknDalMock.Setup(x => x.GetData(TdkBedah1)).Returns(MayBe.From(MapBedah1Jkn));
        _mapSkemaJknDalMock.Setup(x => x.GetData(TdkBedah2)).Returns(MayBe.From(MapBedah2Jkn));
        _mapSkemaJknDalMock.Setup(x => x.GetData(JualObat1)).Returns(MayBe.From(MapObat1Jkn));
        _mapSkemaJknDalMock.Setup(x => x.GetData(JualObat2)).Returns(MayBe.From(MapObat2Jkn));
        _mapSkemaJknDalMock.Setup(x => x.GetData(PakaiKamar1)).Returns(MayBe.From(MapKamar1Jkn));   
        tarifRs.GenerateSkemaJkn(_mapSkemaJknDalMock.Object);

        tarifRs.ListSkema.Should().HaveCount(3);
        tarifRs.ListSkema.First(x => x.SkemaJkn == SkemaJknType.ProsedurBedah).Nilai.Should().Be(300);
        tarifRs.ListSkema.First(x => x.SkemaJkn == SkemaJknType.Obat).Nilai.Should().Be(650);
        tarifRs.ListSkema.First(x => x.SkemaJkn == SkemaJknType.Kamar).Nilai.Should().Be(400);
    }
    
}