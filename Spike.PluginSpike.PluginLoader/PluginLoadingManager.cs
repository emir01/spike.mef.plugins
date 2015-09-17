using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using Spike.PluginSpike.PluginContract;

namespace Spike.PluginSpike.PluginLoader
{
    public class PluginLoadingManager
    {
        private CompositionContainer _container;

        public PluginLoadingManager(PluginLoaderConfiguration pluginLoaderConfiguration)
        {
            var catalogue = CreatePluginCatalog(pluginLoaderConfiguration);

            CreateCompositionContainer(catalogue);
        }

        #region Build Step

        private void CreateCompositionContainer(AggregateCatalog catalogue)
        {
            _container = new CompositionContainer(catalogue);
        }

        private AggregateCatalog CreatePluginCatalog(PluginLoaderConfiguration pluginLoaderConfiguration)
        {
            var pluginsCatalog = new AggregateCatalog();

            foreach (var pluginDirectory in GetPluginDirectories(pluginLoaderConfiguration.PluginsRootLocation))
            {
                pluginsCatalog.Catalogs.Add(new DirectoryCatalog(pluginDirectory));
            }

            return pluginsCatalog;
        }

        /// <summary>
        /// Return a collection of plugin directories to be loaded
        /// </summary>
        /// <param name="baseDirectory"></param>
        /// <returns></returns>
        private string[] GetPluginDirectories(string baseDirectory)
        {
            var directories = Directory.EnumerateDirectories(baseDirectory, "*", SearchOption.TopDirectoryOnly);
            return directories.ToArray();
        }

        #endregion

        /// <summary>
        /// Retrieves a plugin for the given identifier using MEF and the Plugins Location object
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public IPluginContract GetPlugin(string identifier)
        {
            var pluginWrapper = _container
                .GetExports<IPluginContract, IPluginMetadata>()
                .FirstOrDefault(pl => pl.Metadata.PluginName == identifier);

            if (pluginWrapper != null)
            {
                return pluginWrapper.Value;
            }
            else
            {
                return null;
            }
        }
    }

    public class PluginLoaderConfiguration
    {
        public string PluginsRootLocation { get; set; }
    }
}
