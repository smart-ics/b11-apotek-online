using AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;
using AptOnline.Domain.EKlaimContext.IdrgFeature;

namespace AptOnline.Infrastructure.EKlaimContext.GrouperIdrgFeature;

public class GrouperIdrgDiagDto
{
    public GrouperIdrgDiagDto(string eKlaimId,GrouperIdrgDiagnosaType diag)
    {
        EKlaimId = eKlaimId;
        NoUrut = diag.NoUrut;
        IdrgId = diag.Idrg.IdrgId;
        Im = diag.Idrg.Im;
        IdrgName = diag.Idrg.IdrgName;
    }

    public GrouperIdrgDiagDto()
    {
    }

    public string EKlaimId { get; set; }
    public int NoUrut { get; set; }
    public string IdrgId { get; set; }
    public bool Im { get; set; }
    public string IdrgName { get; set; }
    
    public GrouperIdrgDiagnosaType ToModel() 
        => new(NoUrut, new IdrgRefferenceType(IdrgId, IdrgName, Im));
}