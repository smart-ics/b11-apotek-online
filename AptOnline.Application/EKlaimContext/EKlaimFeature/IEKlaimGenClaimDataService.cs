using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.EKlaimContext.EKlaimFeature;

public interface IEKlaimSetClaimDataService : INunaService<EKlaimSetClaimDataDto, EKlaimModel>
{
}

public record EKlaimSetClaimDataDto(bool IsSuccess, string Message);