    public class Apgar
    {
    public Menit1 menit_1 { get; set; }
    public Menit5 menit_5 { get; set; }
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

    public class Delivery
    {
        public string delivery_sequence { get; set; }
        public string delivery_method { get; set; }
        public string delivery_dttm { get; set; }
        public string letak_janin { get; set; }
        public string kondisi { get; set; }
        public string use_manual { get; set; }
        public string use_forcep { get; set; }
        public string use_vacuum { get; set; }
        public string shk_spesimen_ambil { get; set; }
        public string shk_lokasi { get; set; }
        public string shk_spesimen_dttm { get; set; }
        public string shk_alasan { get; set; }
    }

    public class Menit1
    {
        public int appearance { get; set; }
        public int pulse { get; set; }
        public int grimace { get; set; }
        public int activity { get; set; }
        public int respiration { get; set; }
    }

    public class Menit5
    {
        public int appearance { get; set; }
        public int pulse { get; set; }
        public int grimace { get; set; }
        public int activity { get; set; }
        public int respiration { get; set; }
    }

    public class Persalinan
    {
        public string usia_kehamilan { get; set; }
        public string gravida { get; set; }
        public string partus { get; set; }
        public string abortus { get; set; }
        public string onset_kontraksi { get; set; }
        public List<Delivery> delivery { get; set; }
    }

Level 0:

    public class Root
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

        public Ventilator ventilator { get; set; }

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

        public Apgar apgar { get; set; }

        public Persalinan persalinan { get; set; }

        public string tarif_poli_eks { get; set; }
        public string nama_dokter { get; set; }
        public string kode_tarif { get; set; }
        public string payor_id { get; set; }
        public string payor_cd { get; set; }
        public string cob_cd { get; set; }
        public string coder_nik { get; set; }
    }

Level 1:

    public class Ventilator
    {
        public string use_ind { get; set; }
        public string start_dttm { get; set; }
        public string stop_dttm { get; set; }
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

