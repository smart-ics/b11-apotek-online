using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public class EKlaimModel
{
    public string nomor_sep { get; set; }
    public string nomor_kartu { get; set; }
    public string tgl_masuk { get; set; }
    public string tgl_pulang { get; set; }
    public string cara_masuk { get; set; }
    public string jenis_rawat { get; set; }
    public string kelas_rawat { get; set; }
    public string adl_sub_acute { get; set; }
    public string adl_chronic { get; set; }
    public string icu_indikator { get; set; }
    public string icu_los { get; set; }
    public string ventilator_hour { get; set; }

    public VentilatorType Ventilator { get; set; }

    public string upgrade_class_ind { get; set; }
    public string upgrade_class_class { get; set; }
    public string upgrade_class_los { get; set; }
    public string upgrade_class_payor { get; set; }
    public string add_payment_pct { get; set; }
    public string birth_weight { get; set; }
    public int sistole { get; set; }
    public int diastole { get; set; }
    public string discharge_status { get; set; }
    public string diagnosa { get; set; }
    public string procedure { get; set; }
    public string diagnosa_inagrouper { get; set; }
    public string procedure_inagrouper { get; set; }

    public TarifRs tarif_rs { get; set; }

    public string pemulasaraan_jenazah { get; set; }
    public string kantong_jenazah { get; set; }
    public string peti_jenazah { get; set; }
    public string plastik_erat { get; set; }
    public string desinfektan_jenazah { get; set; }
    public string mobil_jenazah { get; set; }
    public string desinfektan_mobil_jenazah { get; set; }
    public string covid19_status_cd { get; set; }
    public string nomor_kartu_t { get; set; }
    public string episodes { get; set; }
    public string covid19_cc_ind { get; set; }
    public string covid19_rs_darurat_ind { get; set; }
    public string covid19_co_insidense_ind { get; set; }

    public Covid19PenunjangPengurang covid19_penunjang_pengurang { get; set; }

    public string terapi_konvalesen { get; set; }
    public string akses_naat { get; set; }
    public string isoman_ind { get; set; }
    public int bayi_lahir_status_cd { get; set; }
    public int dializer_single_use { get; set; }
    public int kantong_darah { get; set; }
    public int alteplase_ind { get; set; }

    public ApgarType Apgar { get; set; }

    public RiwayatPersalinanType persalinan { get; set; }

    public string tarif_poli_eks { get; set; }
    public string nama_dokter { get; set; }
    public string kode_tarif { get; set; }
    public string payor_id { get; set; }
    public string payor_cd { get; set; }
    public string cob_cd { get; set; }
    public string coder_nik { get; set; }
}

public class Covid19PenunjangPengurang
{
    public string lab_asam_laktat { get; set; }
    public string lab_procalcitonin { get; set; }
    public string lab_crp { get; set; }
    public string lab_kultur { get; set; }
    public string lab_d_dimer { get; set; }
    public string lab_pt { get; set; }
    public string lab_aptt { get; set; }
    public string lab_waktu_pendarahan { get; set; }
    public string lab_anti_hiv { get; set; }
    public string lab_analisa_gas { get; set; }
    public string lab_albumin { get; set; }
    public string rad_thorax_ap_pa { get; set; }
}

public record PersalinanDeliveryType
{
    public PersalinanDeliveryType(string deliverySequence, string deliveryMethod)
    {
        DeliverySequence = deliverySequence;
        DeliveryMethod = deliveryMethod;
    }
    public string DeliverySequence { get; init; }
    public string DeliveryMethod { get; init; }
    public DateTime DeliveryTimestamp { get; init; }
    public LetakJaninType LetakJanin { get; init; }
    public string Kondisi { get; init; }
    public bool IsUseManual { get; init; }
    public bool IsUseForcep { get; init; }
    public bool IsUseVacuum { get; init; }
    public SkriningHkType SkriningHk { get; init; }
}


public record SkriningHkType
{
    private SkriningHkType(bool isSkrining, string alasan, string lokasi, DateTime timestamp)
    {
        IsSkrining = isSkrining;
        AlasanTidakSkrining = alasan;
        SpecimenLokasi = lokasi;
        SpecimenTimestamp = timestamp;
    }

    public static SkriningHkType CreateSkrining(string lokasi, DateTime timestamp)
    {
        lokasi = lokasi.ToLower();
        if (lokasi.ToLower() is not "vena" and "tumit")
            throw new ArgumentException("Lokasi Skrining Hipotiroid Konginetal harus 'vena' atau 'tumit'");
        
        var result = new SkriningHkType(true, string.Empty, lokasi, timestamp);
        return result;
    }

    public static SkriningHkType CreateoNoSkrining(string alasan)
    {
        if (alasan.ToLower() is not "tidak-dapat" and "akses-sulit")
            throw new ArgumentException("Alasan Tidak Skrining Hipotiroid Konginetal harus 'tidak-dapat' atau 'akses-sulit'");  
        
        return new SkriningHkType(false, alasan, string.Empty, DateTime.MinValue);    
    }

    public static SkriningHkType Load(bool isSkrining, string alasan, string lokasi, DateTime timestamp)
        => new SkriningHkType(isSkrining, alasan, lokasi, timestamp);
    
    public static SkriningHkType Default() 
        => new SkriningHkType(false, string.Empty, string.Empty, new DateTime(3000,1,1));
    
    public bool IsSkrining { get; private set; }
    public string SpecimenLokasi { get; private set; }
    public string AlasanTidakSkrining { get; private set; }
    public DateTime SpecimenTimestamp { get; private set; }
}


public record RiwayatPersalinanType
{
    private readonly List<PersalinanDeliveryType> _listDeilvery;
    public RiwayatPersalinanType(int usiaKehamilan, int gravida, int partus, 
        int abortus, OnsetKontraksiType onsetKontraksi)
    {
        UsiaKehamilan = usiaKehamilan;
        Gravida = gravida;
        Partus = partus;
        Abortus = abortus;
        OnsetKontraksi = onsetKontraksi;
        _listDeilvery = new List<PersalinanDeliveryType>();
    }

    public int UsiaKehamilan { get; init; }
    public int Gravida { get; init; }
    public int Partus { get; init; }
    public int Abortus { get; init; }
    public OnsetKontraksiType OnsetKontraksi { get; init; }
    public IEnumerable<PersalinanDeliveryType> Delivery => _listDeilvery.AsEnumerable();
    public void AddDelivery(PersalinanDeliveryType delivery) => _listDeilvery.Add(delivery);
    
    public static RiwayatPersalinanType Default => new RiwayatPersalinanType(0,0,0,0, OnsetKontraksiType.Default);
}

public class TarifRs
{
    public string prosedur_non_bedah { get; set; }
    public string prosedur_bedah { get; set; }
    public string konsultasi { get; set; }
    public string tenaga_ahli { get; set; }
    public string keperawatan { get; set; }
    public string penunjang { get; set; }
    public string radiologi { get; set; }
    public string laboratorium { get; set; }
    public string pelayanan_darah { get; set; }
    public string rehabilitasi { get; set; }
    public string kamar { get; set; }
    public string rawat_intensif { get; set; }
    public string obat { get; set; }
    public string obat_kronis { get; set; }
    public string obat_kemoterapi { get; set; }
    public string alkes { get; set; }
    public string bmhp { get; set; }
    public string sewa_alat { get; set; }
}

