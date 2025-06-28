using AptOnline.Domain.EKlaimContext.BayiLahirFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.BillingContext.ParamSistemFeature;

public record ParamSistemType(string ParamSistemId, 
    string ParamSistemName, string ParamValue) : IParamSistemKey
{
    public static ParamSistemType Create(string id, string name, string paramValue)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.NullOrWhiteSpace(paramValue, nameof(paramValue));
        return new ParamSistemType(id, name, paramValue);
    }
    public static ParamSistemType Default => new("-", "-", "-");
    public static IParamSistemKey Key(string id)
        => Default with {ParamSistemId = id};
}