using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.PegFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.Covid19Feature;
using AptOnline.Domain.EKlaimContext.DischargeStatusFeature;
using AptOnline.Domain.EKlaimContext.JenisRawatFeature;
using AptOnline.Domain.EKlaimContext.KelasTarifRsFeature;
using AptOnline.Domain.EKlaimContext.PayorFeature;
using AptOnline.Domain.EKlaimContext.PelayananDarahFeature;
using AptOnline.Domain.EKlaimContext.TarifRsFeature;
using AptOnline.Domain.EKlaimContext.TbIndikatorFeature;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.EKlaimFeature
{
    public class EKlaimModel : IEKlaimKey
    {
        private EKlaimModel(string eKlaimId, DateTime eKlaimDate, RegRefference reg, 
            SepRefference sep, PasienType pasien, PesertaBpjsRefference pesertaBpjs)
        {
            
            //      MANDATORY
            EKlaimId = eKlaimId;
            EKlaimDate = eKlaimDate;
            Reg = reg;
            Sep = sep;
            Pasien = pasien;
            PesertaBpjs = pesertaBpjs;

            //      ADMINISTRASI MASUK
            Dpjp = DokterType.Default;
            CaraMasuk = CaraMasukType.Default;
            JenisRawat = JenisRawatType.Default;
            
            //      MEDIS PASIEN
            AdlScore = AdlScoreType.Default;
            IcuIndikator = IcuIndikatorType.Default;
            Covid19 = Covid19Type.Default;
            PelayananDarah = PelayananDarahType.Default;
            VitalSign = VitalSignType.Default;
            PasienTb = TbIndikatorType.Default;
            
            //      BILL PASIEN
            KelasRawat = KelasRawatType.Default;
            KelasTarifRs = KelasTarifRsType.Default;
            TarifRs = TarifRsModel.Default;
            TarifPoliEksekutif = 0;
            UpgradeKelasIndikator = UpgradeKelasIndikatorType.Default;
            DischargeStatus = DischargeStatusType.Default;
            Payor = PayorType.Default;
            Coder = PegType.Default;
            LengthOfStay = 0;
        }
        
        #region PROPERTIES
        //      EKLAIM IDENTITY
        public string EKlaimId { get; private set; }
        public DateTime EKlaimDate { get; private set; }
        //      ADMINISTRASI MASUK
        public SepRefference Sep { get; private set; }
        public RegRefference Reg { get; private set; }
        public PasienType Pasien { get; private set; }
        public DokterType Dpjp { get; private set;}
        public PesertaBpjsRefference PesertaBpjs { get; private set; }
        public CaraMasukType CaraMasuk { get; private set; }
        public JenisRawatType JenisRawat { get; private set; }
        //      MEDIS PASIEN
        public AdlScoreType AdlScore { get; private set; }
        public IcuIndikatorType IcuIndikator { get; private set; }
        public Covid19Type Covid19 { get; private set; }
        public PelayananDarahType PelayananDarah { get; private set; }
        public VitalSignType VitalSign { get; private set; }
        public TbIndikatorType PasienTb { get; private set; }
        //      BILL & ADMIN KELUAR
        public KelasRawatType KelasRawat { get; private set; }
        public KelasTarifRsType KelasTarifRs { get; private set; }
        public TarifRsModel TarifRs { get; private set; }
        public decimal TarifPoliEksekutif { get; private set; }
        public UpgradeKelasIndikatorType  UpgradeKelasIndikator { get; private set; }
        public DischargeStatusType DischargeStatus { get; private set; }
        public PayorType Payor { get; private set; }
        public PegType Coder { get; private set; }
        public int LengthOfStay { get; private set; }
        #endregion
        
        #region STATIC FACTORY
        public static EKlaimModel CreateFromSep(SepType sep, DateTime eKlaimDate)
        {
            Guard.Against.Null(sep, nameof(sep), "SEP tidak boleh kosong");

            var eKlaimId = Ulid.NewUlid().ToString();
            
            return new EKlaimModel(eKlaimId, eKlaimDate, sep.Reg.ToRefference(),
                sep.ToRefference(), sep.Reg.Pasien,
                sep.PesertaBpjs);
        }
        public static EKlaimModel Load(string eKlaimId, DateTime eKlaimDate, RegRefference reg, 
            SepRefference sep, PasienType pasien, PesertaBpjsRefference pesertaBpjs) 
            => new EKlaimModel(eKlaimId, eKlaimDate, reg, sep, pasien, pesertaBpjs);

        public static EKlaimModel Default => new EKlaimModel(
            "-", new DateTime(3000,1,1), RegType.Default.ToRefference(), 
            SepType.Default.ToRefference(), PasienType.Default, 
            PesertaBpjsType.Default.ToRefference());

        public static IEKlaimKey Key(string id)
        {
            var result = EKlaimModel.Default;
            result.EKlaimId = id;
            return result;
        }
        #endregion
        
        #region BEHAVIOR
        public void SetAdministrasiMasuk(DokterType dpjp, CaraMasukType caraMasuk, JenisRawatType jenisRawat)
        {
            Guard.Against.Null(dpjp, nameof(dpjp));
            Guard.Against.Null(caraMasuk, nameof(caraMasuk));
            Guard.Against.Null(jenisRawat, nameof(jenisRawat));
            
            Dpjp = dpjp;
            CaraMasuk = caraMasuk;
            JenisRawat = jenisRawat;
        } 
        public void SetMedisPasien(AdlScoreType adlScore, 
            IcuIndikatorType icuIndikator, Covid19Type covid19, 
            PelayananDarahType pelayananDarah, VitalSignType vitalSign, 
            TbIndikatorType pasienTb)
        {
            Guard.Against.Null(adlScore, nameof(adlScore));
            Guard.Against.Null(icuIndikator, nameof(icuIndikator));
            Guard.Against.Null(covid19, nameof(covid19));
            Guard.Against.Null(pelayananDarah, nameof(pelayananDarah));
            Guard.Against.Null(vitalSign, nameof(vitalSign));
            Guard.Against.Null(pasienTb, nameof(pasienTb));
            
            AdlScore = adlScore;
            IcuIndikator = icuIndikator;
            Covid19 = covid19;
            PelayananDarah = pelayananDarah;
            VitalSign = vitalSign;
            PasienTb = pasienTb;
        }
        
        public void SetBillPasien(KelasRawatType kelasRawat, KelasTarifRsType kelasTarifRs, 
            TarifRsModel tarifRs, decimal tarifPoliEksekutif, UpgradeKelasIndikatorType upgradeKelasIndikator,
            DischargeStatusType dischargeStatus, PayorType payor, PegType coder, int lengthOfStay)
        {
            Guard.Against.Null(kelasRawat, nameof(kelasRawat));
            Guard.Against.Null(kelasTarifRs, nameof(kelasTarifRs));
            Guard.Against.Null(tarifRs, nameof(tarifRs));
            Guard.Against.Null(upgradeKelasIndikator, nameof(upgradeKelasIndikator));
            Guard.Against.Null(dischargeStatus, nameof(dischargeStatus));
            Guard.Against.Null(payor, nameof(payor));
            Guard.Against.Null(coder, nameof(coder));
            
            KelasRawat = kelasRawat;
            KelasTarifRs = kelasTarifRs;
            TarifRs = tarifRs;
            TarifPoliEksekutif = tarifPoliEksekutif;
            UpgradeKelasIndikator = upgradeKelasIndikator;
            DischargeStatus = dischargeStatus;
            Payor = payor;
            Coder = coder;
            LengthOfStay = lengthOfStay;
        }
        #endregion

    }
}
