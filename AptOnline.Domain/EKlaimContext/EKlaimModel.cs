using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptOnline.Domain.EKlaimContext
{
    public class EKlaimModel : IEKlaimKey
    {
        private EKlaimModel(string eKlaimId, DateTime eKlaimDate, 
            SepRefference sep, PasienType pasien, PesertaBpjsRefference pesertaBpjs)
        {
            EKlaimId = eKlaimId;
            EKlaimDate = eKlaimDate;
            Sep = sep;
            Pasien = pasien;
            PesertaBpjs = pesertaBpjs;
        }

        public string EKlaimId { get; private set; }
        public DateTime EKlaimDate { get; private set; }
        public SepRefference Sep { get; private set; }
        public PasienType Pasien { get; private set; }
        public PesertaBpjsRefference PesertaBpjs { get; private set; }

        public static EKlaimModel CreateFromSep(SepType sep, DateTime eKlaimDate)
        {
            Guard.Against.Null(sep, nameof(sep), "SEP tidak boleh kosong");

            var eKlaimId = Ulid.NewUlid().ToString();
            
            return new EKlaimModel(eKlaimId, eKlaimDate,
                sep.ToRefference(), sep.Reg.Pasien,
                sep.PesertaBpjs.ToRefference());
        }
        
        public static EKlaimModel Load(string eKlaimId, DateTime eKlaimDate, 
            SepRefference sep, PasienType pasien, PesertaBpjsRefference pesertaBpjs) 
            => new EKlaimModel(eKlaimId, eKlaimDate, sep, pasien, pesertaBpjs);

        public static EKlaimModel Default => new EKlaimModel(
            "-", new DateTime(3000,1,1), SepType.Default.ToRefference(), 
            PasienType.Default, PesertaBpjsType.Default.ToRefference());

    }

    public interface IEKlaimKey
    {
        string EKlaimId { get; }
    }
}
