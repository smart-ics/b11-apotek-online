using AptOnline.Domain.EKlaimContext.IdrgFeature;

namespace AptOnline.Infrastructure.EKlaimContext.IdrgFeature;

public record IdrgDto( string IdrgId, string Code2, string IdrgName,
    string StdSystem, int Kategori, bool ValidCode, string Accpdx,
    bool Asterisk, bool Im)
{
    public IdrgAbstract ToModel()
    {
        var isAllowPrimary = Accpdx == "Y" ? true : false;
        IdrgAbstract result = Kategori switch
        {
            0 => new IdrgDiagnosaType(IdrgId, IdrgName, Im, isAllowPrimary, Asterisk),
            1 => new IdrgProsedurType(IdrgId, IdrgName, Im),
            2 => new IdrgMorfologiType(IdrgId, IdrgName, Im),
            _ => throw new ArgumentOutOfRangeException(nameof(Kategori), Kategori, null)
        };
        return result;
    }
}