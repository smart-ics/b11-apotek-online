using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public record EKlaimMedJenazahType
{
    public EKlaimMedJenazahType(YesNoIndikatorValType pemulasaraanJenazah, YesNoIndikatorValType kantongJenazah, 
        YesNoIndikatorValType petiJenazah, YesNoIndikatorValType plastikErat, YesNoIndikatorValType desinfektanJenazah, 
        YesNoIndikatorValType mobilJenazah, YesNoIndikatorValType desinfektanMobilJenazah)
    {
        Guard.NotNull(pemulasaraanJenazah, nameof(pemulasaraanJenazah), "Pemulasaraan Jenazah Tidak boleh kosong");
        Guard.NotNull(kantongJenazah, nameof(kantongJenazah), "Kantong Jenazah Tidak boleh kosong");
        Guard.NotNull(petiJenazah, nameof(petiJenazah), "Peti Jenazah Tidak boleh kosong");
        Guard.NotNull(plastikErat, nameof(plastikErat), "Plastik Erat Tidak boleh kosong");
        Guard.NotNull(desinfektanJenazah, nameof(desinfektanJenazah), "Desinfektan Jenazah Tidak boleh kosong");
        Guard.NotNull(mobilJenazah, nameof(mobilJenazah), "Mobil Jenazah Tidak boleh kosong");
        Guard.NotNull(desinfektanMobilJenazah, nameof(desinfektanMobilJenazah), "Desinfektan Mobil Jenazah Tidak boleh kosong");
        
        PemulasaraanJenazah = pemulasaraanJenazah;
        KantongJenazah = kantongJenazah;
        PetiJenazah = petiJenazah;
        PlastikErat = plastikErat;
        DesinfektanJenazah = desinfektanJenazah;
        MobilJenazah = mobilJenazah;
        DesinfektanMobilJenazah = desinfektanMobilJenazah;
    }
    
    public static EKlaimMedJenazahType Default => new EKlaimMedJenazahType(
        YesNoIndikatorValType.Default, YesNoIndikatorValType.Default, YesNoIndikatorValType.Default, 
        YesNoIndikatorValType.Default, YesNoIndikatorValType.Default, YesNoIndikatorValType.Default, 
        YesNoIndikatorValType.Default);

    public YesNoIndikatorValType PemulasaraanJenazah { get; init; }
    public YesNoIndikatorValType KantongJenazah { get; init; }
    public YesNoIndikatorValType PetiJenazah { get; init; }
    public YesNoIndikatorValType PlastikErat { get; init; }
    public YesNoIndikatorValType DesinfektanJenazah { get; init; }
    public YesNoIndikatorValType MobilJenazah { get; init; }
    public YesNoIndikatorValType DesinfektanMobilJenazah { get; init; }
}
