using AptOnline.Application.AptolCloudContext.PpkAgg;
using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Application.BillingContext.SepAgg;
using AptOnline.Application.PharmacyContext.MapDphoAgg;
using Moq;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsValidateUseCase;

public class ResepRsValidateTest
{
    /* test case:
     A. Happy Path Testig
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

    public ResepRsValidateTest(Mock<IPpkGetService> ppkGetService)
    {
        _writer = new Mock<IResepMidwareWriter>();
        _sepGetByRegService = new Mock<ISepGetByRegService>();
        _ppkGetService = ppkGetService;
        _layananGetService = new Mock<ILayananGetService>();
        _mapDphoGetService = new Mock<IMapDphoGetService>();
        _sut = new ResepRsValidateHandler(
            _sepGetByRegService.Object,
            _ppkGetService.Object,
            _layananGetService.Object,
            _mapDphoGetService.Object,
            _writer.Object);
    }

    // private static RegType FakeReg()
    //     => new RegType
    //     {
    //         RegId = "RG-001",
    //         PasienId = "MR-001",
    //         PasienName = "John Doe",
    //         RegDate = "2025-04-01",
    //         Sep = "SEP-001",
    //     };
    // private void MockRegGetService() 
    //     => _regGetService
    //         .Setup(x => x.Execute(It.IsAny<IRegKey>()))
    //         .Returns(FakeReg());
    
    // private static SepType FakeSep()
    //     => new SepType
    //     {
    //         SepId = "SEP-001",
    //         DpjpId = "DPJP-001",
    //         DpjpName = "DPJP-001-Name",
    //     };

    // private static List<MapDphoModel> ListMapDpho()
    //     => new()
    //     {
    //         new MapDphoModel("OBT1", "Obat-1", "DPHO1", "Obat DPHO-1"),
    //         new MapDphoModel("OBT2", "Obat-2", "DPHO2", "Obat DPHO-2"),
    //     };
    
    //private static List<BrgMo

    private ResepRsValidateCommand Command()
        => new ResepRsValidateCommand("RG-001", "LYN-001",
            new List<ResepRsValidateCommandResep>());

    // [Fact]
    // public async Task T01_GivenSingleAllDpho_ThenSuccess_AndNoValidationNote()
    // {
    //     MockRegGetService();
    //     
    //     var result = await _sut.Handle()
    // }

}