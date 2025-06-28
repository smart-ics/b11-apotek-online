using AptOnline.Domain.EKlaimContext.IdrgFeature;

namespace AptOnline.Infrastructure.EKlaimContext.IdrgFeature;

public record IdrgDto(
    string IdrgId,
    string Code2,
    string IdrgName,
    string StdSystem,
    bool ValidCode,
    string Accpdx,
    bool Asterisk,
    bool Im)
{
    public IdrgType ToModel()
    {
        var stdSystem = StdSystem switch
        {
            "ICD_10_2010_IM" => IdrgStdSystemEnum.Diagnosa,
            "ICD_9CM_2010_IM" => IdrgStdSystemEnum.Prosedur,
            "ICD_O_MORFOLOGI" => IdrgStdSystemEnum.Morfologi,
            _ => IdrgStdSystemEnum.Undefined
        };
        var isAllowPrimary = Accpdx == "Y" ? true : false;
        var result = new IdrgType(IdrgId, Code2, IdrgName, stdSystem, ValidCode, isAllowPrimary, Asterisk, Im);
        return result;
    }
}