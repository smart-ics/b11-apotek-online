using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.SepContext.FaskesFeature;
using AptOnline.Domain.SepContext.ReferensiFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext.PesertaBpjsFeature;

public record PesertaBpjsType
{
    private PesertaBpjsType(string nomorPeserta, string nik, JenisPesertaType jenisPeserta,
        KelasRawatType hakKelas, FaskesRefference provider)
    {
        NomorPeserta = nomorPeserta;
        Nik = nik;
        JenisPeserta = jenisPeserta;
        HakKelas = hakKelas;
        Provider = provider;
    }

    public string NomorPeserta { get; init; }
    public string Nik { get; init; }
    public JenisPesertaType JenisPeserta { get; init; }
    public KelasRawatType HakKelas { get; init; }
    public FaskesRefference Provider { get; init; }

    public static PesertaBpjsType Create(string nomorPeserta, string nik, 
        JenisPesertaType jenisPeserta, KelasRawatType hakKelas, FaskesRefference provider)
    {
        Guard.Against.NullOrWhiteSpace(nomorPeserta, nameof(nomorPeserta), "Nomor peserta harus terisi");
        Guard.Against.StringTooLong(nomorPeserta, 13, nameof(nomorPeserta), "Nomor peserta harus 13 karakter");

        Guard.Against.NullOrWhiteSpace(nik, nameof(nik), "NIK harus terisi");
        Guard.Against.StringTooLong(nik, 16, nameof(nik), "NIK harus 16 karakter");

        Guard.Against.Null(jenisPeserta, nameof(jenisPeserta), "Jenis peserta harus terisi");
        Guard.Against.Null(hakKelas, nameof(hakKelas), "Hak kelas harus terisi");
        Guard.Against.Null(provider, nameof(provider), "Provider harus terisi");

        return new PesertaBpjsType(nomorPeserta, nik, jenisPeserta, hakKelas, provider);
    }
    public static PesertaBpjsType Default 
        => new("-", "-", JenisPesertaType.Default, KelasRawatType.Default, FaskesType.Default.ToRefference());
}