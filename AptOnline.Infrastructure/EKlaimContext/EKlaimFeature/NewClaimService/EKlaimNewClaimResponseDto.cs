using AptOnline.Infrastructure.EKlaimContext.Shared;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.NewClaimService;

public class EKlaimNewClaimResponseDto
{
    public string patient_id { get; set; }
    public string admission_id { get; set; }
    public string hospital_admission_id { get; set; }
}

public class EKlaimNewClaimResponseEnvelope
{
    public EKlaimResponseMeta metadata { get; set; }
    public EKlaimNewClaimResponseDto response { get; set; }
}