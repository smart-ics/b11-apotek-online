using AptOnline.Application.AptolCloudContext.PpkAgg;
using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Application.Helpers;
using AptOnline.Application.PharmacyContext.MapDphoAgg;
using AptOnline.Application.SepContext;
using AptOnline.Application.SepContext.SepFeature;
using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Domain.AptolCloudContext.PpkAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.Helpers;
using AptOnline.Domain.PharmacyContext.BrgAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using AptOnline.Domain.SepContext.AssesmentPelayananFeature;
using AptOnline.Domain.SepContext.FaskesFeature;
using AptOnline.Domain.SepContext.JenisPelayananFeature;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using AptOnline.Domain.SepContext.SkdpFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Nuna.Lib.ValidationHelper;
using Xunit;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsValidateUseCase;

public class ResepRsValidateTest
{
    /* test case:
     A. Happy Path Testing
        1. Resep Single
            T01. All DPHO : 1 DPHO => 0 ValidationNote
            T02. Campuran : 1 DPHO + 1 Non DPHO  => 1 ValidationNote
        2. Resep Racik
            T03. All DPHO : 1 DPHO  => 0 ValidationNote
            T04. Campuran : 1 DPHO + 1 Non DPHO => 1 ValidationNote
        3. Resep Kombinasi
            T05. All DPHO : 2 DPHO         | 1 DPHO => 0 ValidationNote
            T06. Campuran : 1 DPHO + 1 Non | 1 DPHO + 1 Non DPHO => 2 ValidationNote

     B. Negatif Testing
        T07. Single All Non-DPHO
        T08. Racik All Non-DPHO
        T09. Sep Not Found
        T10. Reg Not Found
        T11. Faskes Not Found
        T12. Layanan Not Found
     */
    private readonly ResepRsValidateHandler _sut;
    private readonly Mock<IResepMidwareWriter> _writer;
    private readonly Mock<ISepDal> _sepDal;
    private readonly Mock<IPpkGetService> _ppkGetService;
    private readonly Mock<ILayananGetService> _layananGetService;
    private readonly Mock<IMapDphoGetService> _mapDphoGetService;
    private readonly Mock<ITglJamProvider> _dateTime;

    public ResepRsValidateTest()
    {
        _writer = new Mock<IResepMidwareWriter>();
        _sepDal = new Mock<ISepDal>();
        _ppkGetService = new Mock<IPpkGetService>();
        _layananGetService = new Mock<ILayananGetService>();
        _mapDphoGetService = new Mock<IMapDphoGetService>();
        _dateTime = new Mock<ITglJamProvider>();
        _sut = new ResepRsValidateHandler(
            _sepDal.Object,
            _ppkGetService.Object,
            _layananGetService.Object,
            _mapDphoGetService.Object,
            _writer.Object,
            _dateTime.Object);
    }

    private static MayBe<SepType> SepFaker()
    {
        var pesertaBpjs = PesertaBpjsType.Create("A", "B", "", JenisPesertaType.Default, KelasRawatType.Default, FaskesType.Default.ToRefference());
        var result  = MayBe
            .From(new SepType(
                "SEP-ID-1", new DateTime(2025,4,1), "SEP-NO-1", pesertaBpjs.ToRefference(), FaskesType.Default, 
                JenisPelayananType.Default, AssesmentPelayananType.Default, SkdpRefference.Default, 
                RegType.Load("REG-1", new DateTime(2025,4,1), new DateTime(3000,1,1), PasienType.Default, JenisRegEnum.Unknown, KelasRawatType.Default),
                new DokterType("DOKTER-ID-1", "DOKTER-NAME-1"), DokterType.Default,  false, "-"));
        return result;
    }
    
    private static PpkType PpkFaker()
        => new PpkType(
            "PPK-ID-1", "PPK-NAME-1","-","-","-",
            KepalaType.Default, 
            VerifikatorType.Default, 
            ApotekType.Default);
    private static LayananType LayananFaker()
        => new LayananType(
            "LYN-ID-1", "LYN-NAME-1", true,
            PoliBpjsType.Default);
    private static BrgType BrgDphoFaker()
        => new BrgType("BRG-ID-1", "BRG-NAME-1");
    private static BrgType BrgNonDphoFaker()
        => new BrgType("BRG-ID-2", "BRG-NAME-2");
    
    private static DphoType DphoFaker()
        => new DphoType("DPHO-ID-1", "DPHO-NAME-1",
            "","", "", 0,"","",false);
    private static MapDphoType MapDphoFaker()
        => new MapDphoType(BrgDphoFaker(), DphoFaker().ToRefference());
    
    private static ResepRsValidateCommand Command()
        => new ResepRsValidateCommand("RG-001", "LYN-ID-1",
            new List<ResepRsValidateCommandResep>
            {
                new ResepRsValidateCommandResep("RESEP-1",
                    new List<ResepRsValidateCommandObat>())
            });

