using AptOnline.Infrastructure.EKlaimContext.Shared;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.NewClaimService;

public class EKlaimNewClaimResponse
{
    public EKlaimResponseMeta metadata { get; set; }
    public EKlaimNewClaimRespDto response { get; set; }
    public static EKlaimNewClaimResponse Default 
        => new EKlaimNewClaimResponse
        {
            metadata = EKlaimResponseMeta.Default,
            response = EKlaimNewClaimRespDto.Default
        };
}

public class EKlaimNewClaimRespDto
{
    public string patient_id { get; set; }
    public string admission_id { get; set; }
    public string hospital_admission_id { get; set; }
    public static EKlaimNewClaimRespDto Default => new EKlaimNewClaimRespDto
    {
        patient_id = string.Empty,
        admission_id = string.Empty,
        hospital_admission_id = string.Empty
    };
}
