namespace AptOnline.Domain.EKlaimContext.IdrgFeature;

public record class IdrgType(string IdrgId, string Code2, string IdrgName, IdrgStdSystemEnum StdSystem,
    bool IsValidCode, bool IsAllowPrimary, bool IsAsterisk, bool Im) : IIdrgKey
{

    public static IdrgType Default 
        => new IdrgType(string.Empty, string.Empty, string.Empty, IdrgStdSystemEnum.Undefined, 
            false, false, false, false);
    public static IIdrgKey Key(string idrgId, bool im) 
        => Default with { IdrgId = idrgId, Im = im };
    
}