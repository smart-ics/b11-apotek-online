using AptOnline.Api.Infrastructures.Repos;
using AptOnline.Api.Models;

namespace AptOnline.Api.Helpers
{
    public static class LogHelper
    {
        private static IServiceProvider? _serviceProvider;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public static void Log(LogModel log)
        {
            try 
            {
                var _logDal = _serviceProvider.GetRequiredService<ILogDal>();
                _logDal.Insert(log);
            }
            catch { }
        }
    }
}
