using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Application.BillingContext.RoomChargeFeature;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.LayananDkFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.RoomChargeFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Infrastructure.BillingContext.RoomChargeFeature;

public class RoomChargeGetService : IRoomChargeGetService
{
    private readonly IDbConnection _conn;
    private readonly IRegGetService _regGetService;
    private readonly IListDataMayBe<RoomChargeBedDto, IRegKey> _roomChargeListDal;

    public RoomChargeGetService(IOptions<DatabaseOptions> opt, IRegGetService regGetService, 
        IListDataMayBe<RoomChargeBedDto, IRegKey> roomChargeListDal)
    {
        _regGetService = regGetService;
        _roomChargeListDal = roomChargeListDal;
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }

    public MayBe<RoomChargeModel> Execute(IRegKey reg)
    {
        var register = _regGetService.Execute(reg);
        if (register is null) 
            return MayBe<RoomChargeModel>.None;

        var listRoomCharge = _roomChargeListDal.ListData(reg);
        if (!listRoomCharge.HasValue) 
            return MayBe<RoomChargeModel>.None;

        var result = new RoomChargeModel(register.ToRefference());
        foreach (var item in listRoomCharge.Value)
        {
            var tgl = item.Tgl.ToDate(DateFormatEnum.YMD);
            var bed = new BedType(item.BedId, item.BedName);
            var kelasDk = new KelasDkType(item.KelasDkId, item.KelasDkName);
            var layanan = new LayananRefference(item.LayananId, item.LayananName);
            var layananDk = new LayananDkType(item.LayananDkId, item.LayananDkName);
            result.AddRoomCharge(tgl, bed, kelasDk, layanan, layananDk);
        }
        return MayBe.From(result);
    }

}