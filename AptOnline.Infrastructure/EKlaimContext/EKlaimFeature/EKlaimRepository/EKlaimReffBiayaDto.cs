using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.TarifRsFeature;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public class EKlaimReffBiayaDto
{
    public EKlaimReffBiayaDto()
    {
    }
    public string EKlaimId { get; set; }
    public int NoUrut { get; set; }
    public string TrsId { get; set; }
    public string ReffBiayaId { get; set; }
    public int ReffClass { get; set; }
    public string KetBiaya { get; set; }
    public decimal Nilai { get; set; }
    public string SkemaTarifJknId { get; set; }
    public string SkemaTarifJknName { get; set; }

    public TarifRsReffBiayaType ToModel()
    {
        var reffBiaya = new ReffBiayaType(ReffBiayaId, (JenisReffBiayaEnum)ReffClass);
        return new TarifRsReffBiayaType(NoUrut, TrsId, reffBiaya, KetBiaya, Nilai);
    }

    public static IEnumerable<EKlaimReffBiayaDto> Create(EKlaimModel model)
    {
        if (model.TarifRs.ListReffBiaya == null) 
            return new List<EKlaimReffBiayaDto>();

        var result = model.TarifRs.ListReffBiaya
            .Select(item => new EKlaimReffBiayaDto
            {
                EKlaimId = model.EKlaimId,
                NoUrut = item.NoUrut,
                TrsId = item.TrsId,
                ReffBiayaId = item.ReffBiaya.ReffBiayaId,
                ReffClass = (int)item.ReffBiaya.ReffClass,
                KetBiaya = item.KetBiaya,
                Nilai = item.Nilai,
                SkemaTarifJknId = item.SkemaJkn.SkemaTarifJknId,
                SkemaTarifJknName = item.SkemaJkn.SkemaTarifJknName
            }).ToList();
        return result;
    }
    
}