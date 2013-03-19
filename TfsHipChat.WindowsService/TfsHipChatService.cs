using System;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceProcess;
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
            var configurationProvider = new ConfigurationProvider { Path = GetConfigPath() };
            var errors = configurationProvider.Validate();

            if (errors.Count > 0)
            {
                var message = String.Join("\n", errors);
                throw new Exception(message);
            }
            
            var hipChatNotifier = new HipChatNotifier(configurationProvider);
            var notificationHandler = new NotificationHandler(hipChatNotifier, configurationProvider);
            _host = new ServiceHost(new TfsHipChatEventService(notificationHandler));
            _host.Open();
        }

        protected override void OnStop()
        {
            _host.Close();
        }

        private static string GetConfigPath()
        {
            var servicePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(servicePath ?? "", Settings.Default.DefaultConfigPath);
        }
    }
}
