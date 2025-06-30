using AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;
using AptOnline.Domain.EKlaimContext.IdrgFeature;

namespace AptOnline.Infrastructure.EKlaimContext.GrouperIdrgFeature;

public class GrouperIdrgProsDto
{
    public GrouperIdrgProsDto(string eKlaimId, GrouperIdrgProsedurType pros)
    {
        EKlaimId = eKlaimId;
        NoUrut = pros.NoUrut;
        IdrgId = pros.Idrg.IdrgId;
        Im = pros.Idrg.Im;
        IdrgName = pros.Idrg.IdrgName;
        Multiplicity = pros.Multiplicity;
        Setting = pros.Setting;
    }

    public GrouperIdrgProsDto()
    {
    }

    public string EKlaimId { get; set; }
    public int NoUrut { get; set; }
    public string IdrgId { get; set; }
    public bool Im { get; set; }
    public string IdrgName { get; set; }
    public int Multiplicity { get; set; }
    public int Setting { get; set; }
    
    public GrouperIdrgProsedurType ToModel() 
        => new(NoUrut, new IdrgRefferenceType(IdrgId, IdrgName, Im),
            Multiplicity, Setting);
}