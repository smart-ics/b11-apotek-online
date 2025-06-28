using AptOnline.Domain.BillingContext.RegAgg;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public class TarifRsModel
{
    private readonly List<TarifRsReffBiayaType> _listReffBiaya;

    public TarifRsModel(RegRefference reg)
    {
        Guard.Against.Null(reg, nameof(reg));
        
        Reg = reg;
        _listReffBiaya = new List<TarifRsReffBiayaType>();
        
    }
    public RegRefference Reg { get; init; }
    public IEnumerable<TarifRsReffBiayaType> ListReffBiaya => _listReffBiaya;
    public IEnumerable<TarifRsSkemaJknModel> ListSkema 
        => _listReffBiaya
            .GroupBy(x => x.SkemaJkn)
            .Select(g => g.Aggregate(
                new TarifRsSkemaJknModel(g.Key),
                (hdr, dtl) => 
                {
                    hdr.AddReffBiaya(dtl);
                    return hdr;
                }
            ));
    
    
    public void AddReffBiaya(string trsId, ReffBiayaType reffBiaya, 
        string ketBiaya, decimal nilai)
    {
        Guard.Against.NullOrWhiteSpace(trsId, nameof(trsId));
        Guard.Against.Null(reffBiaya, nameof(reffBiaya));
        var maxNoUrut = _listReffBiaya
            .DefaultIfEmpty(new TarifRsReffBiayaType(0, "-", ReffBiayaType.Default, "", 0))
            .Max(x => x.NoUrut);
        maxNoUrut++;
        _listReffBiaya.Add(new TarifRsReffBiayaType(maxNoUrut, trsId, reffBiaya, ketBiaya, nilai));
    }
    
    public void GenerateSkemaJkn(IMapSkemaJknDal mapSkemaJkn)
    {
        foreach (var item in _listReffBiaya)
        {
            var skemaMap = mapSkemaJkn.GetData(item.ReffBiaya);
            if (!skemaMap.HasValue)
                continue;
            item.SetSkemaJkn(skemaMap.Value.SkemaJkn);
        }
    }

    public static TarifRsModel Default => new TarifRsModel(RegType.Default.ToRefference());
    public static TarifRsModel Create(IEnumerable<TarifRsReffBiayaType> listReffBiaya)
    {
        var result = new TarifRsModel(RegType.Default.ToRefference());
        foreach (var item in listReffBiaya)
            result._listReffBiaya.Add(item);
        return result;
    } 
}