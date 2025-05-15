using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.SepContext.ReferensiFeature;

namespace AptOnline.Domain.SepContext;

public record PesertaBpjsType(string NomorKartu, PasienType Pasien,
    JenisPesertaType JenisPeserta, KelasRawatType HakKelas,
    ProviderPesertaType Provider);
public record JenisPesertaType(string Code, string Name);
public record ProviderPesertaType(string Code, string Name);
