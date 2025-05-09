using AptOnline.Domain.Helpers;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.EKlaimContext;

public record CaraMasukValType : StringLookupValueObject<CaraMasukValType>
{
    private const string RUJUKAN_FKTP = "gp";
    private const string RUJUKAN_FKRTL = "hosp-trans";
    private const string RUJUKAN_SPESIALIS = "mp";
    private const string DARI_RAWAT_JALAN = "outp";
    private const string DARI_RAWAT_INAP = "inp";
    private const string DARI_RAWAT_DARURAT = "emd";
    private const string LAHIR_DI_RS = "born";
    private const string RUJUKAN_PANTI_JOMPO = "nursing";
    private const string RUJUKAN_RS_JIWA = "psych";
    private const string RUJUKAN_FASILITAS_REHAB = "rehab";
    private const string LAIN_LAIN = "other";

    public CaraMasukValType() : base("")
    {
    }
    public CaraMasukValType(string value) : base(value)
    {
    }

    public static CaraMasukValType Resolve(
        string jnsPlyn, string assPlyn, 
        string tipePpk, string noSkdp, 
        string ppkPerujuk, string ppkRs,
        string tipeLynDk)
    {
        //  NORMALIZE
        jnsPlyn = jnsPlyn switch
        {
            "rawat jalan" => "2",
            "2" => "2",
            "1" => "1",
            _ => "1"
        };
        assPlyn = assPlyn.Trim() switch
        {
            "0" => string.Empty,
            _ => assPlyn,
        };
        tipePpk = tipePpk.Trim() switch
        {
            "1" =>  "1",
            "2" => "2",
            _ => "2"
        };
        noSkdp = noSkdp.Trim();

        //  BUILD
        var result =  (jnsPlyn, assPlyn, tipePpk, noSkdp, ppkPerujuk, ppkRs, tipeLynDk) switch
        {
            ("2", "", "1", "", _, _, _) => RUJUKAN_FKTP,
            ("2", "", "1", _, _, _, _) => DARI_RAWAT_JALAN,
            ("2", "", _, "", _, _, _) => RUJUKAN_FKRTL,
            ("2", "", _, _, _, _, _) => DARI_RAWAT_JALAN,
            ("2", _, _, "", _, _, _) => RUJUKAN_SPESIALIS,
            ("2", _, _, _, var x1, var x2, _) when x1 == x2 => DARI_RAWAT_INAP,
            ("2", _, _, _, _, _, _) => DARI_RAWAT_JALAN,
            (_, _, _, _, _, _, "2") => DARI_RAWAT_DARURAT,
            _ => DARI_RAWAT_JALAN
        };
        /*
        // tradisional-if-else 
        var result2 = string.Empty;
        if (jnsPlyn == "2")
        {
            if (assPlyn == string.Empty)
            {
                if (tipePpk == "1")
                {
                    if (noSkdp == string.Empty)
                        result2 = "gp";
                    else
                        result2 = "outp";
                }
                else
                {
                    if (noSkdp == string.Empty)
                        result2 = "hosp-trans";
                    else
                        result2 = "outp";
                }
            }
            else
            {
                if (noSkdp == string.Empty)
                {
                    result2 = "mp";
                }
                else
                {
                    if (ppkPerujuk == ppkRs)
                        result2 = "inp";
                    else
                        result2 = "outp";
                }
            }
        }
        else
        {
            if (tipeLynDk == "2")
                result2 = "emd";
            else
                result2 = "outp";
        }
        */
        return Create(result);
    }
    protected override string[] ValidValues => new[]
    {
        RUJUKAN_FKTP,    
        RUJUKAN_FKRTL, 
        RUJUKAN_SPESIALIS,    
        DARI_RAWAT_JALAN, 
        DARI_RAWAT_INAP,   
        DARI_RAWAT_DARURAT,  
        LAHIR_DI_RS,  
        RUJUKAN_PANTI_JOMPO, 
        RUJUKAN_RS_JIWA, 
        RUJUKAN_FASILITAS_REHAB, 
        LAIN_LAIN,       
    };
}

public class CaraMasukValTypeTest
{
    [Theory]
    [InlineData("2", "", "1", "", "", "", "", "gp")]
    [InlineData("2", "", "1", "x", "", "", "", "outp")]
    public void UT1_GivenValidArg_WhenResolve_ThenAsExpected(
        string jnsPlyn, string assPlyn, 
        string tipePpk, string noSkdp, 
        string ppkPerujuk,string ppkRs, 
        string tipeLynDk, string caraMasuk)
    {
        var expected = CaraMasukValType.Create(caraMasuk);
        var actual = CaraMasukValType.Resolve(jnsPlyn, assPlyn, tipePpk, noSkdp, ppkPerujuk, ppkRs, tipeLynDk);
        var exptectedValue = expected.Value;
        var actualValue = actual.Value;
        exptectedValue.Should().Be(actualValue);
    }
}