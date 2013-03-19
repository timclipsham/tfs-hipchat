using System.Collections.Generic;

namespace TfsHipChat.Configuration
{
    public interface IConfigurationProvider
    {
        TfsHipChatConfig Config { get; }
        List<string> Validate();
    }
}