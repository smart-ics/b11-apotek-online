using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext;

public record EKlaimAdmPaymentType
{
    private EKlaimAdmPaymentType(string payorId, string payorCode, string cobCode)
    {
        PayorId = payorId;
        PayorCode = payorCode;
        CobCode = cobCode;
    }
    public static EKlaimAdmPaymentType Create(string payorId, string payorCode, string cobCode)
    {
        Guard.Against.NullOrWhiteSpace(payorId, nameof(payorId));
        Guard.Against.NullOrWhiteSpace(payorCode, nameof(payorCode));
        Guard.Against.NullOrWhiteSpace(cobCode, nameof(cobCode));
        
        return new EKlaimAdmPaymentType(payorId, payorCode, cobCode);
    }
    public static EKlaimAdmPaymentType Load(string payorId, string payorCode, string cobCode) 
        => new(payorId, payorCode, cobCode);

    public static EKlaimAdmPaymentType Default 
        => new EKlaimAdmPaymentType(string.Empty, string.Empty, string.Empty); 

    
    public string PayorId { get ;init; }
    public string PayorCode { get ;init; } 
    public string CobCode { get ;init; }
}
