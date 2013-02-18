namespace TfsHipChat.Configuration
{
    public interface IConfigurationProvider
    {
        TfsHipChatConfig Config { get; }
    }
}