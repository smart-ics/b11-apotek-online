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

public class EKlaimMedisDto : IEKlaimKey
{
    public EKlaimMedisDto()
    {
    }

    public EKlaimMedisDto(EKlaimModel model)
    {
        EKlaimId = model.EKlaimId;
        AdlSubAcuteScore = model.AdlScore.SubAcute;
        AdlChronicScore = model.AdlScore.Chronic;
        IcuFlag = model.IcuIndikator.IcuFlag;
        IcuLos = model.IcuIndikator.Los;
        IcuDescription = model.IcuIndikator.Description;
        Covid19StatusId = model.Covid19.Status.Covid19StatusId;
        Covid19StatusName = model.Covid19.Status.Covid19StatusName;
        Covid19TipeNoKartuId = model.Covid19.TipeNoKartu.TipeNoKartuId;
        Covid19TipeNoKartuName = model.Covid19.TipeNoKartu.TipeNoKartuName;
        IsPemulasaranJenazah = model.Covid19.Jenazah.IsPemulasaranJenazah;
        IsKantongJenazah = model.Covid19.Jenazah.IsKantongJenazah;
        IsPetiJenazah = model.Covid19.Jenazah.IsPetiJenazah;
        IsPlastikErat = model.Covid19.Jenazah.IsPlastikErat;
        IsDesinfeksiJenazah = model.Covid19.Jenazah.IsDesinfektanJenazah;
        IsMobilJenazah = model.Covid19.Jenazah.IsMobilJenazah;
        IsDesinfektanMobilJenazah = model.Covid19.Jenazah.IsDesinfektanMobilJenazah;
        IsIsoman = model.Covid19.IsIsoman;
        Episodes = model.Covid19.Episodes;
        AksesNaat = model.Covid19.AksesNaat;
        DializerUsageId = model.PelayananDarah.DializerUsage.DializerUsageId;
        DializerUsageName = model.PelayananDarah.DializerUsage.DializerUsageName;
        JumKantongDarah = model.PelayananDarah.JumlahKantongDarah;
        AlteplaseIndikator = model.PelayananDarah.AlteplaseIndikator;
        Sistole = model.VitalSign.Sistole;
        Diastole = model.VitalSign.Diastole;
        BodyWeight = model.VitalSign.BodyWeight;
        TbIndikatorId = model.PasienTb.TbIndikatorId;
        TbIndikatorName = model.PasienTb.TbIndikatorName;        
    }
    public string EKlaimId { get; set; }
    public int AdlSubAcuteScore { get; set; }
    public int AdlChronicScore { get; set; }
    public int IcuFlag { get; set; }
    public int IcuLos { get; set; }
    public string IcuDescription { get; set; }
    public string Covid19StatusId { get; set; }
    public string Covid19StatusName { get; set; }
    public string Covid19TipeNoKartuId { get; set; }
    public string Covid19TipeNoKartuName { get; set; }
    public bool IsPemulasaranJenazah { get; set; }
    public bool IsKantongJenazah { get; set; }
    public bool IsPetiJenazah { get; set; }
    public bool IsPlastikErat { get; set; }
    public bool IsDesinfeksiJenazah { get; set; }
    public bool IsMobilJenazah { get; set; }
    public bool IsDesinfektanMobilJenazah { get; set; }
    public bool IsIsoman { get; set; }
    public string Episodes { get; set; }
    public string AksesNaat { get; set; }
    public string DializerUsageId { get; set; }
    public string DializerUsageName { get; set; }
    public int JumKantongDarah { get; set; }
    public bool AlteplaseIndikator { get; set; }
    public int Sistole { get; set; }
    public int Diastole { get; set; }
    public decimal BodyWeight { get; set; }
    public string TbIndikatorId { get; set; }
    public string TbIndikatorName { get; set; }

    public EKlaimModel ToModel(EKlaimModel model)
    {
        var adlScore = AdlScoreType.Create(AdlSubAcuteScore, AdlChronicScore);
        var icuIndikator = IcuIndikatorType.Create(IcuLos);
        
        var status = Covid19StatusType.Create(Covid19StatusId, Covid19StatusName);
        var tipeNoKartu = new TipeNoKartuType(Covid19TipeNoKartuId, Covid19TipeNoKartuName);
        var jenazah = new Covid19JenazahType(IsPemulasaranJenazah, IsKantongJenazah, IsPetiJenazah, 
            IsPlastikErat, IsDesinfeksiJenazah, IsMobilJenazah, IsDesinfektanMobilJenazah);
        var covid19 = Covid19Type.Create(status, tipeNoKartu, jenazah, IsIsoman);
        var dializerUsage = new DializerUsageType(DializerUsageId, DializerUsageName);
        var pelayananDarah = new PelayananDarahType(dializerUsage, JumKantongDarah, AlteplaseIndikator);
        var vitalSign = new VitalSignType(Sistole, Diastole, BodyWeight);
        var tbIndikator = new TbIndikatorType(TbIndikatorId, TbIndikatorName);
        model.SetMedisPasien(adlScore, icuIndikator, covid19, pelayananDarah, vitalSign, tbIndikator);

        return model;
    }
}

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