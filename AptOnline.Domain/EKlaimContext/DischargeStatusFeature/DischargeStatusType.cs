using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.DischargeStatusFeature;

public record DischargeStatusType(string DischargeStatusId, string DischargeStatusName) : IDischargeStatusKey
{
    public static DischargeStatusType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new DischargeStatusType(id, name);
    }
    public static DischargeStatusType Default => new("-", "-");
    public static IDischargeStatusKey Key(string id)
        => Default with {DischargeStatusId = id};
    
    public static DischargeStatusType PersetujuanDokter => new("1", "Atas Pesetujuan Dokter");
    public static DischargeStatusType Dirujuk => new("2", "Dirujuk");
    public static DischargeStatusType AtasPermintaanSendiri => new("3", "Atas permintaan sendiri");
    public static DischargeStatusType Meninggal => new("4", "Meninggal");
    public static DischargeStatusType LainLain => new("5", "Lain-lain");
    public static IEnumerable<DischargeStatusType> ListData()
        => new[]
        {
            PersetujuanDokter, 
            Dirujuk, 
            AtasPermintaanSendiri, 
            Meninggal, 
            LainLain
        };
}