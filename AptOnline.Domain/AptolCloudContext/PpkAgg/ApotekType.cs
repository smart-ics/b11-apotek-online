using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.AptolCloudContext.PpkAgg;

public record ApotekType(string NamaPetugas, string NipPetugas, string NamaApoteker, string CheckStock)
{
    public static ApotekType Default => new ApotekType(AppConst.DASH, AppConst.DASH, AppConst.DASH, AppConst.DASH);
}