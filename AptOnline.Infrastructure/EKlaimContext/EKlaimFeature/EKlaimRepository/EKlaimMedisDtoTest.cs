using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.EKlaimContext.Covid19Feature;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.PelayananDarahFeature;
using AptOnline.Domain.EKlaimContext.TbIndikatorFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public class EKlaimMedisDtoTest
{
    private static EKlaimMedisDto Faker()
        => new EKlaimMedisDto
        {
            EKlaimId = "EKlaimId",
            AdlSubAcuteScore = 1,
            AdlChronicScore = 2,
            IcuFlag = 1,
            IcuLos = 3,
            IcuDescription = "IcuDescription",
            Covid19StatusId = "Covid19StatusId",
            Covid19StatusName = "Covid19StatusName",
            Covid19TipeNoKartuId = "Covid19TipeNoKartuId",
            Covid19TipeNoKartuName = "Covid19TipeNoKartuName",
            IsPemulasaranJenazah = true,
            IsKantongJenazah = false,
            IsPetiJenazah = true,
            IsPlastikErat = false,
            IsDesinfeksiJenazah = true,
            IsMobilJenazah = false,
            IsDesinfektanMobilJenazah = true,
            IsIsoman = false,
            Episodes = "Episodes",
            AksesNaat = "AksesNaat",
            DializerUsageId = "DializerUsageId",
            DializerUsageName = "DializerUsageName",
            JumKantongDarah = 4,
            AlteplaseIndikator = true,
            Sistole = 5,
            Diastole = 6,
            BodyWeight = 7,
            TbIndikatorId = "TbIndikatorId",
            TbIndikatorName = "TbIndikatorName"
        };

    private static EKlaimModel ModelFaker()
    {
        var reg = new RegRefference("RegId", new DateTime(2000, 1, 1));
        var sep = new SepRefference("SepId", "SepName", new DateTime(2000, 1, 1));
        var pasien = PasienType.Create("PasienId", "PasienName", new DateTime(2000, 1, 1), GenderType.Default);
        var pesertaBpjs = new PesertaBpjsRefference("PesertaBpjsId", "PesertaBpjsName");
        var result = new EKlaimModel("EKlaimId", new DateTime(2000, 1, 1), reg, sep, pasien, pesertaBpjs);
        
        result.SetMedisPasien(
            AdlScoreType.Create(1, 2),
            IcuIndikatorType.Create(3),
            Covid19Type.Create(
                new Covid19StatusType("Covid19StatusId", "Covid19StatusName"),
                new TipeNoKartuType("Covid19TipeNoKartuId", "Covid19TipeNoKartuName"),
                new Covid19JenazahType(true, false, true, false, true, false, true),
                false),
            new PelayananDarahType(
                new DializerUsageType("DializerUsageId", "DializerUsageName"), 4, true),
            new VitalSignType(5, 6, 7),
            new TbIndikatorType("TbIndikatorId", "TbIndikatorName"));   
        return result;
    }
    
    [Fact]
    public void UT1_CreateModelTest()
    {
        var dto = Faker();
        var actual = dto.ToModel(EKlaimModel.Default);
        actual.Should().BeEquivalentTo(ModelFaker(),
            opt => opt
                .Excluding(x => x.EKlaimId)
                .Excluding(x => x.EKlaimDate)
                .Excluding(x => x.Reg)
                .Excluding(x => x.Sep)
                .Excluding(x => x.Pasien)
                .Excluding(x => x.PesertaBpjs)
                .Excluding(x => x.Dpjp)
                .Excluding(x => x.CaraMasuk)
                .Excluding(x => x.JenisRawat)
                .Excluding(x => x.KelasJkn)
                .Excluding(x => x.KelasTarifRs)
                .Excluding(x => x.TarifRs)
                .Excluding(x => x.TarifPoliEksekutif)
                .Excluding(x => x.UpgradeKelasIndikator)
                .Excluding(x => x.DischargeStatus)
                .Excluding(x => x.Payor)
                .Excluding(x => x.Coder)
                .Excluding(x => x.LengthOfStay));
    }
}