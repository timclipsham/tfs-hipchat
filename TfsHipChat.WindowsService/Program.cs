using System.ServiceProcess;

namespace TfsHipChat.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var servicesToRun = new ServiceBase[] { new TfsHipChatService() };
            ServiceBase.Run(servicesToRun);
        }
    }
}
