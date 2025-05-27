using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.SepContext.FaskesFeature;
using AptOnline.Domain.SepContext.ReferensiFeature;

namespace AptOnline.Domain.SepContext.PesertaBpjsFeature;

public record PesertaBpjsType
{
    public string NomorKartu { get; init; }
    public string Nik { get; init; }
    //public string Pisa
    public JenisPesertaType JenisPeserta { get; init; }
    public KelasRawatType HakKelas { get; init; }
    public FaskesRefference Provider { get; init; }
}