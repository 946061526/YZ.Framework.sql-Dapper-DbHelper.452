using System.Configuration;

namespace YZ.Framework.Core
{
    public class PluginConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public PluginConfigurationCollection PluginCollection
        {
            get
            {
                return (PluginConfigurationCollection)base[""];
            }
        }
    }
}

