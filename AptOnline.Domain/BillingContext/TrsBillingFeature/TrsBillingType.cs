using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext.SkemaTarifFeature;
using Ardalis.GuardClauses;
namespace AptOnline.Domain.BillingContext.TrsBillingFeature;

public class TrsBillingModel
{
    private readonly List<TrsBillingTarifType> _listTarif;

    public TrsBillingModel(RegRefference reg)
    {
        Guard.Against.Null(reg, nameof(reg));
        Reg = reg;
        _listTarif = new List<TrsBillingTarifType>();
    }
    public RegRefference Reg { get; init; }
    public IEnumerable<TrsBillingTarifType> ListTarif => _listTarif;
    public static TrsBillingModel Default => new TrsBillingModel(RegType.Default.ToRefference());
    
    public void AddBiaya(string trsId, string refBiaya, 
        decimal nilai, int modul, SkemaTarifType skemaTarif)
    {
        Guard.Against.Null(trsId, nameof(trsId));
        Guard.Against.Null(refBiaya, nameof(refBiaya));
        _listTarif.Add(new TrsBillingTarifType(trsId, refBiaya, nilai, modul, skemaTarif));
    }
}