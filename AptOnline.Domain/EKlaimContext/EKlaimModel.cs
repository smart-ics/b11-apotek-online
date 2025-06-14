using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using Ardalis.GuardClauses;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;

namespace AptOnline.Domain.EKlaimContext
{
    public class EKlaimModel : IEKlaimKey
    {
        private EKlaimModel(string eKlaimId, DateTime eKlaimDate, RegRefference reg, 
            SepRefference sep, PasienType pasien, PesertaBpjsRefference pesertaBpjs)
        {
            EKlaimId = eKlaimId;
            EKlaimDate = eKlaimDate;
            Reg = reg;
            Sep = sep;
            Pasien = pasien;
            PesertaBpjs = pesertaBpjs;
        }

        public string EKlaimId { get; private set; }
        public DateTime EKlaimDate { get; private set; }
        public RegRefference Reg { get; private set; }
        public SepRefference Sep { get; private set; }

        public PasienType Pasien { get; private set; }
        public PesertaBpjsRefference PesertaBpjs { get; private set; }

        public CaraMasukType CaraMasuk { get; private set; }
        
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

    }
}
