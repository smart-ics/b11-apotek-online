namespace AptOnline.Domain.EKlaimContext;

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
    public LokasiSpesimenType SpecimenLokasi { get; private set; }
    public string AlasanTidakSkrining { get; private set; }
    public DateTime SpecimenTimestamp { get; private set; }
}