using AptOnline.Domain.EKlaimContext.PayorFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.PayorFeature;

public class PayorListTest
{
    private readonly PayorListHandler _sut;
    private readonly Mock<IPayorDal> _payorDal;

    public PayorListTest()
    {
        _payorDal = new Mock<IPayorDal>();
        _sut = new PayorListHandler(_payorDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<PayorGetResponse> { new("A", "B", "C") };
        var result = new List<PayorType> { new("A", "B", "C" )};
        _payorDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<PayorType>>(result));
        var actual = await _sut.Handle(new PayorListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _payorDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<PayorType>>(null!));
        var actual = () => _sut.Handle(new PayorListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}