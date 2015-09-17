using System.Web.Mvc;

namespace Spike.PluginSpike.Api.InternalApi.ProcessB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
