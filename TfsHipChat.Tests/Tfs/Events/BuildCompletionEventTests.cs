using TfsHipChat.Tfs.Events;
using Xunit;

namespace TfsHipChat.Tests.Tfs.Events
{
    public class BuildCompletionEventTests
    {
        [Fact]
        public void GetRequestedByName_ShouldReturnFormattedName_WhenStandardDomainName()
        {
            var buildEvent = new BuildCompletionEvent { RequestedBy = @"DOMAIN\user.name" };

            var name = buildEvent.GetRequestedByName();

            Assert.Equal("User Name", name);
        }

        [Fact]
        public void GetRequestedByName_ShouldReturnUnformattedName_WhenNonStandardDomainName()
        {
            var buildEvent = new BuildCompletionEvent { RequestedBy = @"DOMAIN\uname1" };

            var name = buildEvent.GetRequestedByName();

            Assert.Equal(@"DOMAIN\uname1", name);
        }
    }
}
