using AptOnline.Domain.BillingContext.RegAgg;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public class TarifRsModel
{
    private readonly List<TarifRsReffBiayaType> _listReffBiaya;
    private readonly List<TarifRsSkemaJknModel> _listSkema;

    public TarifRsModel(RegRefference reg)
    {
        Guard.Against.Null(reg, nameof(reg));
        
        Reg = reg;
        _listReffBiaya = new List<TarifRsReffBiayaType>();
        _listSkema = new  List<TarifRsSkemaJknModel>();
        
    }
    public RegRefference Reg { get; init; }
    public IEnumerable<TarifRsReffBiayaType> ListReffBiaya => _listReffBiaya;
    public IEnumerable<TarifRsSkemaJknModel> ListSkema => _listSkema;
    
    
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
    
    public void GenerateSkemaJkn(IMapSkemaJknDal mapSkemaJknType)
    {
        foreach (var item in _listReffBiaya)
        {
            var skemaMap = mapSkemaJknType.GetData(item.ReffBiaya);
            if (!skemaMap.HasValue)
                continue;
            
            var skema = _listSkema.FirstOrDefault(x => x.SkemaJkn == skemaMap.Value.SkemaJkn);
            if (skema is null)
            {
                skema = new TarifRsSkemaJknModel(skemaMap.Value.SkemaJkn);
                _listSkema.Add(skema);
            }
            
            skema.AddReffBiaya(item);
        }
    }

    public static TarifRsModel Default => new TarifRsModel(RegType.Default.ToRefference());
}