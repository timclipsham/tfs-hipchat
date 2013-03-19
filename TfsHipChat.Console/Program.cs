using System.IO;
using System.Reflection;
using System.ServiceModel;
using TfsHipChat.Configuration;
using TfsHipChat.Console.Properties;

namespace TfsHipChat.Console
{
    static class Program
    {
        static void Main()
        {
            var configurationProvider = new ConfigurationProvider { Path = GetConfigPath() };
            var errors = configurationProvider.Validate();

            if (errors.Count > 0)
            {
                errors.ForEach(e => System.Console.WriteLine("ERROR: " + e));
                return;
            }

            StartService(configurationProvider);
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

        private static string GetConfigPath()
        {
            var servicePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(servicePath ?? "", Settings.Default.DefaultConfigPath);
        }
    }
}
