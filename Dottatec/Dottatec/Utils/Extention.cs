using Microsoft.AppCenter.Analytics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Dottatec.Utils
{
    public static class Extention
    {
        public static void Report(this System.Exception ex, [CallerMemberName]string caller = "")
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            Analytics.TrackEvent($"{ex.GetType().Name} ({caller})",
                      new Dictionary<string, string>()
                      {
                          {"menssagem", ex.Message}
                      });
        }
    }
}
