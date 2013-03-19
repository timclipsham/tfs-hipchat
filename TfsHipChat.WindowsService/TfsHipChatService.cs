using System;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceProcess;
using Newtonsoft.Json;
using TfsHipChat.Configuration;
using TfsHipChat.WindowsService.Properties;

namespace TfsHipChat.WindowsService
{
    partial class TfsHipChatService : ServiceBase
    {
        private ServiceHost _host;

        public TfsHipChatService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var servicePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var configPath = Path.Combine(servicePath ?? "", Settings.Default.DefaultConfigPath);
            var error = ValidateConfiguration(configPath);

            if (error != null)
            {
                throw new Exception(error);
            }

            var configurationProvider = new ConfigurationProvider(configPath);
            var hipChatNotifier = new HipChatNotifier(configurationProvider);
            var notificationHandler = new NotificationHandler(hipChatNotifier, configurationProvider);
            _host = new ServiceHost(new TfsHipChatEventService(notificationHandler));
            _host.Open();
        }

        protected override void OnStop()
        {
            _host.Close();
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
