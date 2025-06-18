using AptOnline.Application.EKlaimContext.BayiLahirFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Application.EKlaimContext.JenisRawatFeature;

public class JenisRawatGetTest
{
    private readonly JenisRawatGet _sut = new();

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var actual = await _sut.Handle(new JenisRawatGetQuery("1"), CancellationToken.None);
        actual.Should().BeEquivalentTo(new JenisRawatGetResponse("1", "Rawat Inap"));
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        var actual = () => _sut.Handle(new JenisRawatGetQuery("4"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}