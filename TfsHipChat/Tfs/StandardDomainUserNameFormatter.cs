using System.Globalization;
using System.Text.RegularExpressions;

namespace TfsHipChat.Tfs
{
    public class StandardDomainUserNameFormatter : IUserNameFormatter
    {
        // regex to match "DOMAIN\user.name"
        private static readonly Regex StandardDomainUserName = new Regex(
            @"^[^\\]+\\([^\.]+)\.([^\.]+)", RegexOptions.Compiled);

        public bool Valid(string userName)
        {
            return StandardDomainUserName.Match(userName).Success;
        }

        public string ToDisplayName(string userName)
        {
            if (!Valid(userName))
            {
                return userName;
            }

            var match = StandardDomainUserName.Match(userName);
            var displayName = match.Groups[1].Value + " " + match.Groups[2].Value;
            return CultureInfo.GetCultureInfo("en-US").TextInfo.ToTitleCase(displayName);
        }
    }
}