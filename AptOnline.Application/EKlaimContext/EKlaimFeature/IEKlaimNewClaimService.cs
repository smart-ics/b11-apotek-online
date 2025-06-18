using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.Helpers;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.EklaimContext;

public interface IEKlaimNewClaimService : INunaService<EKlaimNewClaimDto, EKlaimModel>
{
}
public record EKlaimNewClaimDto(bool IsSuccess, string Message);