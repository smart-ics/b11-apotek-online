using System.Globalization;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

public class SignaParser
{
    public class SignaType
    {
        public int DailyDose { get; set; }
        public decimal ConsumeAmount { get; set; }
        public string FormattedSigna => $"{DailyDose} dd {ConsumeAmount}";

        public override string ToString()
        {
            return $"Signa Result = {FormattedSigna}\nDailyDose = {DailyDose}\nConsumeAmount = {ConsumeAmount}";
        }
    }

    public static SignaType Parse(string prescriptionText)
    {
        var normalizedText = prescriptionText.ToLower();
        
        /*
        Cara kerja utama
        1. Tentukan Anchor Symbol: 'dd' atau 'x'
        2. Ambil angka sebelum anchor sebagai DailyDose (DD)
        3. Ambil angka setelah anchor sebagai ConsumeAmount (CA)
        4. Antara Anchor dengan DD atau CA boleh ada spasi atau langsung
          
        Beberapa kasus variasi
        1. CA bisa ditulis dengan fraction
           Conntoh: "2dd 1/2" => DD = 2, CA = 0.5
                    "2dd 1/3" => DD = 2, CA = 0.33     
        2. CA bisa ditulis dengan decimal koma atau titik
           Contoh: "2dd 0.5" => DD = 2, CA = 0.5
                   "2dd 0,5" => DD = 2, CA = 0.5
        3. Jika tidak disebutkan CA, maka default-nya adalah 1
           Contoh: "2dd" => DD = 2, CA = 1
        */

        normalizedText = Regex.Replace(normalizedText, @"(\d+)\s*x\s*", "$1 dd ");
        SignaType result;
        
        result = ParsingVariant1_Fraction(normalizedText);
        if (result != null) return result;

        result = ParsingVariant2_Decimal(normalizedText);
        if (result != null) return result;

        result = ParsingNormalCase(normalizedText);
        if (result != null) return result;

        result = ParsingVariant3_NoConsumeAmount(normalizedText);
        if (result != null) return result;
        
        // If no match found, throw an exception
        throw new FormatException($"Invalid signa: '{prescriptionText}'");
    }

    private static SignaType ParsingNormalCase(string text)
    {
        var wholeNumberSignaRegex = new Regex(@"(\d+)\s*dd\s*(\d+)");
        var match = wholeNumberSignaRegex.Match(text);

        if (!match.Success) return null;

        var dailyDose = int.Parse(match.Groups[1].Value);
        var consumeAmount = int.Parse(match.Groups[2].Value);

        return new SignaType
        {
            DailyDose = dailyDose,
            ConsumeAmount = consumeAmount
        };
    }
    
    private static SignaType ParsingVariant1_Fraction(string text)
    {
        var fractionSignaRegex = new Regex(@"(\d+)\s*dd\s*(\d+)/(\d+)");
        var match = fractionSignaRegex.Match(text);

        if (!match.Success) return null;
        
        var dailyDose = int.Parse(match.Groups[1].Value);
        var numerator = int.Parse(match.Groups[2].Value);
        var denominator = int.Parse(match.Groups[3].Value);
            
        var consumeAmount = (decimal)numerator / (decimal)denominator;
            
        return new SignaType
        {
            DailyDose = dailyDose,
            ConsumeAmount = consumeAmount
        };

    }
    
    private static SignaType ParsingVariant2_Decimal(string text)
    {
        var decimalSignaRegex = new Regex(@"(\d+)\s*dd\s*(\d+[.,]\d+)");
        var match = decimalSignaRegex.Match(text);

        if (!match.Success) return null;
        
        var dailyDose = int.Parse(match.Groups[1].Value);
            
        // Handle comma dan titik sbg decimal separators
        var consumeAmountStr = match.Groups[2].Value.Replace(',', '.');
        var consumeAmount = decimal.Parse(consumeAmountStr, CultureInfo.InvariantCulture);
            
        return new SignaType
        {
            DailyDose = dailyDose,
            ConsumeAmount = consumeAmount
        };

    }
    
    
    private static SignaType ParsingVariant3_NoConsumeAmount(string text)
    {
        var noConsumeSignaRegex = new Regex(@"(\d+)\s*dd(?!\s*\d)");
        var match = noConsumeSignaRegex.Match(text);

        if (!match.Success) return null;
        
        var dailyDose = int.Parse(match.Groups[1].Value);
            
        return new SignaType
        {
            DailyDose = dailyDose,
            ConsumeAmount = 1
        };
    }
}


public class SignaParserTest
{
    [Theory]
    [InlineData("2x1", 2, 1)]
    [InlineData("2dd1", 2, 1)]
    [InlineData("2 x 1", 2, 1)]
    [InlineData("2 dd 1", 2, 1)]
    [InlineData("2 dd 1/2", 2, 0.5)]
    [InlineData("2 dd 0.5", 2, 0.5)]
    [InlineData("2 dd 0,5", 2, 0.5)]
    [InlineData("2dd", 2, 1)]
    [InlineData("s3dd1 pc batuk",3, 1)]
    [InlineData("s 4dd4cc bila demam",4, 4)]
    public void GivenSigna_WhenParse_ThenShouldMatchCAandDD(string signa, int dd, decimal ca)
    {
        var result = SignaParser.Parse(signa);
        result.DailyDose.Should().Be(dd);
        result.ConsumeAmount.Should().Be(ca);
    }
}