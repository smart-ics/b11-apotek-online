using AptOnline.Application.AptolCloudContext.FaskesAgg;
using AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsValidateUseCase;
using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Application.BillingContext.SepAgg;
using AptOnline.Application.PharmacyContext.MapDphoAgg;
using AptOnline.Domain.AptolCloudContext.FaskesAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using MediatR;
using Nuna.Lib.PatternHelper;
using Nuna.Lib.TransactionHelper;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;

#region COMMAND
public record ResepRsValidateCommand(
    string RegId, string LayananId, 
    List<ResepRsValidateCommandResep> ListResep) 
    : IRequest<IEnumerable<ResepRsValidateResponse>>, IRegKey, ILayananKey;

public record ResepRsValidateCommandResep(
    string Description, 
    List<ResepRsValidateCommandObat> ListItem);

public record ResepRsValidateCommandObat(
    string BrgId, string BrgName, int Qty,
    string Signa, string CaraPakai, int Iter,
    List<ResepRsValidateCommandKomponenRacik> ListKomponenRacik) : IBrgKey;

public record ResepRsValidateCommandKomponenRacik(
    string BrgId, string BrgName, int Qty,
    string Signa, string CaraPakai) : IBrgKey;
#endregion








