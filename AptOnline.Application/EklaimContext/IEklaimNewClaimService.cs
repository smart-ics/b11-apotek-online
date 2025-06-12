using AptOnline.Domain.EKlaimContext;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.EklaimContext;

public interface IEklaimNewClaimService : INunaService<EKlaimNewClaimDto, EklaimModel>
{
}
public class EKlaimNewClaimDto
{
    public EKlaimNewClaimDto(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; internal set; }
    public string Message { get; internal set; }
}