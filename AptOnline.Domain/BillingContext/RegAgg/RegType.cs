using AptOnline.Domain.BillingContext.SepAgg;
using AptOnline.Domain.Helpers;
using GuardNet;

namespace AptOnline.Domain.BillingContext.RegAgg;

public record RegType : IRegKey
{
    #region CONSTRUCTORS
    private RegType(string regId, 
        DateTime regDate, DateTime regOutDate,
        string pasienId,  string pasienName,
        JenisRegEnum jenisReg)
    {
        RegId = regId;
        RegDate = regDate;
        RegOutDate = regOutDate;
        PasienId = pasienId;
        PasienName = pasienName;
        JenisReg = jenisReg;
    }
    public static RegType Create(
        string regId, DateTime regDate, DateTime regOutDate,
        string pasienId, string pasienName, JenisRegEnum jenisReg)
    {
        Guard.NotNullOrWhitespace(regId, nameof(regId));
        Guard.NotNullOrWhitespace(pasienId, nameof(pasienId));
        Guard.NotNullOrWhitespace(pasienName, nameof(pasienName));

        return new RegType(regId, regDate, regOutDate, pasienId, pasienName, jenisReg);
    }

    public static RegType Load(
        string regId, DateTime regDate, DateTime regOutDate,
        string pasienId, string pasienName, JenisRegEnum jenisReg)
        => new RegType(regId, regDate, regOutDate, pasienId, pasienName, jenisReg);

    public static RegType Default => new RegType(
        AppConst.DASH, AppConst.DEF_DATE, AppConst.DEF_DATE,
        AppConst.DASH, AppConst.DASH, JenisRegEnum.RawatJalan);

    public static IRegKey Key(string regId)
        => new RegType(regId, AppConst.DEF_DATE, AppConst.DEF_DATE, AppConst.DASH, AppConst.DASH, JenisRegEnum.RawatJalan);
    #endregion

    #region PROPERTIES
    public string RegId { get; init; }
    public DateTime RegDate { get; init; }
    public DateTime RegOutDate { get; init; }
    public string PasienId { get; init; }
    public string PasienName { get; init; }
    public JenisRegEnum JenisReg { get; init; }
    #endregion
}

