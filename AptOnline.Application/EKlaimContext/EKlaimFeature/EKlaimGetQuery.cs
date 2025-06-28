using AptOnline.Domain.BillingContext.RegAgg;
using MediatR;

namespace AptOnline.Application.EKlaimContext.EKlaimFeature;

public record EKlaimGetQuery(string RegId) : IRequest<EKlaimGetResponse>;

public record EKlaimGetResponse(
    string EKlaimId, string EKlaimDate,
    string RegId, string PasienId, string PasienName,
    string SepNo, string SepDate, string PesertaBpjsNo,
    string Dpjp, string CaraMasuk, string KelasHak, 
    int UpgradeIndikator, decimal AddPaymentProcentage,
    string DischargeStatus, string Payor, string CoderNik,
    int Los);

public class EKlaimGetHandler : IRequestHandler<EKlaimGetQuery, EKlaimGetResponse>
{
    private readonly IEKlaimRepo _eKlaimRepo;

    public EKlaimGetHandler(IEKlaimRepo eKlaimRepo)
    {
        _eKlaimRepo = eKlaimRepo;
    }

    public Task<EKlaimGetResponse> Handle(EKlaimGetQuery request, CancellationToken cancellationToken)
    {
        var eklaim = _eKlaimRepo.LoadEntity(RegType.Key(request.RegId))
            .GetValueOrThrow($"eKlaim utk register '{request.RegId}' tidak ditemukan");
        
        var result = new EKlaimGetResponse(
            eklaim.EKlaimId, $"{eklaim.EKlaimDate:yyyy-MM-dd}",
            eklaim.Reg.RegId, eklaim.Pasien.PasienId, eklaim.Pasien.PasienName,
            eklaim.Sep.SepNo, $"{eklaim.Sep.SepDate:yyyy-MM-dd}", eklaim.PesertaBpjs.PesertaBpjsNo,
            eklaim.Dpjp.DokterId, eklaim.CaraMasuk.CaraMasukName, 
            eklaim.KelasJkn.KelasJknName, eklaim.UpgradeKelasIndikator.UpgradeIndikator, 
            eklaim.UpgradeKelasIndikator.AddPaymentProsentage,
            eklaim.DischargeStatus.DischargeStatusName, eklaim.Payor.Code, eklaim.Coder.Nik,
            eklaim.LengthOfStay);
        
        return Task.FromResult(result);
    }
}