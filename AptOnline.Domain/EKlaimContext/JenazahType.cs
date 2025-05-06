namespace AptOnline.Domain.EKlaimContext;

public record JenazahType
{
    public JenazahType(YesNoIndikatorType pemulasaraanJenazah, YesNoIndikatorType kantongJenazah, YesNoIndikatorType petiJenazah, YesNoIndikatorType plastikErat, YesNoIndikatorType desinfektanJenazah, YesNoIndikatorType mobilJenazah, YesNoIndikatorType desinfektanMobilJenazah)
    {
        PemulasaraanJenazah = pemulasaraanJenazah;
        KantongJenazah = kantongJenazah;
        PetiJenazah = petiJenazah;
        PlastikErat = plastikErat;
        DesinfektanJenazah = desinfektanJenazah;
        MobilJenazah = mobilJenazah;
        DesinfektanMobilJenazah = desinfektanMobilJenazah;
    }

    public static JenisRawatType Default => new JenazahType(
        YesNoIndikatorType.Default,
        YesNoIndikatorType.Default,
        YesNoIndikatorType.Default,
        YesNoIndikatorType.Default,
        YesNoIndikatorType.Default,
        YesNoIndikatorType.Default,
        YesNoIndikatorType.Default);

    public YesNoIndikatorType PemulasaraanJenazah { get; init; }
    public YesNoIndikatorType KantongJenazah { get; init; }
    public YesNoIndikatorType PetiJenazah { get; init; }
    public YesNoIndikatorType PlastikErat { get; init; }
    public YesNoIndikatorType DesinfektanJenazah { get; init; }
    public YesNoIndikatorType MobilJenazah { get; init; }
    public YesNoIndikatorType DesinfektanMobilJenazah { get; init; }
    
}