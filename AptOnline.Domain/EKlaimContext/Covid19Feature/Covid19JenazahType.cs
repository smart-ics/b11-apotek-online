namespace AptOnline.Domain.EKlaimContext.Covid19Feature;

public record Covid19JenazahType(
    bool IsPemulasaranJenazah,
    bool IsKantongJenazah,
    bool IsPetiJenazah,
    bool IsPlastikErat,
    bool IsDesinfektanJenazah,
    bool IsMobilJenazah,
    bool IsDesinfektanMobilJenazah)
{
    public static Covid19JenazahType Default() => new Covid19JenazahType(false, false, 
        false, false, false, false, false);
}