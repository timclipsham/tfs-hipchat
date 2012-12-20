using System;
using System.ServiceModel;

namespace TfsHipChat
{
    class Program
    {
        static void Main()
        {
            using (var host = new ServiceHost(typeof(CheckinEventService)))
            {
                host.Open();
                Console.WriteLine("TfsHipChat started!");
                Console.ReadLine();
            }
        }
    }
}
