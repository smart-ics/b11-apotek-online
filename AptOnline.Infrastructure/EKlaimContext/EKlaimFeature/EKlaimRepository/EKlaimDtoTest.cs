using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.PegFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.DischargeStatusFeature;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.JenisRawatFeature;
using AptOnline.Domain.EKlaimContext.KelasTarifRsFeature;
using AptOnline.Domain.EKlaimContext.PayorFeature;
using AptOnline.Domain.EKlaimContext.TarifRsFeature;
using AptOnline.Domain.EKlaimContext.UpgradeIndikatorFeature;
using AptOnline.Domain.SepContext.KelasJknFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public class EKlaimDtoTest
{
    private EKlaimDto DtoFaker()
        => new EKlaimDto
        {
            EKlaimId = "A",
            EKlaimDate = new DateTime(2025, 1, 2),
            SepId = "B",
            SepNo = "C",
            SepDate = new DateTime(2025, 1, 3),
            KartuBpjsNo = "D",
            RegId = "E",
            RegDate = new DateTime(2025, 1, 4),
            PasienId = "F",
            PasienName = "G",
            BirthDate = new DateTime(2025, 1, 5),
            Gender = "1",
            DpjpId = "I",
            DpjpName = "J",
            CaraMasukId = "K",
            CaraMasukName = "L",
            JenisRawatId = "M",
            JenisRawatName = "N",
            KelasJknId = "O",
            KelasJknName = "P",
            KelasJknValue = 1,
            KelasTarifRsId = "Q",
            KelasTarifRsName = "R",
            TarifPoliEksekutif = 2,
            UpgradeIndikator = 3,
            AddPaymentProcentage = 4,
            DischargeStatusId = "S",
            DischargeStatusName = "T",
            PayorId = "U",
            PayorName = "V",
            CoderPegId = "W",
            CoderPegName = "X",
            CoderNik = "Y",
            Los = 5,
        };

    private static EKlaimModel ModelFaker()
    {
        var reg = new RegRefference("E", new DateTime(2025, 1, 4));
        var sep = new SepRefference("B", "C", new DateTime(2025, 1, 3));
        var pasien = PasienType.Create("F", "G", new DateTime(2025, 1, 5), GenderType.Create("1"));
        var pesertaBpjs = new PesertaBpjsRefference("D", "G");
        var result = new EKlaimModel("A", new DateTime(2025, 1, 2), reg, sep, pasien, pesertaBpjs);

        var dokter = new DokterType("I", "J");
        var caraMasuk = new CaraMasukType("K", "L");
        var jenisRawat = new JenisRawatType("M", "N");
        result.SetAdministrasiMasuk(dokter, caraMasuk, jenisRawat);
        
        var kelasJkn = new KelasJknType("O", "P", 1);
        var kelasTarifRs = new KelasTarifRsType("Q", "R");
        var tarifRs = TarifRsModel.Default;
        var upgradeIndikator = new UpgradeKelasIndikatorType(3, 4);
        var dischargeStatus = new DischargeStatusType("S", "T");
        var payor = new PayorType("U", "V", "V");
        var coder = new PegType("W", "X", "Y");
        result.SetBillPasien(kelasJkn, kelasTarifRs, tarifRs, 2, upgradeIndikator, dischargeStatus, payor, coder, 5);
        
        return result;  
    }
    
    [Fact]
    public void UT1_DtoToModel_Test()
    {
        var dto = DtoFaker();
        var result = dto.ToModel();
        result.Should().BeEquivalentTo(ModelFaker(), 
            opt => opt
                .Excluding(x => x.AdlScore)
                .Excluding(x => x.IcuIndikator)
                .Excluding(x => x.Covid19)
                .Excluding(x => x.PelayananDarah)
                .Excluding(x => x.VitalSign)
                .Excluding(x => x.PasienTb));
    }
}