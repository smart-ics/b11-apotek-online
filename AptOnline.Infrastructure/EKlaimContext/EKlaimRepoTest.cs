using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using AptOnline.Infrastructure.Helpers;
using Nuna.Lib.TransactionHelper;
using Xunit;

namespace AptOnline.Infrastructure.EKlaimContext;

public class EKlaimRepoTest
{
    private readonly EKlaimRepo _sut;

    public EKlaimRepoTest()
    {
        _sut = new EKlaimRepo(FakeAppSetting.GetDatabaseOptions());
    }

    private static EKlaimModel Faker() => EKlaimModel.Load(
        "A", new DateTime(2025,6,6), SepType.Default.ToRefference(), 
        PasienType.Default, PesertaBpjsType.Default.ToRefference());
    
    [Fact]
    public void UT1_InsertTest()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(Faker());
    }
}