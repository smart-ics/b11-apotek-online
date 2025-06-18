using AptOnline.Application.EKlaimContext.BayiLahirFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Application.SepContext.TipeFaskesFeature;

public class TipeFaskesGetTest
{
    private readonly TipeFaskesGet _sut = new();

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var actual = await _sut.Handle(new TipeFaskesGetQuery("1"), CancellationToken.None);
        actual.Should().BeEquivalentTo(new TipeFaskesGetResponse("1", "Faskes-1"));
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        var actual = () => _sut.Handle(new TipeFaskesGetQuery("3"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}