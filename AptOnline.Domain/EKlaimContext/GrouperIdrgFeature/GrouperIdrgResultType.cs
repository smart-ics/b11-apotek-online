namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

public record GrouperIdrgResultType(string Info, string JenisRawat, 
    MdcType Mdc, DrgType Drg, string Status)
{
    public static GrouperIdrgResultType Default => new(string.Empty, string.Empty, 
        MdcType.Default, DrgType.Default, string.Empty);
    
    //  TODO: Setting Phase
    public GrouperPhaseEnum Phase { get; private set; } 
}

public enum GrouperPhaseEnum
{
    NotSet,
    SetButFailed,
    SetAndSuccess,
    Final
}