namespace AptOnline.Domain.EKlaimContext;

public class OrderClass
{
    private readonly List<ItemProductType> _listProduct;
    public OrderClass(string orderId, DateTime orderDate, string customerName, 
        string shipmentAddress)
    {
        OrderId = orderId;
        OrderDate = orderDate;
        CustomerName = customerName;
        ShipmentAddress = shipmentAddress;
        _listProduct = new List<ItemProductType>();
    }

    public string OrderId { get; init; }
    public DateTime OrderDate { get; init; }
    public string CustomerName { get; init; }
    public string ShipmentAddress { get; init; }
    public decimal TotalBill => _listProduct.Sum(x => x.SubTotal);

    public IEnumerable<ItemProductType> Products => _listProduct.AsEnumerable();

    public void AddProduct(ItemProductType newItem) => _listProduct.Add(newItem);
}

public record ItemProductType( int NoUrut, string ProductId,
    string ProductName, int Qty, decimal UnitPrice, decimal SubTotal);

