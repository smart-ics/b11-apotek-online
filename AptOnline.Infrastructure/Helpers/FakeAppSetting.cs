using Microsoft.Extensions.Options;

namespace AptOnline.Infrastructure.Helpers;

public class FakeAppSetting
{
    public static IOptions<BillingOptions> GetBillingOptions()
    {
        var result = Options.Create(new BillingOptions
        {
            BaseApiUrl = "http://dev.smart-ics.com:8888/bipubapi",
            ConsId = "AptOnlineBridger",
            SecretKey = "Aptol123!"
        });
        return result;
    }
    
    public static IOptions<BpjsOptions> GetBpjsOptions()
    {
        var result = Options.Create(new BpjsOptions()
        {
            BaseApiUrl  = "https://apijkn-dev.bpjs-kesehatan.go.id/apotek-rest-dev",
            PpkId  = "0137A047",
            PpkName  = "IFRS Mekar Sari",
            ConsId  = "26537",
            SecretKey  = "4rEC33ECAB",
            UserKey  = "c30a9c170d85c269ecc6e6b511287ac5"
        });
        return result;
    }
    
}