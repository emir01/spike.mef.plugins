using System.Web.Http;
using Spike.PluginSpike.PluginLoader;

namespace Spike.PluginSpike.Api.PublicApi.Controllers
{
    public class PublicController : ApiController
    {
        private PluginLoadingManager _pluginManager;
        
        private readonly string PluginRootLocation = "C:/external_plugins";

        public PublicController()
        {
            _pluginManager = new PluginLoadingManager(new PluginLoaderConfiguration
            {
                PluginsRootLocation = PluginRootLocation
            });
        }

        public string Get(string requestIdentifier)
        {
            // create a plugin manager to decide which internal system is going to handle the request
            var pluginIdentifier = GetPluginIdentifier(requestIdentifier);

            // decide which plugin is going to handle the request
            var processPlugin = _pluginManager.GetPlugin(pluginIdentifier);

            // this is going to execute a call to an internal api and return the results? 
            // todo: think about async!!!!!
            var result = processPlugin.ExecutePlugin(null);

            return result.Output;
        }

        private string GetPluginIdentifier(string requestIdentifier)
        {
            return requestIdentifier;
        }
    }
}
