using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg;

public class ResepBpjsDeleteResponse
{
    public ResepBpjsDeleteResponse(string resepRsId, string reffId,
        string deleteResult, string deleteNote)
    {
        ResepRsId = resepRsId;
        ReffId = reffId;
        DeleteResult = deleteResult;
        DeleteNote = deleteNote;
    }
    public string ResepRsId { get; set; }
    public string ReffId { get; set; }
    public string DeleteResult { get; set; }
    public string DeleteNote { get; set; }
}

//public record ResepBpjsDeleteResponseDto(
//    ResepMidwareModel ResepMidware,
//    bool IsCreated,
//    string Message);