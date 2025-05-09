using AptOnline.Domain.BillingContext.RegAgg;

namespace AptOnline.Infrastructure.BillingContext.RegAgg;

public class RegGetDto
{
    public string? RegId {get;set;}
    public string? RegDate {get;set;}
    public string? RegTime {get;set;}
    public string? RegOutDate {get;set;}
    public string? RegOutTime {get;set;}
    public string? RegJenis {get;set;}
    public string? PasienId {get;set;}
    public string? PasienName {get;set;}

    public RegType ToModel()
    {
        var jenisReg = RegJenis switch
        {
            "0" => JenisRegEnum.RawatJalan,
            "1" => JenisRegEnum.RawatInap,
            "2" => JenisRegEnum.External,
            "3" => JenisRegEnum.RawatDarurat,
            "4" => JenisRegEnum.Meninggal,
            "5" => JenisRegEnum.ExternalInap,
            _ => JenisRegEnum.RawatJalan
        };
        return RegType.Load(
            RegId ?? string.Empty,
            DateTime.Parse($"{RegDate}"),
            DateTime.Parse($"{RegOutDate}"),
            PasienId ?? string.Empty,
            PasienName ?? string.Empty,
            jenisReg);
    }
}