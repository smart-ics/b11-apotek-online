using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record NomorKartuTType : StringLookupValueObject<NomorKartuTType>
{
    public NomorKartuTType(string value) : base(value)
    {
    }

    protected override string[] ValidValues => new[]
    {
        "nik", //= untuk Nomor Induk Kependudukan
        "kitas", //= untuk KITAS/KITAP
        "kartu_jkn", // = untuk Nomor Kartu Peserta JKN (BPJS)
        "kk", // = untuk nomor pada Kartu Keluarga
        "unhcr", // = untuk nomor pada dokumen dari UNHCR
        "kelurahan", // = untuk nomor pada dokumen dari kelurahan
        "dinsos", // = untuk nomor pada dokumen dari Dinas Sosial
        "dinkes", //= untuk nomor pada dokumen dari Dinas Kesehatan
        "sjp", // = untuk nomor Surat Jaminan Perawatan (SJP)
        "klaim_ibu", // = mandatori untuk jaminan bayi baru lahir.
        "lainnya", //   = untuk nomor identitas lainnya yang dapat
        // dipertanggungjawabkan oleh rumah sakit
        // dan lembaga yang berwenang lainnya
    };
}