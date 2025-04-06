using AptOnline.Application.AptolCloudContext.PpkAgg;
using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Application.BillingContext.SepAgg;
using AptOnline.Application.PharmacyContext.MapDphoAgg;
using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Domain.AptolCloudContext.PpkAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using AptOnline.Domain.PharmacyContext.BrgAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using FluentAssertions;
using Moq;
using Xunit;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsValidateUseCase;

public class ResepRsValidateTest
{
    /* test case:
     A. Happy Path Testing
        1. Resep Single
            T01. All DPHO : 2 DPHO => 0 ValidationNote
            T02. Campuran : 2 DPHO + 1 Non DPHO  => 1 ValidationNote
        2. Resep Racik
            T03. All DPHO : 2 DPHO  => 0 ValidationNote
            T04. Campuran : 2 DPHO + 1 Non DPHO => 1 ValidationNote
        3. Resep Kombinasi
            T05. All DPHO : 2 DPHO         | 1 DPHO => 0 ValidationNote
            T06. Campuran : 1 DPHO + 1 Non | 1 DPHO + 1 Non DPHO => 2 ValidationNote

     B. Negatif Testing
        T07. Single All DPHO
        T08. Racik All DPHO
        T09. Sep Not Found
        T10. Reg Not Found
        T11. Faskes Not Found
        T12. Layanan Not Found
     */
    private readonly ResepRsValidateHandler _sut;
    private readonly Mock<IResepMidwareWriter> _writer;
    private readonly Mock<ISepGetByRegService> _sepGetByRegService;
    private readonly Mock<IPpkGetService> _ppkGetService;
    private readonly Mock<ILayananGetService> _layananGetService;
    private readonly Mock<IMapDphoGetService> _mapDphoGetService;

    public ResepRsValidateTest()
    {
        _writer = new Mock<IResepMidwareWriter>();
        _sepGetByRegService = new Mock<ISepGetByRegService>();
        _ppkGetService = new Mock<IPpkGetService>();
        _layananGetService = new Mock<ILayananGetService>();
        _mapDphoGetService = new Mock<IMapDphoGetService>();
        _sut = new ResepRsValidateHandler(
            _sepGetByRegService.Object,
            _ppkGetService.Object,
            _layananGetService.Object,
            _mapDphoGetService.Object,
            _writer.Object);
    }

    private static SepType SepFaker()
        => new SepType(
            "SEP-ID-1", new DateTime(2025,4,1), "SEP-NO-1", "PESERTA-1", 
            new RegType("REG-1", new DateTime(2025,4,1), "PASIEN-1", "PASIEN-NAME-1"),
            new DokterType("DOKTER-ID-1", "DOKTER-NAME-1"),
            false, "-");
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

    private static ResepMidwareModel ResepMidware1Faker()
        =>  new ResepMidwareModel {ResepMidwareId = "RESEP-1"};

    private static ResepMidwareModel ResepMidware2Faker()
        =>  new ResepMidwareModel {ResepMidwareId = "RESEP-2"};
    

    
    private void HappyPathMocking()
    {
        _sepGetByRegService
            .Setup(x => x.Execute(It.IsAny<IRegKey>()))
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
            .SetupSequence(x => x.Save(It.IsAny<ResepMidwareModel>()))
            .Returns(ResepMidware1Faker())
            .Returns(ResepMidware2Faker());
    }
    
    [Fact]
    public async Task T01_GivenSingleAllDpho_ThenSuccess_AndNoValidationNote()
    {
        var cmd = Command();
        cmd.ListResep.First().ListItem.Add(
            new ResepRsValidateCommandObat("BRG-ID-1",
                "BRG-NAME-1", 1, "2dd1", "CaraPakai", 1,
                new List<ResepRsValidateCommandKomponenRacik>()));
        HappyPathMocking();
        var result = await _sut.Handle(cmd, CancellationToken.None);
        var resultFetched = result.ToList();
        resultFetched.Should().NotBeNull();
        resultFetched.First().ValidationNote.Should().BeNullOrEmpty();
    }
    
    [Fact]
    public async Task T02_GivenSingle1Dpho_And1NonDpho_ThenSuccess_AndGet1ValidationNote()
    {
        var cmd = Command();
        cmd.ListResep.First().ListItem.Add(
            new ResepRsValidateCommandObat("BRG-ID-1",
                "BRG-NAME-1", 1, "2dd1", "CaraPakai", 1,
                new List<ResepRsValidateCommandKomponenRacik>()));
        cmd.ListResep.First().ListItem.Add(
            new ResepRsValidateCommandObat("BRG-ID-2",
                "BRG-NAME-2", 1, "2dd1", "CaraPakai", 1,
                new List<ResepRsValidateCommandKomponenRacik>()));
        HappyPathMocking();
        var result = await _sut.Handle(cmd, CancellationToken.None);
        var resultFetched = result.ToList();
        resultFetched.Should().NotBeNull();
        resultFetched.First().ValidationNote.Should().NotBeNull();
    }
}