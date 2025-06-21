using AptOnline.Domain.BillingContext.TrsBillingFeature;
using AptOnline.Domain.EKlaimContext.SkemaTarifJknFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record TarifRsType
{
    private readonly List<TarifRsSkemaType> _listSkema;

    public TarifRsType(IEnumerable<TarifRsSkemaType> listSkema)
    {
        _listSkema = listSkema.ToList();
    }

    public IEnumerable<TarifRsSkemaType> ListSkema => _listSkema;
    public static TarifRsType Default => new TarifRsType(Enumerable.Empty<TarifRsSkemaType>());
    
    public static TarifRsType Create(TrsBillingModel trsBilling, IEnumerable<SkemaReffBiayaMapType> map) 
    {
        var listSkema = new List<TarifRsSkemaType>();
        var listMap = map.ToList();
        foreach (var tarif in trsBilling.ListTarif)
        {
            var skemaMap = listMap.FirstOrDefault(x => x.ReffBiaya == tarif.ReffBiaya);
            if (skemaMap is null)
                continue;
            
            var newTarifRsType = new TarifRsSkemaType(tarif.ReffBiaya, skemaMap.SkemaTarifJkn, tarif.Nilai);
            listSkema.Add(newTarifRsType);
        }

        return new TarifRsType(listSkema);
    }
}