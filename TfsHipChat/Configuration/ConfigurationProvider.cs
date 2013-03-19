using System.IO;
using Newtonsoft.Json;

namespace TfsHipChat.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationProvider(string path)
        {
            using (var reader = new JsonTextReader(new StreamReader(path)))
            {
                Config = (new JsonSerializer()).Deserialize<TfsHipChatConfig>(reader);
            }
        }

        public TfsHipChatConfig Config { get; private set; }
    }
}
