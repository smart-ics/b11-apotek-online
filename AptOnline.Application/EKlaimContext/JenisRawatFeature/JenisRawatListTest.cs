using AptOnline.Domain.EKlaimContext.JenisRawatFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.JenisRawatFeature;

public class JenisRawatListTest
{
    private readonly JenisRawatListHandler _sut;
    private readonly Mock<IJenisRawatDal> _jenisRawatDal;

    public JenisRawatListTest()
    {
        _jenisRawatDal = new Mock<IJenisRawatDal>();
        _sut = new JenisRawatListHandler(_jenisRawatDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<JenisRawatGetResponse> { new("A", "B") };
        var result = new List<JenisRawatType> { new("A", "B") };
        _jenisRawatDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<JenisRawatType>>(result));
        var actual = await _sut.Handle(new JenisRawatListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _jenisRawatDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<JenisRawatType>>(null!));
        var actual = () => _sut.Handle(new JenisRawatListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}