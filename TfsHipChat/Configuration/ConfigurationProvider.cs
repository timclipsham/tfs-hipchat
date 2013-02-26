using System.IO;
using Newtonsoft.Json;
using TfsHipChat.Properties;

namespace TfsHipChat.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationProvider()
        {
            using (var reader = new JsonTextReader(new StreamReader(Settings.Default.DefaultConfigPath)))
            {
                Config = (new JsonSerializer()).Deserialize<TfsHipChatConfig>(reader);
            }
        }

        public TfsHipChatConfig Config { get; private set; }
    }
}
