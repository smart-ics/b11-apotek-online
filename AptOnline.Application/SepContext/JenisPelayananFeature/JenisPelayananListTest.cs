using AptOnline.Application.EKlaimContext.BayiLahirFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Application.SepContext.JenisPelayananFeature;

public class JenisPelayananListTest
{
    private readonly JenisPelayananListHandler _sut = new();

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var actual = await _sut.Handle(new JenisPelayananListQuery(), CancellationToken.None);
        actual.Count.Should().Be(2);
        actual.Should().Contain(new JenisPelayananListResponse("1", "Rawat Inap"));
        actual.Should().Contain(new JenisPelayananListResponse("2", "Rawat Jalan"));
    }
}