namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public class EKlaimDto
{
    public string EKlaimId { get;set;} 
    public DateTime EKlaimDate { get;set;} 
    public string SepNo { get;set;} 
    public string KartuBpjsNo { get;set;} 
    public string RegId { get;set;} 
    public DateTime  RegDate { get;set;} 
    public string PasienId { get;set;} 
    public string PasienName { get;set;} 
    public DateTime BirthDate { get;set;} 
    public string Gender { get;set;} 
    public string DpjpId { get;set;} 
    public string DpjpName { get;set;} 
    public string CaraMasukId { get;set;} 
    public string CaraMasukName { get;set;} 
    public string KelasJknId { get;set;} 
    public string KelasJknName { get;set;} 
    public int KelasJknValue { get;set;} 
    public string KelasTarifRsId { get;set;} 
    public decimal TarifPoliEksekutif { get;set;} 
    public int UpgradeIndikator { get;set;} 
    public decimal AddPaymentProcentage { get;set;} 
    public string DischargeStatusId { get;set;}  
    public string DischargeStatusName { get;set;} 
    public string PayorId { get;set;} 
    public string PayorName { get;set;} 
    public string CoderPegId { get;set;} 
    public string CoderPegName { get;set;} 
    public string CoderNik { get;set;} 
    public int Los { get;set;} 
}