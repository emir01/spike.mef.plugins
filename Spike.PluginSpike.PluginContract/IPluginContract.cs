using Spike.PluginSpike.PluginContract.Contracts;

namespace Spike.PluginSpike.PluginContract
{
    public interface IPluginContract
    {
        PluginExecutionResult ExecutePlugin(PluginExecutionEntry entry);
    }
}
