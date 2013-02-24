using TfsHipChat.Tfs;
using Xunit;

namespace TfsHipChat.Tests.Tfs
{
    public class StandardDomainUserNameFormatterTests
    {
        [Fact]
        public void Valid_ShouldReturnTrue_WhenStandardDomainUserName()
        {
            const string userName = @"DOMAIN\user.name";
            var formatter = new StandardDomainUserNameFormatter();

            var valid = formatter.Valid(userName);

            Assert.True(valid);
        }

        [Fact]
        public void Valid_ShouldReturnFalse_WhenNonStandardDomainUserName()
        {
            const string userName = @"DOMAIN\uname1";
            var formatter = new StandardDomainUserNameFormatter();

            var valid = formatter.Valid(userName);

            Assert.False(valid);
        }

        [Fact]
        public void ToDisplayName_ShouldReturnFormattedDisplayName_WhenStandardDomainUserName()
        {
            const string userName = @"DOMAIN\user.name";
            var formatter = new StandardDomainUserNameFormatter();

            var displayName = formatter.ToDisplayName(userName);

            Assert.Equal("User Name", displayName);
        }

        [Fact]
        public void ToDisplayName_ShouldReturnGivenUserName_WhenNonStandardDomainUserName()
        {
            const string userName = @"DOMAIN\uname1";
            var formatter = new StandardDomainUserNameFormatter();

            var displayName = formatter.ToDisplayName(userName);

            Assert.Equal(@"DOMAIN\uname1", displayName);
        }
    }
}
