namespace AptOnline.Domain.BillingContext.DokterAgg;

public record DokterModel(string DokterId, string DokterName) : IDokterKey;


public interface IDokterKey
{
    string DokterId { get; }
}