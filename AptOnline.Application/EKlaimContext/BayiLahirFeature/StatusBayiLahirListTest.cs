using FluentAssertions;
using Xunit;

namespace AptOnline.Application.EKlaimContext.BayiLahirFeature;

public class StatusBayiLahirListTest
{
    private readonly StatusBayiLahirListHandler _sut = new();

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var actual = await _sut.Handle(new StatusBayiLahirListQuery(), CancellationToken.None);
        actual.Count.Should().Be(2);
        actual.Should().Contain(new StatusBayiLahirListResponse("1", "Tanpa Kelainan"));
        actual.Should().Contain(new StatusBayiLahirListResponse("2", "Dengan Kelainan"));
    }
}