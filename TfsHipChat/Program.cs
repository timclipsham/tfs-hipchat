using System;
using System.IO;
using System.ServiceModel;
using Newtonsoft.Json;
using TfsHipChat.Configuration;
using TfsHipChat.Properties;

namespace TfsHipChat
{
    static class Program
    {
        static void Main()
        {
            var error = ValidateConfiguration();

            if (error != null)
            {
                Console.WriteLine("ERROR: " + error);
                return;
            }

            StartService();
        }

        private static void StartService()
        {
            using (var host = new ServiceHost(typeof (TfsHipChatEventService)))
            {
                host.Open();
                Console.WriteLine("TfsHipChat started!");
                Console.ReadLine();
            }
        }

        private static string ValidateConfiguration()
        {
            string error = null;

            try
            {
                // ReSharper disable ObjectCreationAsStatement
                new ConfigurationProvider();
                // ReSharper restore ObjectCreationAsStatement
            }
            catch (FileNotFoundException)
            {
                error = String.Format(
                    "Can't find configuration file. Copy SampleConfig.json to {0} and fill in the blanks.",
                    Settings.Default.DefaultConfigPath);
            }
            catch (JsonReaderException)
            {
                error = String.Format("Can't parse the {0} configuration file. Ensure it is a valid JSON file.",
                    Settings.Default.DefaultConfigPath);
            }

            return error;
        }
    }
}
