using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace TfsHipChat.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (Environment.UserInteractive && args.Length > 0)
            {
                var path = Assembly.GetExecutingAssembly().Location;

                switch (args[0])
                {
                    case "/i":
                        ManagedInstallerClass.InstallHelper(new[] { path });
                        break;
                    case "/u":
                        ManagedInstallerClass.InstallHelper(new[] { "/u", path });
                        break;
                    default:
                        throw new NotSupportedException("Given argument(s) not supported: " + string.Concat(args));
                }
            }
            else
            {
                var servicesToRun = new ServiceBase[] { new TfsHipChatService() };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
