using AptOnline.Domain.Helpers;
using FluentAssertions;
using Xunit;
using Ardalis.GuardClauses; 

namespace AptOnline.Domain.PharmacyContext.DphoAgg;

public class DphoType : IDphoKey
{
    public DphoType(string id, string name, string prb, 
        string kronis, string kemo, decimal harga, 
        string restriksi, string generik, bool isAktif)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        
        DphoId = id;
        DphoName = name;
        Prb = prb;
        Kronis = kronis;
        Kemo = kemo;
        Harga = harga;
        Restriksi = restriksi;
        Generik = generik;
        IsAktif = isAktif;
    }
    public string DphoId { get; private set; }
    public string DphoName{ get; private set; }
    public string Prb { get; private set; }
    public string Kronis { get; private set; }
    public string Kemo { get; private set; }
    public decimal Harga { get; private set; }
    public string Restriksi { get; private set; }
    public string Generik { get; private set; }
    public bool IsAktif { get; private set; }

    public static DphoType Default 
        => new DphoType(AppConst.DASH, AppConst.DASH, string.Empty, 
            string.Empty, string.Empty, 0, string.Empty, 
            string.Empty, false);

    public DphoRefference ToRefference()
        => new DphoRefference(DphoId, DphoName);
}

public record DphoRefference(string DphoId, string DphoName)
{
    public static DphoRefference Default => new(AppConst.DASH, AppConst.DASH);
};


public class DphoRefferenceTest
{
    [Fact]
    public void UT1_CreateFromDphoModel_ShouldReturnValidDphoRefference()
    {
        var dpho = new DphoType("A","B","C","D","E",1,"F","G",true);
        var act = dpho.ToRefference();
        act.DphoId.Should().Be("A");
        act.DphoName.Should().Be("B");
    }
}