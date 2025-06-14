using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.SepContext.FaskesFeature;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext.PesertaBpjsFeature;

public record PesertaBpjsType
{
    private PesertaBpjsType(string pesertaBpjsId, string pesertaBpjsName, string nik, JenisPesertaType jenisPeserta,
        KelasRawatType hakKelas, FaskesRefference provider)
    {
        PesertaBpjsId = pesertaBpjsId;
        PesertaBpjsName = pesertaBpjsName;
        Nik = nik;
        JenisPeserta = jenisPeserta;
        HakKelas = hakKelas;
        Provider = provider;
    }

    public string PesertaBpjsId { get; init; }
    public string PesertaBpjsName { get; init; }
    public string Nik { get; init; }
    public JenisPesertaType JenisPeserta { get; init; }
    public KelasRawatType HakKelas { get; init; }
    public FaskesRefference Provider { get; init; }

    public static PesertaBpjsType Create(string pesertaBpjsId, string pesertaBpjsName, string nik, 
        JenisPesertaType jenisPeserta, KelasRawatType hakKelas, FaskesRefference provider)
    {
        Guard.Against.NullOrWhiteSpace(pesertaBpjsId, nameof(pesertaBpjsId), "Nomor peserta harus terisi");
        Guard.Against.StringTooLong(pesertaBpjsId, 13, nameof(pesertaBpjsId), "Nomor peserta harus 13 karakter");
        Guard.Against.NullOrWhiteSpace(pesertaBpjsName, nameof(pesertaBpjsName), "Nama Peserta BPJS harus terisi");

        Guard.Against.NullOrWhiteSpace(nik, nameof(nik), "NIK harus terisi");
        Guard.Against.StringTooLong(nik, 16, nameof(nik), "NIK harus 16 karakter");

        Guard.Against.Null(jenisPeserta, nameof(jenisPeserta), "Jenis peserta harus terisi");
        Guard.Against.Null(hakKelas, nameof(hakKelas), "Hak kelas harus terisi");
        Guard.Against.Null(provider, nameof(provider), "Provider harus terisi");

        return
            new PesertaBpjsType(pesertaBpjsId, pesertaBpjsName, nik, jenisPeserta, hakKelas, provider);
    }
    
    
    public static PesertaBpjsType Default 
        => new("-", "-", "-", JenisPesertaType.Default, KelasRawatType.Default, FaskesType.Default.ToRefference());
    public PesertaBpjsRefference ToRefference() 
        => new PesertaBpjsRefference(PesertaBpjsId, PesertaBpjsName);
}