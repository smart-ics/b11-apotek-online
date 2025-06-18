using FluentAssertions;
using Xunit;

namespace AptOnline.Application.EKlaimContext.TbIndikatorFeature;

public class TbIndikatorGetTest
{
    private readonly TbIndikatorGet _sut = new();

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var actual = await _sut.Handle(new TbIndikatorGetQuery("1"), CancellationToken.None);
        actual.Should().BeEquivalentTo(new TbIndikatorGetResponse("1", "Ya"));
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        var actual = () => _sut.Handle(new TbIndikatorGetQuery("3"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}