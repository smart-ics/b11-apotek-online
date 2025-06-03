using FluentAssertions;
using Xunit;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext;

public record CaraMasukType 
{
    private static readonly HashSet<string> ValidValues = new()
    {
        "gp", "hosp-trans", "mp", "outp",
        "inp", "emd", "born", "nursing",
        "psych", "rehab", "other",
    };
        
    private CaraMasukType(string value) => Value = value;

    public string Value { get; private set; }

    public static CaraMasukType Create(string value)
    {
        /*  Code Translation:
                - gp = Rawat Jalan (Rujukan FKTP)
                - hosp-trans = Rawat Jalan (Rujukan FKRTL)
                - mp = Rawat Jalan (Rujukan Spesialis)
                - outp = Rawat Jalan (Dari Rawat Jalan)
                - inp = Rawat Inap (Dari Rawat Inap)
                - emd = Rawat Inap (Dari Rawat Darurat)
                - born = Bayi Lahir Di RS
                - nursing = Rawat Jalan (Rujukan Panti Jompo),
                
            psych = Rawat Jalan (Rujukan RS Jiwa)
            rehab = Rawat Jalan (Rujukan Fasilitas Rehab)
            other = Lain-lain */
        var validValues = new[]
        {
            "gp", "hosp-trans", "mp", "outp",
            "inp", "emd", "born", "nursing",
            "psych", "rehab", "other"
        };
        Guard.Against.InvalidInput<string>(value, nameof(value), x =>!validValues.Contains(x), 
            "Invalid Cara Masuk");
        var result = new CaraMasukType(value); 
        return result;  
    }

    public static CaraMasukType Load(string value) => new(value);

    public static CaraMasukType Resolve(
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
            ("2", "", "1", "", _, _, _)     => "gp",    // RUJUKAN_FKTP,        // 1.1
            ("2", "", "2", "", _, _, _)     => "hosp-trans", // RUJUKAN_FKRTL,  // 1.2
            ("2", _, _, not "", var r, var rs, _) 
                when r == rs && r != ""                                 => "inp",  // DARI_RAWAT_DARURAT,   // 1.5
            ("2", _, _, not "", _, _, _)                  => "outp", // DARI_RAWAT_JALAN,     // 1.3
            ("2", not "", _, "", _, _, _)          => "mp",   // RUJUKAN_SPESIALIS,    // 1.4
            ("1", _, _, _, _, _, "2")                   => "emd",  // DARI_RAWAT_DARURAT,   // 2.1
            ("1", _, _, _, _, _, _)                              => "outp", // DARI_RAWAT_JALAN,     // 2.2
            _                                                           => "outp"  // DARI_RAWAT_JALAN
        };
        return Create(result);
    }

    public static CaraMasukType Default => new CaraMasukType(string.Empty);
    
}

public class CaraMasukTypeTest
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
        var result = CaraMasukType.Resolve(jnsPlyn, assPlyn,
            tipePpk, noSkdp, ppkPerujuk, ppkRs, tipeLayananDk);

        // Assert
        result.Value.Should().Be(expected);
    }
}