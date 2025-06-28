using AptOnline.Domain.EKlaimContext.IdrgFeature;

namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

public record GrouperIdrgDiagnosaType
{
    public GrouperIdrgDiagnosaType(int noUrut, IdrgRefferenceType idrg)
    {
        NoUrut = noUrut;
        Idrg = idrg;
    }
    public int NoUrut { get; private set; }
    public IdrgRefferenceType Idrg { get; init; } 
    public bool IsPrimary => NoUrut == 1;
    public void SetNoUrut(int noUrut) => NoUrut = noUrut;
}