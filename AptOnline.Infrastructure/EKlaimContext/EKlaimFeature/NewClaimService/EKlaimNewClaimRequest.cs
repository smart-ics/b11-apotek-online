using AptOnline.Domain.EKlaimContext.EKlaimFeature;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.NewClaimService;

public record EKlaimNewClaimRequest(EKlaimNewClaimRequestMeta metadata, EKlaimNewClaimRequestData data);
public record EKlaimNewClaimRequestMeta(string method);
public record EKlaimNewClaimRequestData(string nomor_kartu,string nomor_sep,
    string nomor_rm, string nama_pasien, string tgl_lahir, string gender)
{
    public EKlaimNewClaimRequestData(EKlaimModel req) :
        this(req.PesertaBpjs.PesertaBpjsNo, req.Sep.SepNo,
            req.Pasien.PasienId, req.Pasien.PasienName,
            $"{req.Pasien.BirthDate:yyyy-MM-dd HH:mm:ss}",
            req.Pasien.Gender.Value)
    {
        nomor_kartu = req.PesertaBpjs.PesertaBpjsNo;
        nomor_sep = req.Sep.SepNo;
        nomor_rm = req.Pasien.PasienId;
        nama_pasien = req.Pasien.PasienName;
        tgl_lahir = req.Pasien.BirthDate.ToString("yyyy-MM-dd HH:mm:ss");
        gender = req.Pasien.Gender.Value;
    }
}
