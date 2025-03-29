namespace AptOnline.Domain.AptolCloudContext.FaskesAgg;

public class FaskesModel : IFaskesKey
{
    public FaskesModel()
    {
    }

    public FaskesModel(string id, string name)
    {
        FaskesId = id;
        FaskesName = name;
    }
    public string FaskesId { get; set; }
    public string FaskesName { get; set; }
}