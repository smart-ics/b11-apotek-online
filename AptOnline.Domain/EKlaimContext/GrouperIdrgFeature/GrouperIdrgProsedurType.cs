using AptOnline.Domain.EKlaimContext.IdrgFeature;
using Ardalis.GuardClauses;
namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

public record GrouperIdrgProsedurType
{
    public GrouperIdrgProsedurType(int noUrut, IdrgRefferenceType idrg, int multiplicity, int setting)
    {
        Guard.Against.NegativeOrZero(multiplicity, nameof(multiplicity));
        Guard.Against.NegativeOrZero(setting, nameof(setting));
        
        NoUrut = noUrut;
        Idrg = idrg;
        Multiplicity = multiplicity;
        Setting = setting;
        
    }
    public int NoUrut { get; private set; }
    public IdrgRefferenceType Idrg { get; init; }
    public int Multiplicity { get; private set; }
    public int Setting { get; private set; }
    public void SetNoUrut(int noUrut) => NoUrut = noUrut;
    public void ChangeMultiplicity(int multiplicity) => Multiplicity = multiplicity;
    public void ChangeSetting(int setting) => Setting = setting;
    
}