using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.SepContext;

#region RESERVED
//public record PembiayaanNaikKelasType(string Code, string Name, string PenanggungJawab);
//public record KelasRawatInfoType(KelasRawatType KelasHak, KelasRawatType KelasNaik,
//    PembiayaanNaikKelasType Pembiayaan);
//public record JaminanLakaInfoType(StatusLakaType StatusLaka, 
//    string NoLaporanPolisi, PenjaminLakaType Penjamin);
//public record PenjaminLakaType(DateTime TglKejadian,string Keterangan,
//    SuplesiInfoType Suplesi);

//public record JenisPelayananType(string Code, string Name);

//public record RujukanInfoType(FaskesType FaskesPerujuk, DateTime TglRujukan, string NomorRujukan);
//public record PoliTujuanInfoType(PoliType Poli, bool IsPoliEksekutif);
//public record StatusPulangType(string KodeStatusPulang, DateTime TglPulang, DateTime TglMeninggal,
//    string NoSuratKematian, string NoLaporanPolisiManual);
#endregion

public class SepModel
{
    private SepModel(string sepId, string nomorSep, DateTime sepDate, 
        PesertaBpjsType peserta, PasienType pasien)
    {
        SepId = sepId;
        SepDate = sepDate;
        NomorSep = nomorSep;
        Peserta = peserta;
        Pasien = pasien;
    }
    public string SepId { get; init; }
    public DateTime SepDate { get; private set; }
    public string NomorSep { get; private init; }
    public PesertaBpjsType Peserta { get; init; }
    public PasienType Pasien { get; init; }

    #region RESERVED
    //public RegType Reg { get; private set; }
    //public RujukanInfoType Rujukan { get; private set; }
    //public DokterType DpjpLayan { get; set; }

    //public FaskesType Faskes { get; set; }
    //public JenisPelayananType JenisPelayanan { get; set; }

    //public KelasRawatType KelasRawat { get; set; } 
    //public KelasRawatInfoType KelasSaatRawat { get; set; } 


    //public string Catatan { get; set; }
    //public DiagnosaType DiagnosaAwal { get; set; } 
    //public PoliTujuanInfoType PoliTujuan { get; set; } 

    //public bool  IsCob { get; set; }
    //public AsuransiType Cob { get; set; }

    //public bool IsKatarak { get; set; }
    //public JaminanLakaInfoType JaminanLaka { get; set; } 
    //public TujuanKunjunganType TujuanKunjungan { get; set; }    
    //public FlagProcedureType FlagProcedure { get; set; } 
    //public PenunjangType Penunjang { get; set; } 
    //public AssesmentPelayananType AssestmenPelayanan { get; set; }  
    //public SkdpType Skdp { get; set; } 
    //public bool IsESep { get; set; }
    //public string UserSep { get; set; }
    //public string SepInternal { get; set; }
    //public StatusPulangType StatusPulang { get; set; }
    #endregion

    public static SepModel Create(string nomorSep, DateTime sepDate, 
        PesertaBpjsType peserta, PasienType pasien)
    {
        Guard.Against.NullOrWhiteSpace(nomorSep, nameof(nomorSep), "Nomor SEP harus terisi");
        Guard.Against.Null(peserta, nameof(peserta), "Peserta BPJS harus terisi");
        Guard.Against.Null(pasien, nameof(pasien), "Pasien harus terisi");

        var sepId = Ulid.NewUlid().ToString();
        return new SepModel(sepId, nomorSep, sepDate, peserta, pasien);
    }
}