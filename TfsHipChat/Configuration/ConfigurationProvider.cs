using System.IO;
using Newtonsoft.Json;

namespace TfsHipChat.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private TfsHipChatConfig _tfsHipChatConfig;

        public TfsHipChatConfig Config
        {
            get
            {
                if (_tfsHipChatConfig == null)
                {
                    using (var reader = new JsonTextReader(new StreamReader(Properties.Settings.Default.DefaultConfigPath)))
                    {
                        _tfsHipChatConfig = (new JsonSerializer()).Deserialize<TfsHipChatConfig>(reader);
                    }
                }

                return _tfsHipChatConfig;
            }
        }
    }
}
