using AptOnline.Domain.EKlaimContext.IdrgFeature;

namespace AptOnline.Infrastructure.EKlaimContext.IdrgFeature;

public record IdrgDto(
    string IdrgId,
    string Code2,
    string IdrgName,
    string StdSystem,
    int Kategori,
    bool ValidCode,
    string Accpdx,
    bool Asterisk,
    bool Im)
{
    public IdrgType ToModel()
    {
        var isAllowPrimary = Accpdx == "Y" ? true : false;
        var result = new IdrgType(IdrgId, Code2, IdrgName, StdSystem, (IdrgKetegoriEnum)Kategori, ValidCode, isAllowPrimary, Asterisk, Im);
        return result;
    }
}