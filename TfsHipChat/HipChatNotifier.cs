using HipChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsHipChat
{
    public class HipChatNotifier : INotifier
    {
        private HipChatClient _hipChatClient;

        public HipChatNotifier(HipChatClient hipChatClient)
        {
            _hipChatClient = hipChatClient;
        }

        public void SendMessage(string message)
        {
            _hipChatClient.SendMessage(message);
        }
    }
}
