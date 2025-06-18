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
            TarifRs = TarifRsType.Default;
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
        public TarifRsType TarifRs { get; private set; }
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
        //public void 
        #endregion

    }
}
