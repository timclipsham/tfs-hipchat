using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TfsHipChat.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private TfsHipChatConfig _config;

        public string Path { get; set; }
        
        public TfsHipChatConfig Config
        {
            get { return _config ?? (_config = ReadConfig(Path)); }
        }

        public List<string> Validate()
        {
            var errors = new List<string>();

            try
            {
                ReadConfig(Path);
            }
            catch (FileNotFoundException)
            {
                errors.Add(String.Format(
                    "Can't find configuration file. Copy SampleConfig.json to {0} and fill in the blanks.",
                    Path));
            }
            catch (JsonReaderException)
            {
                errors.Add(String.Format(
                    "Can't parse the {0} configuration file. Ensure it is a valid JSON file.",
                    Path));
            }

            return errors;
        }

        private static TfsHipChatConfig ReadConfig(string path)
        {
            using (var reader = new JsonTextReader(new StreamReader(path)))
            {
                return (new JsonSerializer()).Deserialize<TfsHipChatConfig>(reader);
            }
        }
    }
}
