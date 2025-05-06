namespace AptOnline.Domain.EKlaimContext;

public record PersalinanDeliveryType
{
    public PersalinanDeliveryType(int deliverySequence, string deliveryMethod, DateTime deliveryTimestamp, LetakJaninType letakJanin, KondisiJaninType kondisiJanin, bool isUseManual, bool isUseForcep, bool isUseVacuum, SkriningHkType skriningHk)
    {
        DeliverySequence = deliverySequence;
        DeliveryMethod = deliveryMethod;
        DeliveryTimestamp = deliveryTimestamp;
        LetakJanin = letakJanin;
        KondisiJanin = kondisiJanin;
        IsUseManual = isUseManual;
        IsUseForcep = isUseForcep;
        IsUseVacuum = isUseVacuum;
        SkriningHk = skriningHk;
    }
    public int DeliverySequence { get; init; }
    public string DeliveryMethod { get; init; }
    public DateTime DeliveryTimestamp { get; init; }
    public LetakJaninType LetakJanin { get; init; }
    public KondisiJaninType KondisiJanin { get; init; }
    public bool IsUseManual { get; init; }
    public bool IsUseForcep { get; init; }
    public bool IsUseVacuum { get; init; }
    public SkriningHkType SkriningHk { get; init; }
}