    private static ResepMidwareModel ResepMidwareFaker()
        => new ResepMidwareModel(new DateTime(2025, 1, 1), 1, SepType.Default,
            PpkType.Default.ToRefference(), PoliBpjsType.Default);

    
    private void HappyPathMocking()
    {
        _sepDal
            .Setup(x => x.GetData(It.IsAny<IRegKey>()))
            .Returns(SepFaker());
        _ppkGetService
            .Setup(x => x.Execute())
            .Returns(PpkFaker());
        _layananGetService
            .Setup(x => x.Execute(It.IsAny<ILayananKey>()))
            .Returns(LayananFaker());
        _mapDphoGetService
            .Setup(x => x.Execute(It.Is<IBrgKey>(y => y.BrgId == "BRG-ID-1")))
            .Returns(MapDphoFaker());
        _writer
            .Setup(x => x.Save(It.IsAny<ResepMidwareModel>()))
            .Returns(ResepMidwareFaker());
        _dateTime
            .Setup(x => x.Now)
            .Returns(new DateTime(2025, 1, 1));
    }
    
    [Fact]
    public async Task T01_GivenSingleAllDpho_ThenSuccess_AndNoValidationNote()
    {
        //  ARRANGE
        var cmd = Command();
        cmd.ListResep.First().ListItem.Add(
            new ResepRsValidateCommandObat("BRG-ID-1", "BRG-NAME-1", 1, "2dd1", "CaraPakai", 1,
                new List<ResepRsValidateCommandKomponenRacik>()));
        HappyPathMocking();
        var expected = new List<ResepRsValidateResponse>
        {
            new(1, "xxx", "SUCCESS", "")
        };        
        //  ACT
        var result = await _sut.Handle(cmd, CancellationToken.None);
        //  ASSERT
        var resultFetched = result.ToList();
        resultFetched.Should().BeEquivalentTo(expected, opt => opt.Excluding(x => x.ResepMidwareId));
    }
    
    [Fact]
    public async Task T02_GivenSingle1Dpho_And1NonDpho_ThenSuccess_AndGet1ValidationNote()
    {
        //  ARRANGE
        var cmd = Command();
        cmd.ListResep.First().ListItem.Add(
            new ResepRsValidateCommandObat("BRG-ID-1", "BRG-NAME-1", 1, "2dd1", "CaraPakai", 1,
                new List<ResepRsValidateCommandKomponenRacik>()));
        cmd.ListResep.First().ListItem.Add(
            new ResepRsValidateCommandObat("BRG-ID-2", "BRG-NAME-2", 1, "2dd1", "CaraPakai", 1,
                new List<ResepRsValidateCommandKomponenRacik>()));
        var expected = new List<ResepRsValidateResponse>
        {
            new(1, "xxx", "SUCCESS", "'BRG-NAME-2' tidak masuk dalam daftar DPHO")
        };
        HappyPathMocking();
        //  ACT
        var result = await _sut.Handle(cmd, CancellationToken.None);
        //  ASSERT
        var resultFetched = result.ToList();
        resultFetched.Should().BeEquivalentTo(expected, opt => opt.Excluding(x => x.ResepMidwareId));
    }

    [Fact]
    public async Task T03_GivenRacikAllDpho_ThenSuccess_AndNoValidationNote()
    {
        //  ARRANGE
        var cmd = Command();
        cmd.ListResep.First().ListItem.Add(
            new ResepRsValidateCommandObat("RACIK-ID-1",
                "RACIK-NAME-1", 1, "2dd1", "CaraPakai", 1,
                new List<ResepRsValidateCommandKomponenRacik>
                {
                    new("BRG-ID-1", "BRG-NAME-1", 10, "3dd1", "diminum")                    
                }));
        var expected = new List<ResepRsValidateResponse>
        {
            new(1, "xxx", "SUCCESS", "")
        };
        HappyPathMocking();
        //  ACT
        var result = await _sut.Handle(cmd, CancellationToken.None);
        //  ASSERT
        var resultFetched = result.ToList();
        resultFetched.Should().BeEquivalentTo(expected, opt => opt.Excluding(x => x.ResepMidwareId));
    }
    
    [Fact]
    public async Task T04_GivenRacikDpho_And1NonDpho_ThenSuccess_AndGet1ValidationNote()
    {
        //  ARRANGE
        var cmd = Command();
        cmd.ListResep.First().ListItem.Add(
            new ResepRsValidateCommandObat("RACIK-ID-1",
                "RACIK-NAME-1", 1, "2dd1", "CaraPakai", 1,
                new List<ResepRsValidateCommandKomponenRacik>
                {
                    new("BRG-ID-1", "BRG-NAME-1", 10, "3dd1", "diminum"),
                    new("BRG-ID-2", "BRG-NAME-2", 8, "2dd1", "diminum")
                }));
        HappyPathMocking();
        var expected = new List<ResepRsValidateResponse>
        {
            new(1, "xxx", "SUCCESS", "'BRG-NAME-2' tidak masuk dalam daftar DPHO")
        };

        //  ACT
        var result = await _sut.Handle(cmd, CancellationToken.None);
        
        //  ASSERT
        var resultFetched = result.ToList();
        resultFetched.Should().BeEquivalentTo(expected, opt => opt.Excluding(x => x.ResepMidwareId));
    }}