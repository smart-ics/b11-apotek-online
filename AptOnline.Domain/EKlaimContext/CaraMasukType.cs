using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record CaraMasukType : StringLookupValueObject<CaraMasukType>
{
    public CaraMasukType(string value) : base(value)
    {
    }

    protected override string[] ValidValues => new []
    {
        "gp",       // = Rujukan FKTP, 
        "hosp",     //-trans = Rujukan FKRTL,
        "mp",       // = Rujukan Spesialis, 
        "outp",     // = Dari Rawat Jalan,
        "inp",      // = Dari Rawat Inap, 
        "emd",      // = Dari Rawat Darurat,
        "born",     // = Lahir di RS, 
        "nursing",  // = Rujukan Panti Jompo,
        "psych",    // = Rujukan dari RS Jiwa, 
        "rehab",    // = Rujukan Fasilitas Rehab, 
        "other",    // = Lain-lain        
    }
}