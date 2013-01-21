using System.Collections;
using Microsoft.TeamFoundation.VersionControl.Common;
using TfsHipChat.Tfs.Events;
using Xunit;

namespace TfsHipChat.Tests.Events
{
    public class CheckinEventExtensionsTests
    {
        [Fact]
        public void GetChangesetUrl_ShouldReturnChangesetUrl_WhenUrlExists()
        {
            const string checkinUrl = "http://some-tfs-server.com/path?query=0";
            var checkinEvent = new CheckinEvent { Artifacts = new ArrayList() };
            checkinEvent.Artifacts.Add(new ClientArtifact(checkinUrl, "Changeset"));

            var url = checkinEvent.GetChangesetUrl();

            Assert.Equal(checkinUrl, url);
        }

        [Fact]
        public void GetChangesetUrl_ShouldReturnNull_WhenUrlDoesNotExist()
        {
            var checkinEvent = new CheckinEvent { Artifacts = new ArrayList() };

            var url = checkinEvent.GetChangesetUrl();

            Assert.Equal(null, url);
        }
    }
}
