using System;
using System.IO;
using System.Reflection;

namespace Spike.PluginSpike.PluginContract.Utilities
{
    public static class ConfigurationUtilities
    {
        public static string GetPluginDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
