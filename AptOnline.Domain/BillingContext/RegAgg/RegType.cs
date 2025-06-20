using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.Helpers;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.BillingContext.RegAgg;

public record RegType : IRegKey
{
    #region CONSTRUCTORS

    public RegType(string regId, DateTime regDate, DateTime regOutDate,
        PasienType pasien, JenisRegEnum jenisReg, KelasRawatType kelasRawat,
        LayananRefference layanan)
    {
        Guard.Against.Null(pasien, nameof(pasien));
        Guard.Against.Null(kelasRawat, nameof(kelasRawat));
        Guard.Against.Null(layanan, nameof(layanan));

        RegId = regId;
        RegDate = regDate;
        RegOutDate = regOutDate;
        Pasien = pasien;
        JenisReg = jenisReg;
        KelasRawat = kelasRawat;
        Layanan = layanan;
    }
    public static RegType Default 
        => new RegType("-", AppConst.DEF_DATE, AppConst.DEF_DATE, 
            PasienType.Default, JenisRegEnum.RawatJalan, KelasRawatType.Default, 
            LayananType.Default.ToRefference());

    public static IRegKey Key(string regId)
        => new RegType(regId, AppConst.DEF_DATE, AppConst.DEF_DATE, 
            PasienType.Default, JenisRegEnum.RawatJalan, KelasRawatType.Default,
            LayananType.Default.ToRefference());
    #endregion

    #region PROPERTIES
    public string RegId { get; init; }
    public DateTime RegDate { get; init; }
    public DateTime RegOutDate { get; init; }
    public PasienType Pasien { get; init; }
    public JenisRegEnum JenisReg { get; init; }
    public KelasRawatType KelasRawat { get; init; }
    public LayananRefference Layanan { get; init; }
    #endregion
    
    public RegRefference ToRefference() => new RegRefference(RegId, RegDate); 
}

public record RegRefference(string RegId, DateTime RegDate);

