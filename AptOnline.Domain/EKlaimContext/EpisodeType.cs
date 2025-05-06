using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record EpisodeType : StringLookupValueObject<EpisodeType>
{
    public EpisodeType(string value) : base(value)
    {
    }

    protected override string[] ValidValues => new[]
    {
        "1", // ICU dengan ventilator
        "2", //ICU tanpa ventilator
        "3", //Isolasi tekanan negatif dengan ventilator
        "4", //Isolasi tekanan negatif tanpa ventilator
        "5", //Isolasi non tekanan negatif dengan ventilator
        "6", //Isolasi non tekanan negatif tanpa ventilator
        "7", //ICU tekanan negatif dengan ventilator
        "8", //ICU tekanan negatif tanpa ventilator
        "9", //ICU tanpa tekanan negatif dengan ventilator
        "10", //ICU tanpa tekanan negatif tanpa ventilator
        "11", //Isolasi tekanan negatif
        "12", //Isolasi tanpa tekanan negatif
    };
}