using AptOnline.Domain.EKlaimContext.IdrgFeature;

namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

public record GrouperIdrgProsedurType
{
    public GrouperIdrgProsedurType(int noUrut, IdrgRefferenceType idrg, int multiplicity, int setting)
    {
        NoUrut = noUrut;
        Idrg = idrg;
        Multiplicity = multiplicity;
        Setting = setting;
        
    }
    public int NoUrut { get; private set; }
    public IdrgRefferenceType Idrg { get; init; }
    public int Multiplicity { get; init; }
    public int Setting { get; init; }
    public void SetNoUrut(int noUrut) => NoUrut = noUrut;
    
}