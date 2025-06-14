using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.DischargeStatusFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.DischargeStatusFeature;

public class DischargeStatusListTest
{
    private readonly DischargeStatusListHandler _sut;
    private readonly Mock<IDischargeStatusDal> _dischargeStatusDal;

    public DischargeStatusListTest()
    {
        _dischargeStatusDal = new Mock<IDischargeStatusDal>();
        _sut = new DischargeStatusListHandler(_dischargeStatusDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<DischargeStatusGetResponse> { new("A", "B") };
        var result = new List<DischargeStatusType> { new("A", "B") };
        _dischargeStatusDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<DischargeStatusType>>(result));
        var actual = await _sut.Handle(new DischargeStatusListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _dischargeStatusDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<DischargeStatusType>>(null!));
        var actual = () => _sut.Handle(new DischargeStatusListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}