using AptOnline.Application.BillingContext.SepAgg;
using AptOnline.Domain.AptolCloudContext.FaskesAgg;
using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.SepAgg;
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
    private readonly ISepGetService _sepGetService;
    private ResepMidwareModel _agg;

    public ResepMidwareBuilder(IResepMidwareDal resepMidwareDal, 
        IResepMidwareItemDal resepMidwareItemDal, 
        ISepGetService sepGetService)
    {
        _resepMidwareDal = resepMidwareDal;
        _resepMidwareItemDal = resepMidwareItemDal;
        _sepGetService = sepGetService;
    }

    public ResepMidwareModel Build()
    {
        _agg.RemoveNull();
        return _agg;
    }

    public IResepMidwareBuilder Create(ResepRsModel resepRs)
    {
        throw new NotImplementedException();
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