namespace TfsHipChat.Tfs
{
    public interface IUserNameFormatter
    {
        bool Valid(string userName);
        string ToDisplayName(string userName);
    }
}