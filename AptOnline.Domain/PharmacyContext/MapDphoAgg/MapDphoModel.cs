namespace AptOnline.Domain.PharmacyContext.MapDphoAgg;

public class MapDphoModel : IBrgKey
{
    public string BrgId { get; set; }
    public string BrgName { get; set; }
    public string DphoId { get; set; }
    public string DphoName { get; set; }
}

public interface IBrgKey
{
    string BrgId { get; }
}