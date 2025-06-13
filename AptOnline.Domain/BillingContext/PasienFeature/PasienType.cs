using Ardalis.GuardClauses;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.BillingContext.PasienFeature;

public record PasienType : IPasienKey
{
    private PasienType(string pasienId, string pasienName, 
        DateTime birthDate, GenderType gender)
    {
        PasienId = pasienId;
        PasienName = pasienName;
        BirthDate = birthDate;
        Gender = gender;
    }

    public string PasienId { get; init; }
    public string PasienName { get; init; }
    public DateTime BirthDate { get; init; }
    public GenderType Gender { get; init; }


    public static PasienType Create(string pasienId, string pasienName,
        DateTime birthDate, GenderType gender)
    {
        Guard.Against.NullOrWhiteSpace(pasienId, nameof(pasienId));
        Guard.Against.NullOrWhiteSpace(pasienName, nameof(pasienName));
        Guard.Against.Null(gender, nameof(gender));

        return new PasienType(pasienId, pasienName, birthDate, gender);
    }
    public static PasienType Load(string pasienId, string pasienName,
        DateTime birthDate, GenderType gender) 
    => new PasienType(pasienId, pasienName, birthDate, gender);

    public static PasienType Default => new PasienType(
        "-", "-", new DateTime(3000,1,1), GenderType.Default);

    public static IPasienKey Key(string id) => 
        Default with { PasienId = id };
}

public class PasientTypeTest
{
    [Fact]
    public void UT1_GivenValidValue_WhenCreate_ThenOk()
    {
        var actual = PasienType.Create("A", "B", new DateTime(2025, 8, 5), GenderType.Default);
        actual.PasienId.Should().Be("A");
        actual.PasienName.Should().Be("B");
        actual.BirthDate.Should().Be(new DateTime(2025, 8, 5));
        actual.Gender.Should().Be(GenderType.Default);
    }

    [Fact]
    public void UT2_GivenEmptyPasienId_WhenCreate_ThenThrowEx()
    {
        Action act = () => PasienType.Create("", "B", new DateTime(2025, 8, 5), GenderType.Default);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UT3_GivenEmptyPasienName_WhenLoad_ThenOk()
    {
        var actual = PasienType.Load("A", "", new DateTime(2025, 8, 5), GenderType.Default);
        actual.PasienId.Should().Be("A");
        actual.PasienName.Should().Be("");
        actual.BirthDate.Should().Be(new DateTime(2025, 8, 5));
        actual.Gender.Should().Be(GenderType.Default);
    }

    [Fact]
    public void UT4_WhenDefault_ThenOk()
    {
        var actual = PasienType.Default;
        actual.PasienId.Should().Be("-");
        actual.PasienName.Should().Be("-");
        actual.BirthDate.Should().Be(new DateTime(3000, 1, 1));
        actual.Gender.Should().Be(GenderType.Default);
    }
}