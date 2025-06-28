
namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg;

public class ResepBpjsSaveResponse
{
    public ResepBpjsSaveResponse(string resepMidwareId, string resepRsId, string reffId,
        string saveResult, string saveNote)
    {
        ResepMidwareId = resepMidwareId;
        ReffId = reffId;
        SaveResult = saveResult;
        SaveNote = saveNote;
        ResepRsId = resepRsId;
    }
    public string ResepMidwareId { get; set; }
    public string ResepRsId { get; set; }
    public string ReffId { get; set; }
    public string SaveResult { get; set; }
    public string SaveNote { get; set; }
}
