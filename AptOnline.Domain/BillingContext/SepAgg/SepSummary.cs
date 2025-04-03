using System.Globalization;
using GuardNet;

namespace AptOnline.Domain.BillingContext.SepAgg;

public record SepSummary
{
    public SepSummary(string sepId, DateTime sepDate, string noPeserta, string dokterId, string dokterName)
    {
        Guard.NotNullOrWhitespace(sepId, nameof(sepId));
        Guard.NotNullOrWhitespace(noPeserta, nameof(noPeserta));
        Guard.NotNullOrWhitespace(dokterId, nameof(dokterId));
        Guard.NotNullOrWhitespace(dokterName, nameof(dokterName));

        SepId = sepId;
        SepDate = sepDate;
        NoPeserta = noPeserta;
        DokterId = dokterId;
        DokterName = dokterName;
    }

    public string SepId {get;}
    public DateTime SepDate {get;}
    public string NoPeserta {get;}
    public string DokterId {get;}
    public string DokterName {get;}

    public static SepSummary Default => new SepSummary("-", new DateTime(3000,1,1), "-", "-", "-");
}
