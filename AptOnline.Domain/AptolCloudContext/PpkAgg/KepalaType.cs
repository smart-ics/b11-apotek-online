using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.AptolCloudContext.PpkAgg;

public record KepalaType(string Nama, string Jabatan, string Nip)
{
    public static KepalaType Default => new KepalaType(AppConst.DASH, AppConst.DASH, AppConst.DASH);
};