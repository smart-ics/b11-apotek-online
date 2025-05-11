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
        #region CLEAN-UP
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
        #endregion

        /*  THE-RULES:
        1. [jenisPelayanan] = "2"
	        1.1 [assesmentPelayanan] kosong, [tipePpk] = 1 ..... 	=> "gp" 		
	        1.2 [assesmentPelayanan] kosong, [tipePpk] = 2 ..... 	=> "hosp-trans" 
	        1.3 [noSkdp] isi.................................... 	=> "outp"		
	        1.4 [noSkdp] kosong, [assesmentPelayanan] isi.......	=> "mp"		
	        1.5 [noSkdp] isi, [ppkRujukan] = ppkRs...............	=> "inp"		

        2. [jenisPelayanan] = 1 (ranap)
	        2.1 [tipeLayananDk] =  2 ............... => "emd"		
	        2.2 [tipeLayananDk] <> 2 ............... => "outp" */

        var result = (jnsPlyn, assPlyn, tipePpk, noSkdp, ppkPerujuk, ppkRs, tipeLynDk) switch
        {
            ("2", "", "1", "", _, _, _)     => RUJUKAN_FKTP,        // 1.1
            ("2", "", "2", "", _, _, _)     => RUJUKAN_FKRTL,       // 1.2
            ("2", _, _, not "", var r, var rs, _) when r == rs && r != "" => DARI_RAWAT_INAP, // 1.5
            ("2", _, _, not "", _, _, _)    => DARI_RAWAT_JALAN,    // 1.3
            ("2", not "", _, "", _, _, _)   => RUJUKAN_SPESIALIS,   // 1.4
            ("1", _, _, _, _, _, "2")       => DARI_RAWAT_DARURAT,  // 2.1
            ("1", _, _, _, _, _, _)         => DARI_RAWAT_JALAN,    // 2.2
            _                               => DARI_RAWAT_JALAN
        };
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
    [InlineData("2", "", "1", "", "", "", "", "gp")]            // 1.1
    [InlineData("2", "", "2", "", "", "", "", "hosp-trans")]    // 1.2
    [InlineData("2", "", "1", "skdp1", "", "", "", "outp")]     // 1.3
    [InlineData("2", "ass", "", "", "", "", "", "mp")]          // 1.4
    [InlineData("2", "ass", "", "skdp1", "rspkl", "rspkl", "", "inp")]  // 1.5
    [InlineData("1", "", "", "", "", "", "2", "emd")]           // 2.1
    [InlineData("1", "", "", "", "", "", "1", "outp")]          // 2.2
    [InlineData("2", "ass", "", "skdp1", "", "", "", "outp")]   // fallback
    public void Resolve_ShouldReturnExpectedValue(
        string jnsPlyn, string assPlyn,
        string tipePpk, string noSkdp,
        string ppkPerujuk, string ppkRs,
        string tipeLayananDk, string expected)
    {
        // Act
        var result = CaraMasukValType.Resolve(jnsPlyn, assPlyn,
            tipePpk, noSkdp, ppkPerujuk, ppkRs, tipeLayananDk);

        // Assert
        result.Value.Should().Be(expected);
    }
}