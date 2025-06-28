using AptOnline.Application.EKlaimContext.JenisRawatFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Application.EKlaimContext.HemodialisaFeature;

public class DializerUsageGetTest
{
    private readonly DializerUsageGet _sut = new();

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var actual = await _sut.Handle(new DializerUsageGetQuery("1"), CancellationToken.None);
        actual.Should().BeEquivalentTo(new DializerUsageGetResponse("1", "Single Use"));
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        var actual = () => _sut.Handle(new DializerUsageGetQuery("4"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}