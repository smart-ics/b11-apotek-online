namespace AptOnline.Domain.EKlaimContext.Covid19Feature;

public record Covid19Type(
    bool IsPemulasaranJenazah,
    bool IsKantongJenazah,
    bool IsPetiJenazah,
    bool IsPlastikErat,
    bool IsDesinfektanJenazah,
    bool IsMobilJenazah,
    bool IsDesinfektanMobilJenazah)
{
    public bool IsCovid19 => IsPemulasaranJenazah || IsKantongJenazah 
        || IsPetiJenazah || IsPlastikErat || IsDesinfektanJenazah 
        || IsMobilJenazah || IsDesinfektanMobilJenazah;
    
    public static Covid19Type Default => new(false, false, false, false, false, false, false);
}
