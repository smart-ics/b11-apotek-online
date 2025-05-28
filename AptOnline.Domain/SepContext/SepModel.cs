using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using AptOnline.Domain.SepContext.FaskesFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.ReferensiFeature;

namespace AptOnline.Domain.SepContext;

public record PembiayaanNaikKelasType(string Code, string Name, string PenanggungJawab);
public record KelasRawatInfoType(KelasRawatType KelasHak, KelasRawatType KelasNaik,
    PembiayaanNaikKelasType Pembiayaan);
public record JaminanLakaInfoType(StatusLakaType StatusLaka, 
    string NoLaporanPolisi, PenjaminLakaType Penjamin);
public record PenjaminLakaType(DateTime TglKejadian,string Keterangan,
    SuplesiInfoType Suplesi);

public record JenisPelayananType(string Code, string Name);

public record RujukanInfoType(FaskesType FaskesPerujuk, DateTime TglRujukan, string NomorRujukan);
public record PoliTujuanInfoType(PoliType Poli, bool IsPoliEksekutif);
public record StatusPulangType(string KodeStatusPulang, DateTime TglPulang, DateTime TglMeninggal,
    string NoSuratKematian, string NoLaporanPolisiManual);

public class SepModel
{
    public string SepId { get; init; }
    public string SepNo { get; private set; }
    public DateTime  SepDate { get; private set; }
    
    public PesertaBpjsType Peserta { get; private set; }
    public RegType Reg { get; private set; }
    public RujukanInfoType Rujukan { get; private set; }
        
    public FaskesType Faskes { get; set; }
    public JenisPelayananType JenisPelayanan { get; set; }
    
    public KelasRawatType KelasRawat { get; set; } 
    public KelasRawatInfoType KelasSaatRawat { get; set; } 


    public string Catatan { get; set; }
    public DiagnosaType DiagnosaAwal { get; set; } 
    public PoliTujuanInfoType PoliTujuan { get; set; } 

    public bool  IsCob { get; set; }
    public AsuransiType Cob { get; set; }

    public bool IsKatarak { get; set; }
    public JaminanLakaInfoType JaminanLaka { get; set; } 
    public TujuanKunjunganType TujuanKunjungan { get; set; }    
    public FlagProcedureType FlagProcedure { get; set; } 
    public PenunjangType Penunjang { get; set; } 
    public AssesmentPelayananType AssestmenPelayanan { get; set; }  
    public SkdpType Skdp { get; set; } 
    public DokterType DpjpLayan { get; set; } 
    public bool IsESep { get; set; }
    public string UserSep { get; set; }
    public string SepInternal { get; set; }
    public StatusPulangType StatusPulang { get; set; }
}