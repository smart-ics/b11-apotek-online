
namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg;

public class ObatBpjsDeleteResponse
{
    public ObatBpjsDeleteResponse(string resepRsId, string barangId,
        string deleteResult, string deleteNote)
    {
        ResepRsId = resepRsId;
        BarangId = barangId;
        DeleteResult = deleteResult;
        DeleteNote = deleteNote;
    }
    public string ResepRsId { get; set; }
    public string BarangId { get; set; }
    public string DeleteResult { get; set; }
    public string DeleteNote { get; set; }
}
