namespace AptOnline.Domain.EKlaimContext.IdrgFeature;

public abstract record IdrgAbstract(string IdrgId, string IdrgName, bool Im) : IIdrgKey
{
    public static IIdrgKey Key(string idrgId, bool im) => new IdrgMorfologiType(idrgId, "", im);
}

public record IdrgDiagnosaType(string IdrgId, string IdrgName, bool Im, bool IsAllowPrimary, bool IsAsterisk)
    : IdrgAbstract(IdrgId, IdrgName, Im);
public record IdrgProsedurType(string IdrgId, string IdrgName, bool Im) : IdrgAbstract(IdrgId, IdrgName, Im);
public record IdrgMorfologiType(string IdrgId, string IdrgName, bool Im) : IdrgAbstract(IdrgId, IdrgName, Im);

