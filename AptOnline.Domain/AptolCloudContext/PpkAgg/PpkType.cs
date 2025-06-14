﻿using AptOnline.Domain.Helpers;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.AptolCloudContext.PpkAgg;

public record PpkType
{
    public PpkType(string ppkId, string ppkName, string siup, string alamat, string kota, 
        KepalaType kepala, VerifikatorType verifikator, ApotekType apotek)
    {
        Guard.Against.NullOrWhiteSpace(ppkId, nameof(ppkId), "PPK ID harus terisi");
        Guard.Against.NullOrWhiteSpace(ppkName, nameof(ppkName), "PPK Name harus terisi");

        PpkId = ppkId;
        PpkName = ppkName;
        Siup = siup;
        Alamat = alamat;
        Kota = kota;
        Kepala = kepala;
        Verifikator = verifikator;
        Apotek = apotek;
    }

    public string PpkId { get; private set; }
    public string PpkName { get; private set; }
    public string Siup { get; private set; }
    public string Alamat { get; private set; }
    public string Kota { get; private set; }
    public KepalaType Kepala { get; private set; }
    public VerifikatorType Verifikator { get; private set; }
    public ApotekType Apotek { get; private set; }
        
    public static PpkType Default => new PpkType(
        AppConst.DASH, AppConst.DASH, AppConst.DASH, AppConst.DASH, AppConst.DASH, 
        KepalaType.Default, VerifikatorType.Default, ApotekType.Default);

    public PpkRefference ToRefference() => new PpkRefference(PpkId, PpkName);
}