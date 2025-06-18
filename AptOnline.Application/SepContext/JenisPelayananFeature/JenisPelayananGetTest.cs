using AptOnline.Application.EKlaimContext.BayiLahirFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Application.SepContext.JenisPelayananFeature;

public class JenisPelayananGetTest
{
    private readonly JenisPelayananGet _sut = new();

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var actual = await _sut.Handle(new JenisPelayananGetQuery("1"), CancellationToken.None);
        actual.Should().BeEquivalentTo(new JenisPelayananGetResponse("1", "Rawat Inap"));
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        var actual = () => _sut.Handle(new JenisPelayananGetQuery("3"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}