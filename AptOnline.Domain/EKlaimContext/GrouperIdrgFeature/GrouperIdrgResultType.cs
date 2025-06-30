namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

public record GrouperIdrgResultType(string InfoResult, string JenisRawat, 
    MdcType Mdc, DrgType Drg, string StatusResult)
{
    public static GrouperIdrgResultType Default => new(string.Empty, string.Empty, 
        MdcType.Default, DrgType.Default, string.Empty);
}

public enum GroupingPhaseEnum
{
    BelumGrouping,
    GroupingTapiGagal,
    GroupingDanBerhasil,
    Final
}