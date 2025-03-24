using AptOnline.Application.AptolCloudContext.LogAgg;
using AptOnline.Infrastructure.AptolMidwareContext.LogAgg;
using Microsoft.Extensions.DependencyInjection;

namespace AptOnline.Infrastructure.Helpers;

public static class LogHelper
{
    private static IServiceProvider? _serviceProvider;
    public static void Initialize(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public static void Log(string resepId, string request, string response, string message )
    {
        // LogModel logModel = new(DateTime.Now, resepId, request, response,
        //     0, 0, message);
        // try 
        // {
        //     var _logDal = _serviceProvider.GetRequiredService<ILogDal>();
        //     _logDal.Insert(logModel);
        // }
        // catch { }
        throw new NotImplementedException();
    }
}