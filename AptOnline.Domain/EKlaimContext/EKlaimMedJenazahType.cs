namespace AptOnline.Domain.EKlaimContext;

public record EKlaimMedJenazahType(
    YesNoIndikatorValType PemulasaraanJenazah,
    YesNoIndikatorValType KantongJenazah,
    YesNoIndikatorValType PetiJenazah,
    YesNoIndikatorValType PlastikErat,
    YesNoIndikatorValType DesinfektanJenazah,
    YesNoIndikatorValType MobilJenazah,
    YesNoIndikatorValType DesinfektanMobilJenazah);
