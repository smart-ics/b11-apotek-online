using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsValidateUseCase;

public class ResepRsValidateResponse
{
    public ResepRsValidateResponse(int noUrut, string resepMidwareId, 
        string validationResult, string validationNote)
    {
        NoUrut = noUrut;
        ResepMidwareId = resepMidwareId;
        ValidationResult = validationResult;
        ValidationNote = validationNote;
    }
    public int NoUrut{get;set;}
    public string ResepMidwareId{get;set;}
    public string ValidationResult{get;set;}
    public string ValidationNote{get;set;}
}

public record ResepRsValidateResponseDto(
    int NoUrut,
    ResepMidwareModel ResepMidware,
    bool IsCreated,
    string ValidationNote);