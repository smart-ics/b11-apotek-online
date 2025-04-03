using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

namespace AptOnline.Infrastructure.AptolMidwareContext.ResepMidwareAgg;

internal record ResepMidwareDto(
    string ResepMidwareId,
    DateTime ResepMidwareDate,
    DateTime BridgeState,
    DateTime CreateTimestamp,
    DateTime SyncTimestamp,
    DateTime UploadTimestamp,
    string ChartId,
    string ResepRsId,
    string RegId,
    string PasienId,
    string PasienName,
    string SepId,
    DateTime SepDate,
    string NoPeserta,
    string FaskesId,
    string FaskesAsal,
    string PoliBpjsId,
    string PoliBpjsName,
    string JenisObatId,
    string DokterId,
    string DokterName,
    string ReffId,
    int Iterasi)
{
    public ResepMidwareModel ToModel()
    {
        // TODO: Mapping SepModel to Dto
        var result = new ResepMidwareModel
        {

        };
        return result;
    }
}