using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.SepContext.AssesmentPelayananFeature;
using AptOnline.Domain.SepContext.FaskesFeature;
using AptOnline.Domain.SepContext.JenisPelayananFeature;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using AptOnline.Domain.SepContext.SkdpFeature;
using AptOnline.Domain.SepContext.TipeFaskesFeature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.SepContext.SepFeature;

public class SepDalTest
{
    private readonly SepDal _sut;

    public SepDalTest()
    {
        var opt = FakeAppSetting.GetDatabaseOptions();
        _sut = new SepDal(opt);
    }

    private static SepType Faker()
    {
        var reg = RegType.Load("RG01376985",
            new DateTime(2024, 6, 6, 23, 26, 31), 
            new DateTime(3000,1,1, 0, 0, 0),
            PasienType.Load("337502200146916", "KEISHA KHALIQA RIZQI", new DateTime(3000,1,1), GenderType.Default),
            JenisRegEnum.RawatJalan, KelasRawatType.Default);
        var dokter = new DokterType("226634", "dr. Dwi Riyanto, Sp.A");
        var dokterLayanan = DokterType.Default;
        var faskesPerujuk = new FaskesType("010", "RSUD DR. H. RACHMAT HIDAYATULLAH", new TipeFaskesType("1", "-"));
        var jenisPelayanan = new JenisPelayananType("1", "-");
        var assesmentPelayanan = new AssesmentPelayananType("2", "-");
        var skdp = new SkdpRefference("1104R0040624V002150");
        var pesertaBpjs = PesertaBpjsType.Default with { PesertaBpjsId = "0002278745223" };
        var result = new SepType("JP00580127",
            new DateTime(2024, 6, 6, 23, 26, 31), "1104R0040624V002150",
            pesertaBpjs.ToRefference(), faskesPerujuk, jenisPelayanan,
            assesmentPelayanan, skdp, reg, dokter, dokterLayanan, 
            true, string.Empty);
        return result;
    } 

    [Fact]
    public void ExecuteTest()
    {
        using var trans = TransHelper.NewScope();   
        _sut.Insert(Faker());
        var act = _sut.GetData(RegType.Key("RG01376985"));
        act.Value.Should().BeEquivalentTo(Faker());
    }
    
}