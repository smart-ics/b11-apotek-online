using AptOnline.Domain.EKlaimContext.PayorFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.PayorFeature;

public class PayorGetTest
{
    private readonly PayorGet _sut;
    private readonly Mock<IPayorDal> _payorDal;

    public PayorGetTest()
    {
        _payorDal = new Mock<IPayorDal>();
        _sut = new PayorGet(_payorDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new  PayorGetResponse("A", "B", "C");
        _payorDal.Setup(x => x.GetData(It.IsAny<IPayorKey>()))
            .Returns(MayBe.From(new PayorType("A", "B", "C")));
        var actual = await _sut.Handle(new PayorGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _payorDal.Setup(x => x.GetData(It.IsAny<IPayorKey>()))
            .Returns(MayBe.From<PayorType>(null!));
        var actual = () => _sut.Handle(new PayorGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}