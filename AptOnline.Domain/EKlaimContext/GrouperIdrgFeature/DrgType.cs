namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

//  DRG = Diagnosis-Related-Group
public record DrgType(string DrgId, string DrgName)
{
    public static DrgType Default => new DrgType("-", "-");
}