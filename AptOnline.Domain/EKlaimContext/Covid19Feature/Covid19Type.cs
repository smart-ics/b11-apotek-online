using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.Covid19Feature;

public record Covid19Type(Covid19StatusType Status,
    TipeNoKartuType TipeNoKartu,
    Covid19JenazahType Jenazah,
    bool IsIsoman,
    string Episodes,
    string AksesNaat)
{
    public static Covid19Type Create(Covid19StatusType status,
        TipeNoKartuType tipeNoKartu,
        Covid19JenazahType jenazah,
        bool isIsoman)
    {
        Guard.Against.Null(status, nameof(status));
        Guard.Against.Null(tipeNoKartu, nameof(tipeNoKartu));
        Guard.Against.Null(jenazah, nameof(jenazah));
        
        return new Covid19Type(status, tipeNoKartu, jenazah, isIsoman, string.Empty, string.Empty);
    }
    
    public static Covid19Type Default => new Covid19Type(Covid19StatusType.Default, TipeNoKartuType.Default, 
        Covid19JenazahType.Default(), false, string.Empty, string.Empty);
}

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