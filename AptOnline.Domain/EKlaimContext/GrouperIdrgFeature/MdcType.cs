namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

//  MDC = Major-Diagnostic-Category
public record MdcType(string MdcId, string MdcName)
{
    public static MdcType Default => new MdcType("-","-");
}