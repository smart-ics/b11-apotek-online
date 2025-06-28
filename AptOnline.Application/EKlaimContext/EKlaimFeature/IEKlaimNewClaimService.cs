using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.EKlaimContext.EKlaimFeature;

public interface IEKlaimNewClaimService : INunaService<EKlaimNewClaimDto, EKlaimModel>
{
}
public record EKlaimNewClaimDto(bool IsSuccess, string Message);