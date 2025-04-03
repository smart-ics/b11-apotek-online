namespace AptOnline.Domain.BillingContext.SepAgg;

public class SepModel : ISepKey
{
    public string SepId { get; set; }
    public string SepDateTime { get; set; }
    public string RegId { get; set; }
    public string PasienId { get; set; }
    public string PasienName { get; set; }
    public string PesertaJaminanId { get; set; }
    public string SepNo { get; set; }
    public string TelpNo { get; set; }
    public string JnsPesertaName { get; set; }
    public string KelasPesertaName { get; set; }
    public string KelasRawatKode { get; set; }
    public string JenisKelaminName { get; set; }
    public string LayananName { get; set; }
    public bool IsPoliEksekutif { get; set; }
    public bool IsKatarak { get; set; }
    public string DpjpLayananId { get; set; }
    public string DpjpLayananName { get; set; }
    public string DpjpId { get; set; }
    public string DpjpName { get; set; }
    public string TujuanKunjunganId { get; set; }
    public object FlagPrecodureId { get; set; }
    public string PenunjangId { get; set; }
    public string AssPelayananId { get; set; }
    public string SkdpNo { get; set; }
    public string RujukanNo { get; set; }
    public string RujukanDate { get; set; }
    public string RujukanTime { get; set; }
    public string RujukanDiagCode { get; set; }
    public string RujukanDiagName { get; set; }
    public string RujukanProviderId { get; set; }
    public string RujukanProviderName { get; set; }
    public string JnsPelayananKode { get; set; }
    public string Provider { get; set; }
    public bool IsCob { get; set; }
    public string CobName { get; set; }
    public string CobNo { get; set; }
    public string Note { get; set; }
    public string LakaId { get; set; }
    public string LakaDate { get; set; }
    public string PropLakaId { get; set; }
    public string PropLakaName { get; set; }
    public string KabLakaId { get; set; }
    public string KabLakaName { get; set; }
    public string KecLakaId { get; set; }
    public string KecLakaName { get; set; }
    public string KeteranganLaka { get; set; }
    public string PenjaminLakaId { get; set; }
    public bool IsSuplesi { get; set; }
    public string SepSuplesiNo { get; set; }
    public bool IsPrb { get; set; }
    public string Prb { get; set; }
    public string SubSpesialis { get; set; }
    public string PembiayaanId { get; set; }
    public string PenanggungJawab { get; set; }
    public string PulangDate { get; set; }
    public string StatusPulangId { get; set; }
    public string MeninggalDate { get; set; }
    public string SuratKematianNo { get; set; }
    public string LpManualNo { get; set; }
    public string RujukanInternalNo { get; set; }
}

public interface ISepKey
{
    string SepId { get; }
}