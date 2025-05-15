using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.Helpers;
using AptOnline.Domain.SepContext.ReferensiFeature;
using GuardNet;

namespace AptOnline.Domain.BillingContext.RegAgg;

public record RegType : IRegKey, IPasienKey
{
    #region CONSTRUCTORS
    private RegType(string regId, 
        DateTime regDate, DateTime regOutDate,
        string pasienId, string pasienName,
        JenisRegEnum jenisReg,
        KelasRawatType kelasRawat)
    {
        RegId = regId;
        RegDate = regDate;
        RegOutDate = regOutDate;
        PasienId = pasienId;
        PasienName = pasienName;
        
        JenisReg = jenisReg;
        KelasRawat = kelasRawat;
    }
    public static RegType Create(
        string regId, DateTime regDate, DateTime regOutDate,
        string pasienId, string pasienName, JenisRegEnum jenisReg, KelasRawatType kelasRawat)
    {
        Guard.NotNullOrWhitespace(regId, nameof(regId));
        Guard.NotNullOrWhitespace(pasienId, nameof(pasienId));
        Guard.NotNullOrWhitespace(pasienName, nameof(pasienName));
        Guard.NotNull(kelasRawat, nameof(kelasRawat));

        return new RegType(regId, regDate, regOutDate, pasienId, pasienName, jenisReg, kelasRawat);
    }

    public static RegType Load(
        string regId, DateTime regDate, DateTime regOutDate,
        string pasienId, string pasienName, JenisRegEnum jenisReg,
        KelasRawatType kelasRawat)
        => new RegType(regId, regDate, regOutDate, 
            pasienId, pasienName, jenisReg, kelasRawat);

    public static RegType Default => new RegType(
        AppConst.DASH, AppConst.DEF_DATE, AppConst.DEF_DATE,
        AppConst.DASH, AppConst.DASH, JenisRegEnum.RawatJalan, KelasRawatType.Default);

    public static IRegKey Key(string regId)
        => new RegType(regId, AppConst.DEF_DATE, AppConst.DEF_DATE, "-", "-", 
            JenisRegEnum.RawatJalan, KelasRawatType.Default);
    #endregion

    #region PROPERTIES
    public string RegId { get; init; }
    public DateTime RegDate { get; init; }
    public DateTime RegOutDate { get; init; }
    public string PasienId { get; init; }
    public string PasienName { get; init; }
    public JenisRegEnum JenisReg { get; init; }
    public KelasRawatType KelasRawat { get; init; }
    #endregion
}

