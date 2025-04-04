using AptOnline.Domain.Helpers;
using FluentAssertions;
using GuardNet;
using Xunit;

namespace AptOnline.Domain.PharmacyContext.DphoAgg;

public class DphoModel : IDphoKey
{
    public DphoModel(string id, string name, string prb, 
        string kronis, string kemo, decimal harga, 
        string restriksi, string generik, bool isAktif)
    {
        Guard.NotNullOrWhitespace(id, nameof(id));
        Guard.NotNullOrWhitespace(name, nameof(name));
        
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
    public static DphoModel Default 
        => new DphoModel(AppConst.DASH, AppConst.DASH, string.Empty, 
            string.Empty, string.Empty, 0, string.Empty, 
            string.Empty, false);
    public string DphoId { get; private set; }
    public string DphoName{ get; private set; }
    public string Prb { get; private set; }
    public string Kronis { get; private set; }
    public string Kemo { get; private set; }
    public decimal Harga { get; private set; }
    public string Restriksi { get; private set; }
    public string Generik { get; private set; }
    public bool IsAktif { get; private set; }
}

public record DphoRefference(string DphoId, string DphoName)
{
    public DphoRefference(DphoModel dpho) : this(dpho.DphoId, dpho.DphoName)
    {
    }
    public static DphoRefference Default => new(DphoModel.Default);
};


public class DphoRefferenceTest
{
    [Fact]
    public void UT1_CreateFromDphoModel_ShouldReturnValidDphoRefference()
    {
        var dpho = new DphoModel("A","B","C","D","E",1,"F","G",true);
        var act = new DphoRefference(dpho);
        act.DphoId.Should().Be("A");
        act.DphoName.Should().Be("B");
    }
}