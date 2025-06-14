using AptOnline.Domain.EKlaimContext.BayiLahirFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.BayiLahirFeature;

public class BayiLahirStatusListTest
{
    private readonly BayiLahirStatusListHandler _sut;
    private readonly Mock<IBayiLahirStatusDal> _bayiLahirStatusDal;

    public BayiLahirStatusListTest()
    {
        _bayiLahirStatusDal = new Mock<IBayiLahirStatusDal>();
        _sut = new BayiLahirStatusListHandler(_bayiLahirStatusDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<BayiLahirStatusGetResponse> { new("A", "B") };
        var result = new List<BayiLahirStatusType> { new("A", "B") };
        _bayiLahirStatusDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<BayiLahirStatusType>>(result));
        var actual = await _sut.Handle(new BayiLahirStatusListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _bayiLahirStatusDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<BayiLahirStatusType>>(null!));
        var actual = () => _sut.Handle(new BayiLahirStatusListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}