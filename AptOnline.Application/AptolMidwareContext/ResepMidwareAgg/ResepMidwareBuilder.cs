using AptOnline.Application.BillingContext.SepAgg;
using AptOnline.Domain.AptolCloudContext.FaskesAgg;
using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.EmrContext.ResepRsAgg;
using Nuna.Lib.CleanArchHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;

public interface IResepMidwareBuilder : INunaBuilder<ResepMidwareModel>
{
    IResepMidwareBuilder Create(ResepRsModel resepRs);
    IResepMidwareBuilder Load(IResepMidwareKey resep);
    IResepMidwareBuilder Faskes(IFaskesKey faskes);
    IResepMidwareBuilder PoliBpjs(IPoliBpjsKey poliBpjs);
    IResepMidwareBuilder JenisObat(string jenisObat);
    IResepMidwareBuilder Iterasi(int iterasi);
}
public class ResepMidwareBuilder : IResepMidwareBuilder
{
    private readonly IResepMidwareDal _resepMidwareDal;
    private readonly IResepMidwareItemDal _resepMidwareItemDal;
    private readonly ISepGetByRegService _sepGetService;
    private readonly ITglJamProvider _dateTime;
    private ResepMidwareModel _agg;

    public ResepMidwareBuilder(IResepMidwareDal resepMidwareDal, 
        IResepMidwareItemDal resepMidwareItemDal, 
        ISepGetByRegService sepGetService, 
        ITglJamProvider dateTime)
    {
        _resepMidwareDal = resepMidwareDal;
        _resepMidwareItemDal = resepMidwareItemDal;
        _sepGetService = sepGetService;
        _dateTime = dateTime;
    }

    public ResepMidwareModel Build()
    {
        _agg.RemoveNull();
        return _agg;
    }

    public IResepMidwareBuilder Create(ResepRsModel resepRs)
    {
        _agg = new ResepMidwareModel
        {
            ResepMidwareDate = resepRs.TglJam.ToDate(),
            BridgeState = "CREATED",
            CreateTimestamp = _dateTime.Now,
            SyncTimestamp = new DateTime(3000,1,1),
            UploadTimestamp = new DateTime(3000,1,1),
            
            ChartId = resepRs.
            PasienId = resepRs.PasienId,
        };
        // _agg.FaskesAsal = resepRs.FaskesAsal;
        // _agg.PoliBpjsId = resepRs.PoliBpjsId;
        // _agg.PoliBpjsName = resepRs.PoliBpjsName;
        // _agg.JenisObatId = resepRs.JenisObatId;
        // _agg.DokterId = resepRs.DokterId;
        // _agg.DokterName = resepRs.DokterName;
        // _agg.ReffId = resepRs.ReffId;
        return this;
    }

    public IResepMidwareBuilder Load(IResepMidwareKey resep)
    {
        throw new NotImplementedException();
    }

    public IResepMidwareBuilder Faskes(IFaskesKey faskes)
    {
        throw new NotImplementedException();
    }

    public IResepMidwareBuilder PoliBpjs(IPoliBpjsKey poliBpjs)
    {
        throw new NotImplementedException();
    }

    public IResepMidwareBuilder Dokter(IDokterKey dokter)
    {
        throw new NotImplementedException();
    }

    public IResepMidwareBuilder JenisObat(string jenisObat)
    {
        throw new NotImplementedException();
    }

    public IResepMidwareBuilder Iterasi(int iterasi)
    {
        throw new NotImplementedException();
    }
}