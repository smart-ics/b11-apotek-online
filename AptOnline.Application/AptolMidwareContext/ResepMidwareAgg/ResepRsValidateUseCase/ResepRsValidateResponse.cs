namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;

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