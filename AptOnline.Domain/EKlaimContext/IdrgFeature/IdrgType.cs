namespace AptOnline.Domain.EKlaimContext.IdrgFeature;

public record class IdrgType(string IdrgId, string Code2, string IdrgName, string StdSystem,
    IdrgKetegoriEnum Kategori, bool IsValidCode, bool IsAllowPrimary, bool IsAsterisk, bool Im) : IIdrgKey
{
    public static IdrgType Default 
        => new IdrgType(string.Empty, string.Empty, string.Empty, string.Empty,IdrgKetegoriEnum.Undefined, 
            false, false, false, false);
    public static IIdrgKey Key(string idrgId, bool im) 
        => Default with { IdrgId = idrgId, Im = im };
}

//public record IdrgDiagnosaType(string IdrgId, string IdrgName, bool Im, bool IsAllowPrimary, ) : IIdrgKey;

public record IdrgRefferenceType(string IdrgId, string IdrgName, bool Im, IdrgKetegoriEnum StdSystem) : IIdrgKey;