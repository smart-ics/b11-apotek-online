namespace AptOnline.Domain.Helpers;

public class MayBeMonadUsageSample
{
    private readonly RegTestDal _regDal = new();
    private readonly PasienTestDal _pasienDal = new();

    public void SampleUsage()
    {
        
        //  List UseCase:
        //  1. GetData dari Repository. Jika ngga ada maka throw exception
        var reg = _regDal
            .GetData("RG007")
            .GetValueOrThrow("Register not found");
        
        //  2. Cek duplikasi; jika sudah ada, maka throw exception
        _regDal
            .GetData("RG007")
            .ThrowIfSome(x => new ArgumentException("Register already exist"));

        //  3. GetData; jika tidak ditemukan maka return default (kasus data optional)
        var reg2 = _regDal
            .GetData("RG007")
            .Match(
                onSome: x => x,
                onNone: () => RegTestModel.Default
            );

        //  4. Get PasienName (string) from PasienDal dengan memanggil RegDal lebih dulu
        //     (Ini hanya contoh; biasanya kita lsg get nama pasien di-query RegDal)
        var pasienName = _regDal
            .GetData("RG007")
            .Bind(x => _pasienDal.GetData(x.PasienId))
            .Map(pasien => pasien.Name)
            .GetValueOrDefault("Unknown Pasien");

        //  5. GetPasien object from PasienDal dengan nge-bind ke RegDal
        var pasien = _regDal
            .GetData("RG007")
            .Bind(x => _pasienDal.GetData(x.PasienId))
            .Match(
                onSome: pasien => pasien,
                onNone: () => PasienTestModel.Default
            );
    }
}

public record RegTestModel(string RegId, DateTime RegDate, string PasienId)
{
    public static RegTestModel Default 
        => new(string.Empty, DateTime.MinValue, string.Empty);
}
public record PasienTestModel(string PasienId, string Name, string Email)
{
    public static PasienTestModel Default 
        => new(string.Empty, string.Empty, string.Empty);
}

public class RegTestDal
{
    public MayBe<RegTestModel> GetData(string orderId)
    {
        // Simulate a database lookup
        if (string.IsNullOrEmpty(orderId))
        {
            return MayBe<RegTestModel>.None;
        }
        var order = new RegTestModel(orderId, DateTime.Now, "Customer A");
        return MayBe<RegTestModel>.Some(order);
    }
}

public class PasienTestDal
{
    public MayBe<PasienTestModel> GetData(string customerId)
    {
        // Simulate a database lookup
        if (string.IsNullOrEmpty(customerId))
        {
            return MayBe<PasienTestModel>.None;
        }
        var customer = new PasienTestModel(customerId, "Agus Budiman", "");
        return MayBe<PasienTestModel>.Some(customer);
    }
}