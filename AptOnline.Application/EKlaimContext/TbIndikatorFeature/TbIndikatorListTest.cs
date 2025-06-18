using AptOnline.Domain.EKlaimContext.TbIndikatorFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.TbIndikatorFeature;

public class TbIndikatorListTest
{
    private readonly TbIndikatorListHandler _sut = new();


    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var actual = await _sut.Handle(new TbIndikatorListQuery(), CancellationToken.None);
        actual.Count.Should().Be(2);
        actual.Should().Contain(new TbIndikatorListResponse("1", "Ya"));
        actual.Should().Contain(new TbIndikatorListResponse("0", "Tidak"));
    }
}