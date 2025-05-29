using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.Helpers;
using AptOnline.Domain.SepContext.ReferensiFeature;
using GuardNet;

namespace AptOnline.Domain.BillingContext.RegAgg;

public record RegType : IRegKey
{
    #region CONSTRUCTORS
    private RegType(string regId, 
        DateTime regDate, DateTime regOutDate,
        PasienType pasien,
        JenisRegEnum jenisReg,
        KelasRawatType kelasRawat)
    {
        RegId = regId;
        RegDate = regDate;
        RegOutDate = regOutDate;
        Pasien = pasien;
        JenisReg = jenisReg;
        KelasRawat = kelasRawat;
    }
    public static RegType Create(
        string regId, DateTime regDate, DateTime regOutDate,
        PasienType pasien, JenisRegEnum jenisReg, KelasRawatType kelasRawat)
    {
        Guard.NotNullOrWhitespace(regId, nameof(regId));
        Guard.NotNull(pasien, nameof(pasien));
        Guard.NotNull(kelasRawat, nameof(kelasRawat));

        return new RegType(regId, regDate, regOutDate, pasien, jenisReg, kelasRawat);
    }

    public static RegType Load(
        string regId, DateTime regDate, DateTime regOutDate,
        PasienType pasien, JenisRegEnum jenisReg,
        KelasRawatType kelasRawat)
        => new RegType(regId, regDate, regOutDate, 
            pasien, jenisReg, kelasRawat);

    public static RegType Default => new RegType(
        AppConst.DASH, AppConst.DEF_DATE, AppConst.DEF_DATE, PasienType.Default,
        JenisRegEnum.RawatJalan, KelasRawatType.Default);

    public static IRegKey Key(string regId)
        => new RegType(regId, AppConst.DEF_DATE, AppConst.DEF_DATE, PasienType.Default,
            JenisRegEnum.RawatJalan, KelasRawatType.Default);
    #endregion

    #region PROPERTIES
    public string RegId { get; init; }
    public DateTime RegDate { get; init; }
    public DateTime RegOutDate { get; init; }
    public PasienType Pasien { get; init; }

    public JenisRegEnum JenisReg { get; init; }
    public KelasRawatType KelasRawat { get; init; }
    #endregion
}

