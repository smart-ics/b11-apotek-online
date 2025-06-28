using AptOnline.Application.EKlaimContext.JenisRawatFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Application.EKlaimContext.HemodialisaFeature;

public class DializerUsageListTest
{
    private readonly DializerUsageListHandler _sut = new();

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var actual = await _sut.Handle(new DializerUsageListQuery(), CancellationToken.None);
        actual.Count.Should().Be(2);
        actual.Should().Contain(new DializerUsageListResponse("1", "Single Use"));
        actual.Should().Contain(new DializerUsageListResponse("2", "Multiple Use"));
    }
}