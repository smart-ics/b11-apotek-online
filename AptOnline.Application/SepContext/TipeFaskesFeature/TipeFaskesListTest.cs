using AptOnline.Application.EKlaimContext.BayiLahirFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Application.SepContext.TipeFaskesFeature;

public class TipeFaskesListTest
{
    private readonly TipeFaskesListHandler _sut = new();

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var actual = await _sut.Handle(new TipeFaskesListQuery(), CancellationToken.None);
        actual.Count.Should().Be(2);
        actual.Should().Contain(new TipeFaskesListResponse("1", "Faskes-1"));
        actual.Should().Contain(new TipeFaskesListResponse("2", "Faskes RS"));
    }
}