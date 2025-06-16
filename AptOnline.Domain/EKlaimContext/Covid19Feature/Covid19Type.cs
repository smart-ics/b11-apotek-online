using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.Covid19Feature;

public record Covid19Type
{
    private Covid19Type(Covid19StatusType status,
        TipeNoKartuType tipeNoKartu,
        Covid19JenazahType jenazah,
        bool isIsoman,
        string Episodes,
        string AksesNaat)
    {
        Guard.Against.Null(status, nameof(status));
        Guard.Against.Null(tipeNoKartu, nameof(tipeNoKartu));
        Guard.Against.Null(jenazah, nameof(jenazah));
        
        Status = status;
        TipeNoKartu = tipeNoKartu;
        Jenazah = jenazah;
        IsIsoman = isIsoman;
        this.Episodes = Episodes;
        this.AksesNaat = AksesNaat;
    }
    
    public Covid19StatusType Status { get; init; }
    public TipeNoKartuType TipeNoKartu { get; init; }
    public Covid19JenazahType Jenazah { get; init; }
    public bool IsIsoman { get; init; }
    public string Episodes { get; init; }
    public string AksesNaat { get; init; }
    
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