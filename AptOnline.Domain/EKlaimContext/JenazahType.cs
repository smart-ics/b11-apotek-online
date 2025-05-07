namespace AptOnline.Domain.EKlaimContext;

public record JenazahType
{
    public JenazahType(YesNoIndikatorValType pemulasaraanJenazah, YesNoIndikatorValType kantongJenazah, YesNoIndikatorValType petiJenazah, YesNoIndikatorValType plastikErat, YesNoIndikatorValType desinfektanJenazah, YesNoIndikatorValType mobilJenazah, YesNoIndikatorValType desinfektanMobilJenazah)
    {
        PemulasaraanJenazah = pemulasaraanJenazah;
        KantongJenazah = kantongJenazah;
        PetiJenazah = petiJenazah;
        PlastikErat = plastikErat;
        DesinfektanJenazah = desinfektanJenazah;
        MobilJenazah = mobilJenazah;
        DesinfektanMobilJenazah = desinfektanMobilJenazah;
    }

    public static JenisRawatValType Default => new JenazahType(
        YesNoIndikatorValType.Default,
        YesNoIndikatorValType.Default,
        YesNoIndikatorValType.Default,
        YesNoIndikatorValType.Default,
        YesNoIndikatorValType.Default,
        YesNoIndikatorValType.Default,
        YesNoIndikatorValType.Default);

    public YesNoIndikatorValType PemulasaraanJenazah { get; init; }
    public YesNoIndikatorValType KantongJenazah { get; init; }
    public YesNoIndikatorValType PetiJenazah { get; init; }
    public YesNoIndikatorValType PlastikErat { get; init; }
    public YesNoIndikatorValType DesinfektanJenazah { get; init; }
    public YesNoIndikatorValType MobilJenazah { get; init; }
    public YesNoIndikatorValType DesinfektanMobilJenazah { get; init; }
    
}