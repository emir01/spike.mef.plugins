using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Xml.Linq;
using Spike.PluginSpike.PluginContract;
using Spike.PluginSpike.PluginContract.Contracts;

namespace Spike.PluginsSpike.InternalApiCaller.ApiA
{
    [Export(typeof(IPluginContract))]
    [ExportMetadata("PluginName", "ApiAPlugin")]
    public class PluginA : IPluginContract
    {
        public PluginExecutionResult ExecutePlugin(PluginExecutionEntry entry)
        {
            var url = GetInternalApiUrl();

            if (!string.IsNullOrWhiteSpace(url))
            {
                var apiResult = ExecuteApiCall(url);

                return new PluginExecutionResult() { Output = apiResult };
            }

            return new PluginExecutionResult()
            {
                Output = "Failed Configuration URL Processing"
            };
        }

        #region Plugin Execution

        /// <summary>
        /// Execute the call to the internal A Api.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string ExecuteApiCall(string url)
        {
            try
            {
                HttpClient client = new HttpClient { BaseAddress = new Uri(url) };
                
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("text/html"));
                
                // List data response.
                HttpResponseMessage response = client.GetAsync("").Result; // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var result = response.Content.ReadAsStringAsync();
                    return result.Result;
                }
                else
                {
                    return "Failed on Plugin Level - Request to Internal API A Failed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region Configuratoin

        private string GetInternalApiUrl()
        {
            XDocument doc = XDocument.Load(GetPluginDirectory()+ "/config.xml");

            var apiUrlConfigurationElement = doc.Root.Elements().FirstOrDefault(e => e.Name == "apiUrl");

            if (apiUrlConfigurationElement != null && apiUrlConfigurationElement.Attribute("value") != null)
            {
                return apiUrlConfigurationElement.Attribute("value").Value;
            }

            return "";
        }

        #endregion

        #region Plugin Path

        /// <summary>
        /// This must be here and not in any common code because we need to get the path where the plugin is stored.
        /// 
        /// There were file issues if it was not internal to the PluginA Class
        /// </summary>
        /// <returns></returns>
        public static string GetPluginDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        #endregion
    }
}
