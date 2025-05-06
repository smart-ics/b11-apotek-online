namespace AptOnline.Domain.EKlaimContext;

public record PersalinanType
{
    private readonly List<PersalinanDeliveryType> _listDeilvery;
    public PersalinanType(int usiaKehamilan, int gravida, int partus, 
        int abortus, OnsetKontraksiType onsetKontraksi)
    {
        UsiaKehamilan = usiaKehamilan;
        Gravida = gravida;
        Partus = partus;
        Abortus = abortus;
        OnsetKontraksi = onsetKontraksi;
        _listDeilvery = new List<PersalinanDeliveryType>();
    }

    public int UsiaKehamilan { get; init; }
    public int Gravida { get; init; }
    public int Partus { get; init; }
    public int Abortus { get; init; }
    public OnsetKontraksiType OnsetKontraksi { get; init; }
    public IEnumerable<PersalinanDeliveryType> Delivery 
        => _listDeilvery.AsEnumerable();
    public void AddDelivery(PersalinanDeliveryType delivery) 
        => _listDeilvery.Add(delivery);
    public void RemoveDelivery(int deliverySequence) =>
        _listDeilvery.RemoveAll(x => x.DeliverySequence == deliverySequence);
    
    public static PersalinanType Default 
        => new PersalinanType(0,0,0,0, OnsetKontraksiType.Default);
}