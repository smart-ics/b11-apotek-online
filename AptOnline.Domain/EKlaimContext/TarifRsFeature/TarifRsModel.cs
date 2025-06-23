using AptOnline.Domain.BillingContext.RegAgg;
using Ardalis.GuardClauses;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public class TarifRsModel
{
    private readonly List<TarifRsReffBiayaType> _listReffBiaya;
    private readonly List<TarifRsSkemaJknModel> _listSkema;

    public TarifRsModel(RegRefference reg)
    {
        Guard.Against.Null(reg, nameof(reg));
        
        Reg = reg;
        _listReffBiaya = new List<TarifRsReffBiayaType>();
        _listSkema = new  List<TarifRsSkemaJknModel>();
        
    }
    public RegRefference Reg { get; init; }
    public IEnumerable<TarifRsReffBiayaType> ListTarif => _listReffBiaya;
    public IEnumerable<TarifRsSkemaJknModel> ListSkema => _listSkema;
    
    
    public void AddReffBiaya(string trsId, ReffBiayaType reffBiaya, 
        decimal nilai)
    {
        Guard.Against.NullOrWhiteSpace(trsId, nameof(trsId));
        Guard.Against.Null(reffBiaya, nameof(reffBiaya));
        _listReffBiaya.Add(new TarifRsReffBiayaType(trsId, reffBiaya, nilai));
    }
    
    public void GenerateSkemaJkn(IEnumerable<MapSkemaJknType> mapSkemaJknType)
    {
        var listMap = mapSkemaJknType.ToList();
        foreach (var item in _listReffBiaya)
        {
            var skemaMap = listMap.FirstOrDefault(x => x.ReffBiaya == item.ReffBiaya);
            if (skemaMap is null)
                continue;
            
            var skema = _listSkema.FirstOrDefault(x => x.SkemaJkn == skemaMap.SkemaJkn);
            if (skema is null)
            {
                skema = new TarifRsSkemaJknModel(skemaMap.SkemaJkn);
                _listSkema.Add(skema);
            }
            
            skema.AddReffBiaya(item);
        }
    }

    public static TarifRsModel Default => new TarifRsModel(RegType.Default.ToRefference());
}

public class TarifRsTest
{
     private static ReffBiayaType TdkBedah1 => new ReffBiayaType("Bedah1", JenisReffBiayaEnum.Jasa);
     private static ReffBiayaType TdkBedah2 => new ReffBiayaType("Bedah2", JenisReffBiayaEnum.Jasa);
     private static ReffBiayaType JualObat1 => new ReffBiayaType("Obat1", JenisReffBiayaEnum.Obat);
     private static ReffBiayaType PakaiKamar1 => new ReffBiayaType("Kamar1", JenisReffBiayaEnum.Akomodasi);
     private static ReffBiayaType PakaiKamarTidakMap => new ReffBiayaType("Kamar2", JenisReffBiayaEnum.Akomodasi);
     
     private static TarifRsModel Reg1()
     {
         var result = new TarifRsModel(RegType.Default.ToRefference());
         result.AddReffBiaya("TU1", TdkBedah1, 100);
         result.AddReffBiaya("TU2", TdkBedah2, 200);
         result.AddReffBiaya("DU1", JualObat1, 300);
         result.AddReffBiaya("BE1", PakaiKamar1, 400);
         result.AddReffBiaya("BE2", PakaiKamarTidakMap, 500);
         return result;
     }

     private static MapSkemaJknType MapBedah1Jkn => new (TdkBedah1, SkemaJknType.ProsedurBedah);
     private static MapSkemaJknType MapBedah2Jkn => new (TdkBedah2, SkemaJknType.ProsedurBedah);
     private static MapSkemaJknType MapObat1Jkn => new (JualObat1, SkemaJknType.Obat);
     private static MapSkemaJknType MapKamar1Jkn => new (PakaiKamar1, SkemaJknType.Kamar);

     [Fact]
     public void UT1_GivenValidTrsBilling_WhenCreate_ThenReturAsExpected()
     {
        var tarifRs = new TarifRsModel(RegType.Default.ToRefference());
        tarifRs.AddReffBiaya("TU1", TdkBedah1, 100);
        tarifRs.AddReffBiaya("TU2", TdkBedah2, 200);
        tarifRs.AddReffBiaya("DU1", JualObat1, 300);
        tarifRs.AddReffBiaya("BE1", PakaiKamar1, 400);
        tarifRs.AddReffBiaya("BE2", PakaiKamarTidakMap, 500);
        tarifRs.GenerateSkemaJkn(new []{ MapBedah1Jkn, MapBedah2Jkn, MapObat1Jkn, MapKamar1Jkn});

        tarifRs.ListSkema.Should().HaveCount(3);
        tarifRs.ListSkema.First(x => x.SkemaJkn == SkemaJknType.ProsedurBedah).Nilai.Should().Be(300);
        tarifRs.ListSkema.First(x => x.SkemaJkn == SkemaJknType.Obat).Nilai.Should().Be(300);
        tarifRs.ListSkema.First(x => x.SkemaJkn == SkemaJknType.Kamar).Nilai.Should().Be(400);
     }
    
}