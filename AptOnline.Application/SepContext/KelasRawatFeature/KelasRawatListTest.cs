using AptOnline.Domain.SepContext.KelasRawatFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.SepContext.KelasRawatFeature;

public class KelasRawatListTest
{
    private readonly KelasRawatListHandler _sut;
    private readonly Mock<IKelasRawatDal> _kelasRawatDal;

    public KelasRawatListTest()
    {
        _kelasRawatDal = new Mock<IKelasRawatDal>();
        _sut = new KelasRawatListHandler(_kelasRawatDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<KelasRawatGetResponse> { new("A", "B") };
        var result = new List<KelasRawatType> { new("A", "B") };
        _kelasRawatDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<KelasRawatType>>(result));
        var actual = await _sut.Handle(new KelasRawatListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _kelasRawatDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<KelasRawatType>>(null!));
        var actual = () => _sut.Handle(new KelasRawatListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}