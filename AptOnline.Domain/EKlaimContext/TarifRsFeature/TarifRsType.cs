using AptOnline.Domain.BillingContext.TrsBillingFeature;
using AptOnline.Domain.EKlaimContext.SkemaTarifFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record TarifRsType
{
    public TarifRsType(TarifRsSkemaType procedureBedah, TarifRsSkemaType procedureNonBedah, 
        TarifRsSkemaType konsultasi, TarifRsSkemaType tenagaAhli,TarifRsSkemaType keperawatan, 
        TarifRsSkemaType penunjang, TarifRsSkemaType radiologi, TarifRsSkemaType laboratorium, 
        TarifRsSkemaType pelayananDarah, TarifRsSkemaType rehabilitasi, 
        TarifRsSkemaType kamar, TarifRsSkemaType rawatIntensif, 
        TarifRsSkemaType obat, TarifRsSkemaType alkes, TarifRsSkemaType bmhp, 
        TarifRsSkemaType sewaAlat, TarifRsSkemaType obatKronis, TarifRsSkemaType obatKemoterapi)
    {
        Guard.Against.InvalidSkema(procedureBedah.SkemaTarif, SkemaTarifType.ProsedurBedah);
        Guard.Against.InvalidSkema(procedureNonBedah.SkemaTarif, SkemaTarifType.ProsedurNonBedah);
        Guard.Against.InvalidSkema(konsultasi.SkemaTarif, SkemaTarifType.Konsultasi);
        Guard.Against.InvalidSkema(tenagaAhli.SkemaTarif, SkemaTarifType.TenagaAhli);
        Guard.Against.InvalidSkema(keperawatan.SkemaTarif, SkemaTarifType.Keperawatan);
        Guard.Against.InvalidSkema(penunjang.SkemaTarif, SkemaTarifType.Penunjang);
        Guard.Against.InvalidSkema(radiologi.SkemaTarif, SkemaTarifType.Radiologi);
        Guard.Against.InvalidSkema(laboratorium.SkemaTarif, SkemaTarifType.Laboratorium);
        Guard.Against.InvalidSkema(pelayananDarah.SkemaTarif, SkemaTarifType.PelayananDarah);
        Guard.Against.InvalidSkema(rehabilitasi.SkemaTarif, SkemaTarifType.Rehabilitasi);
        Guard.Against.InvalidSkema(kamar.SkemaTarif, SkemaTarifType.Kamar);
        Guard.Against.InvalidSkema(rawatIntensif.SkemaTarif, SkemaTarifType.RawatIntensif);
        Guard.Against.InvalidSkema(obat.SkemaTarif, SkemaTarifType.Obat);
        Guard.Against.InvalidSkema(alkes.SkemaTarif, SkemaTarifType.Alkes);
        Guard.Against.InvalidSkema(bmhp.SkemaTarif, SkemaTarifType.Bmhp);        
        Guard.Against.InvalidSkema(sewaAlat.SkemaTarif, SkemaTarifType.SewaAlat);
        Guard.Against.InvalidSkema(obatKronis.SkemaTarif, SkemaTarifType.ObatKronis);
        Guard.Against.InvalidSkema(obatKemoterapi.SkemaTarif, SkemaTarifType.ObatKemoterapi);

        ProcedureBedah = procedureBedah;
        ProcedureNonBedah = procedureNonBedah;
        Konsultasi = konsultasi;
        TenagaAhli = tenagaAhli;
        Keperawatan = keperawatan;
        Penunjang = penunjang;
        Radiologi = radiologi;
        Laboratorium = laboratorium;
        PelayananDarah = pelayananDarah;
        Rehabilitasi = rehabilitasi;
        Kamar = kamar;
        RawatIntensif = rawatIntensif;
        Obat = obat;
        Alkes = alkes;
        Bmhp = bmhp;
        SewaAlat = sewaAlat;
        ObatKronis = obatKronis;
        ObatKemoterapi = obatKemoterapi;
    }
    
    //      PROCEDURE
    public TarifRsSkemaType ProcedureBedah { get; init; }
    public TarifRsSkemaType ProcedureNonBedah { get; init; }
    //      JASA MEDIS
    public TarifRsSkemaType Konsultasi { get; init; }
    public TarifRsSkemaType TenagaAhli { get; init; }
    public TarifRsSkemaType Keperawatan { get; init; }
    //      PENUNJANG
    public TarifRsSkemaType Penunjang { get; init; }
    public TarifRsSkemaType Radiologi { get; init; }
    public TarifRsSkemaType Laboratorium { get; init; }
    public TarifRsSkemaType PelayananDarah { get; init; }
    public TarifRsSkemaType Rehabilitasi { get; init; }
    //      AKOMODASI
    public TarifRsSkemaType Kamar { get; init; }
    public TarifRsSkemaType RawatIntensif { get; init; }
    //      OBAT
    public TarifRsSkemaType Obat { get; init; }
    public TarifRsSkemaType Alkes { get; init; }
    public TarifRsSkemaType Bmhp { get; init; }
    public TarifRsSkemaType SewaAlat { get; init; }
    public TarifRsSkemaType ObatKronis { get; init; }
    public TarifRsSkemaType ObatKemoterapi { get; init; }
    
    
    public static TarifRsType Create(TrsBillingModel trsBilling)
    {
        var allSkema = SkemaTarifType.ListData().ToList();
        var allSkemaRs = allSkema.Select(x => new TarifRsSkemaType(x,0)).ToList(); 
        foreach (var skema in allSkema)
        {
            
        }
        
    }
}

public static class SkemaTarifGuardExtensions
{
    public static void InvalidSkema(
        this IGuardClause guard,
        SkemaTarifType actual,
        SkemaTarifType expected)
    {
        Guard.Against.Null(actual, nameof(actual));
        Guard.Against.Null(expected, nameof(expected));

        if (actual.SkemaTarifId != expected.SkemaTarifId)
        {
            throw new ArgumentException(
                $"Expected skema tarif ID {expected.SkemaTarifId} but got {actual.SkemaTarifId}");
        }
    }
}