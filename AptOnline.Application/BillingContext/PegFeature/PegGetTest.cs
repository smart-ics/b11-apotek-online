using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.BillingContext.PegFeature;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.BillingContext.PegFeature;

public class PegGetTest
{
    private readonly PegGet _sut;
    private readonly Mock<IPegDal> _pegDal;

    public PegGetTest()
    {
        _pegDal = new Mock<IPegDal>();
        _sut = new PegGet(_pegDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new  PegGetResponse("A", "B", "C");
        _pegDal.Setup(x => x.GetData(It.IsAny<IPegKey>()))
            .Returns(MayBe.From(new PegType("A", "B", "C")));
        var actual = await _sut.Handle(new PegGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _pegDal.Setup(x => x.GetData(It.IsAny<IPegKey>()))
            .Returns(MayBe.From<PegType>(null!));
        var actual = () => _sut.Handle(new PegGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}