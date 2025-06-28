using AptOnline.Infrastructure.EKlaimContext.Shared;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.NewClaimService;

public class EKlaimNewClaimResponse
{
    public EKlaimResponseMeta metadata { get; set; }
    public EKlaimNewClaimResponseData response { get; set; }
    public static EKlaimNewClaimResponse Default 
        => new EKlaimNewClaimResponse
        {
            metadata = EKlaimResponseMeta.Default,
            response = EKlaimNewClaimResponseData.Default
        };
}

public class EKlaimNewClaimResponseData
{
    public string patient_id { get; set; }
    public string admission_id { get; set; }
    public string hospital_admission_id { get; set; }
    public static EKlaimNewClaimResponseData Default => new EKlaimNewClaimResponseData
    {
        patient_id = string.Empty,
        admission_id = string.Empty,
        hospital_admission_id = string.Empty
    };
}
