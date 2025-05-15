using AptOnline.Domain.EKlaimContext;
using FluentAssertions;
using GuardNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AptOnline.Domain.BillingContext.PasienFeature;

public record PasienType : IPasienKey
{
    private PasienType(string pasienId, string pasienName, 
        DateTime birthDate, GenderValType gender)
    {
        PasienId = pasienId;
        PasienName = pasienName;
        BirthDate = birthDate;
        Gender = gender;
    }

    public static PasienType Create(string pasienId, string pasienName,
        DateTime birthDate, GenderValType gender)
    {
        Guard.NotNullOrWhitespace(pasienId, nameof(pasienId));
        Guard.NotNullOrWhitespace(pasienName, nameof(pasienName));
        Guard.NotNull(gender, nameof(birthDate));

        return new PasienType(pasienId, pasienName, birthDate, gender);
    }

    public static PasienType Load(string pasienId, string pasienName,
        DateTime birthDate, GenderValType gender) 
    => new PasienType(pasienId, pasienName, birthDate, gender);

    public static PasienType Default => new PasienType(
        "-", "-", new DateTime(3000,1,1), GenderValType.Default);

    public static IPasienKey Key(string id) => 
        Default with { PasienId = id };
    public string PasienId { get; init; }

    public string PasienName { get; init; }
    public DateTime BirthDate { get; init; }
    public GenderValType Gender { get; init; }
}

public class PasientTypeTest
{
    [Fact]
    public void UT1_GivenValidValue_WhenCreate_ThenOk()
    {
        var actual = PasienType.Create("A", "B", new DateTime(2025, 8, 5), GenderValType.Default);
        actual.PasienId.Should().Be("A");
        actual.PasienName.Should().Be("B");
        actual.BirthDate.Should().Be(new DateTime(2025, 8, 5));
        actual.Gender.Should().Be(GenderValType.Default);
    }

    [Fact]
    public void UT2_GivenEmptyPasienId_WhenCreate_ThenThrowEx()
    {
        Action act = () => PasienType.Create("", "B", new DateTime(2025, 8, 5), GenderValType.Default);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UT3_GivenEmptyPasienName_WhenLoad_ThenOk()
    {
        var actual = PasienType.Load("A", "", new DateTime(2025, 8, 5), GenderValType.Default);
        actual.PasienId.Should().Be("A");
        actual.PasienName.Should().Be("");
        actual.BirthDate.Should().Be(new DateTime(2025, 8, 5));
        actual.Gender.Should().Be(GenderValType.Default);
    }

    [Fact]
    public void UT4_WhenDefault_ThenOk()
    {
        var actual = PasienType.Default;
        actual.PasienId.Should().Be("-");
        actual.PasienName.Should().Be("-");
        actual.BirthDate.Should().Be(new DateTime(3000, 1, 1));
        actual.Gender.Should().Be(GenderValType.Default);
    }
}