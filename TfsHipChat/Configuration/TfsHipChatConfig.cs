using System.Collections.Generic;

namespace TfsHipChat.Configuration
{
    public class TfsHipChatConfig
    {
        public string HipChatToken { get; set; }
        public string HipChatFrom { get; set; }
        public List<TeamProjectMapping> TeamProjectMappings { get; set; }
    }
}