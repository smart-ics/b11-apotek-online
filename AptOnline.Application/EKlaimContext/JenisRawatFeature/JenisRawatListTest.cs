using AptOnline.Application.EKlaimContext.BayiLahirFeature;
using FluentAssertions;
using Xunit;

namespace AptOnline.Application.EKlaimContext.JenisRawatFeature;

public class JenisRawatListTest
{
    private readonly JenisRawatListHandler _sut = new();

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var actual = await _sut.Handle(new JenisRawatListQuery(), CancellationToken.None);
        actual.Count.Should().Be(3);
        actual.Should().Contain(new JenisRawatListResponse("1", "Rawat Inap"));
        actual.Should().Contain(new JenisRawatListResponse("2", "Rawat Jalan"));
        actual.Should().Contain(new JenisRawatListResponse("3", "Rawat Darurat"));
    }
}