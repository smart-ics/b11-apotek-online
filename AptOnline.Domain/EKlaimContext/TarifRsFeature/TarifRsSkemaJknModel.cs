namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record TarifRsSkemaJknModel
{
    private readonly List<TarifRsReffBiayaType> _listReffBiaya;
    public TarifRsSkemaJknModel(SkemaJknType skemaJkn)
    {
        SkemaJkn = skemaJkn;
        _listReffBiaya = new List<TarifRsReffBiayaType>();
    }

    public SkemaJknType SkemaJkn { get; init; }
    public decimal Nilai  => _listReffBiaya.Sum(t => t.Nilai);
    public IEnumerable<TarifRsReffBiayaType> ListReffBiaya => _listReffBiaya;

    public void AddReffBiaya(TarifRsReffBiayaType reffBiaya)
    {
        _listReffBiaya.Add(reffBiaya);
    }
}
