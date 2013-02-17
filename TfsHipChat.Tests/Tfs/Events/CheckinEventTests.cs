using System.Collections.Generic;
using TfsHipChat.Tfs.Events;
using Xunit;

namespace TfsHipChat.Tests.Tfs.Events
{
    public class CheckinEventTests
    {
        [Fact]
        public void GetChangesetUrl_ShouldReturnChangesetUrl_WhenUrlExists()
        {
            const string checkinUrl = "http://some-tfs-server.com/path?query=0";
            var checkinEvent = new CheckinEvent { Artifacts = new List<ClientArtifact>() };
            checkinEvent.Artifacts.Add(new ClientArtifact { ArtifactType = "Changeset", Url = checkinUrl });

            var url = checkinEvent.GetChangesetUrl();

            Assert.Equal(checkinUrl, url);
        }

        [Fact]
        public void GetChangesetUrl_ShouldReturnNull_WhenUrlDoesNotExist()
        {
            var checkinEvent = new CheckinEvent { Artifacts = new List<ClientArtifact>() };

            var url = checkinEvent.GetChangesetUrl();

            Assert.Equal(null, url);
        }
    }
}
