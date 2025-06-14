namespace AptOnline.Domain.EKlaimContext.Covid19Feature;

public record Covid19Type(Covid19JenazahType Jenazah, 
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
    
    //public static Covid19Type Default => new(false, false, false, false, false, false, false);
}
public record Covid19JenazahType(
    bool IsPemulasaranJenazah,
    bool IsKantongJenazah,
    bool IsPetiJenazah,
    bool IsPlastikErat,
    bool IsDesinfektanJenazah,
    bool IsMobilJenazah,
    bool IsDesinfektanMobilJenazah);