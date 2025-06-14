using AptOnline.Domain.EKlaimContext.DischargeStatusFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.DischargeStatusFeature;

public class DischargeStatusGetTest
{
    private readonly DischargeStatusGet _sut;
    private readonly Mock<IDischargeStatusDal> _dischargeStatusDal;

    public DischargeStatusGetTest()
    {
        _dischargeStatusDal = new Mock<IDischargeStatusDal>();
        _sut = new DischargeStatusGet(_dischargeStatusDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new  DischargeStatusGetResponse("A", "B");
        _dischargeStatusDal.Setup(x => x.GetData(It.IsAny<IDischargeStatusKey>()))
            .Returns(MayBe.From(new DischargeStatusType("A", "B")));
        var actual = await _sut.Handle(new DischargeStatusGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _dischargeStatusDal.Setup(x => x.GetData(It.IsAny<IDischargeStatusKey>()))
            .Returns(MayBe.From<DischargeStatusType>(null!));
        var actual = () => _sut.Handle(new DischargeStatusGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}