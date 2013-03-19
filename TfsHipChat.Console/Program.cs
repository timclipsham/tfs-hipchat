using System;
using System.IO;
using System.ServiceModel;
using Newtonsoft.Json;
using TfsHipChat.Configuration;
using TfsHipChat.Console.Properties;

namespace TfsHipChat.Console
{
    static class Program
    {
        static void Main()
        {
            var configPath = Settings.Default.DefaultConfigPath;
            var error = ValidateConfiguration(configPath);

            if (error != null)
            {
                System.Console.WriteLine("ERROR: " + error);
                return;
            }

            StartService(new ConfigurationProvider(configPath));
        }

        private static void StartService(IConfigurationProvider configurationProvider)
        {
            var hipChatNotifier = new HipChatNotifier(configurationProvider);
            var notificationHandler = new NotificationHandler(hipChatNotifier, configurationProvider);

            using (var host = new ServiceHost(new TfsHipChatEventService(notificationHandler)))
            {
                host.Open();
                System.Console.WriteLine("TfsHipChat started!");
                System.Console.ReadLine();
            }
        }

        private static string ValidateConfiguration(string path)
        {
            string error = null;

            try
            {
                // ReSharper disable ObjectCreationAsStatement
                new ConfigurationProvider(path);
                // ReSharper restore ObjectCreationAsStatement
            }
            catch (FileNotFoundException)
            {
                error = String.Format(
                    "Can't find configuration file. Copy SampleConfig.json to {0} and fill in the blanks.",
                    path);
            }
            catch (JsonReaderException)
            {
                error = String.Format(
                    "Can't parse the {0} configuration file. Ensure it is a valid JSON file.",
                    path);
            }

            return error;
        }
    }
}
