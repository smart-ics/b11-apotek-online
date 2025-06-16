using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.BillingContext.PegFeature;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.BillingContext.PegFeature;

public class PegListTest
{
    private readonly PegListHandler _sut;
    private readonly Mock<IPegDal> _pegDal;

    public PegListTest()
    {
        _pegDal = new Mock<IPegDal>();
        _sut = new PegListHandler(_pegDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<PegGetResponse> { new("A", "B", "C") };
        var result = new List<PegType> { new("A", "B", "C") };
        _pegDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<PegType>>(result));
        var actual = await _sut.Handle(new PegListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _pegDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<PegType>>(null!));
        var actual = () => _sut.Handle(new PegListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}