using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.PharmacyContext.BrgAgg;
using MediatR;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsValidateUseCase;

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








