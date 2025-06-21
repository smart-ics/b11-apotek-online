using AptOnline.Domain.BillingContext.RegAgg;
using Ardalis.GuardClauses;
namespace AptOnline.Domain.BillingContext.TrsBillingFeature;

public class TrsBillingModel
{
    private readonly List<TrsBillingBiayaType> _listTarif;

    public TrsBillingModel(RegRefference reg)
    {
        Guard.Against.Null(reg, nameof(reg));
        Reg = reg;
        _listTarif = new List<TrsBillingBiayaType>();
    }
    public RegRefference Reg { get; init; }
    public IEnumerable<TrsBillingBiayaType> ListTarif => _listTarif;
    public static TrsBillingModel Default => new TrsBillingModel(RegType.Default.ToRefference());
    
    public void AddBiaya(string trsId, ReffBiayaType reffBiaya, 
        decimal nilai)
    {
        Guard.Against.NullOrWhiteSpace(trsId, nameof(trsId));
        Guard.Against.Null(reffBiaya, nameof(reffBiaya));
        _listTarif.Add(new TrsBillingBiayaType(trsId, reffBiaya, nilai));
    }
}