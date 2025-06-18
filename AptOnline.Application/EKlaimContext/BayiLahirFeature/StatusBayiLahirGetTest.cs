using AptOnline.Application.EKlaimContext.TbIndikatorFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Application.EKlaimContext.BayiLahirFeature;

public class StatusBayiLahirGetTest
{
    private readonly StatusBayiLahirGet _sut = new();

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var actual = await _sut.Handle(new StatusBayiLahirGetQuery("1"), CancellationToken.None);
        actual.Should().BeEquivalentTo(new StatusBayiLahirGetResponse("1", "Tanpa Kelainan"));
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        var actual = () => _sut.Handle(new StatusBayiLahirGetQuery("3"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}