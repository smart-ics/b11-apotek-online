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
}