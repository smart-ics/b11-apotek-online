using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Xunit;

namespace AptOnline.Infrastructure.BillingContext.RoomChargeFeature;

public class RoomChargeListDalTest
{
    private readonly RoomChargeListDal _sut;
    public RoomChargeListDalTest()
    {
        var opt = FakeAppSetting.GetDatabaseOptions();
        _sut =  new RoomChargeListDal(opt);
    }

    [Fact]
    public void UT1_GivenValidRegID_WhenExecute_ThenReturnAsExpected()
    {
        var actual = _sut.ListData(RegType.Key("A"));
        actual.HasValue.Should().BeFalse();
    }
}