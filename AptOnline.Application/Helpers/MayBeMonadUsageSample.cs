namespace AptOnline.Application.Helpers;

public class MayBeMonadUsageSample
{
    public static void SampleUsage()
    {
        var orderDal = new OrderDal();
        
        //  1. Get Data and Throw Excpetion if not found
        var order = orderDal
            .GetData("12345")
            .GetValueOrThrow("Order not found");

        //  2. Check if data already exist then stop (throw exception)
        orderDal
            .GetData("")
            .ThrowIfSome(x => new ArgumentException("Order already exist"));

        //  3. Get Data and return default if not found
        var order2 = orderDal
            .GetData("12345")
            .Match(
                onSome: x => x,
                onNone: () => OrderModel.Default
            );

        //  4. Get CustomerName (string) from CustomerDal which is bind to Order
        var customerName = orderDal
            .GetData("12345")
            .Bind(x => new CustomerDal().GetData(x.CustomerId))
            .Map(customer => customer.Name)
            .GetValueOrDefault("Unknown Customer");

        //  5. Get Customer object from CustomerDal which is bind to Order
        var customer = orderDal
            .GetData("12345")
            .Bind(x => new CustomerDal().GetData(x.CustomerId))
            .Match(
                onSome: customer => customer,
                onNone: () => CustomerModel.Default
            );
    }
}

public record OrderModel(string OrderId, DateTime OrderDate, string CustomerId)
{
    public static OrderModel Default 
        => new(string.Empty, DateTime.MinValue, string.Empty);
}
public record CustomerModel(string CustomerId, string Name, string Email)
{
    public static CustomerModel Default 
        => new(string.Empty, string.Empty, string.Empty);
}

public class OrderDal
{
    public MayBe<OrderModel> GetData(string orderId)
    {
        // Simulate a database lookup
        if (string.IsNullOrEmpty(orderId))
        {
            return MayBe<OrderModel>.None;
        }
        var order = new OrderModel(orderId, DateTime.Now, "Customer A");
        return MayBe<OrderModel>.Some(order);
    }
}

public class CustomerDal
{
    public MayBe<CustomerModel> GetData(string customerId)
    {
        // Simulate a database lookup
        if (string.IsNullOrEmpty(customerId))
        {
            return MayBe<CustomerModel>.None;
        }
        var customer = new CustomerModel(customerId, "John Doe", "");
        return MayBe<CustomerModel>.Some(customer);
    }
}