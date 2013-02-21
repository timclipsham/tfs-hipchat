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

        [Fact]
        public void GetCommitterName_ShouldReturnCommitterDisplay_WhenItExists()
        {
            var checkinEvent = new CheckinEvent
            {
                CommitterDisplay = "User Name",
                Committer = @"DOMAIN\user.name"
            };

            var committerName = checkinEvent.GetCommitterName();

            Assert.Equal("User Name", committerName);
        }

        [Fact]
        public void GetCommitterName_ShouldReturnCommitter_WhenCommitterDisplayIsNull()
        {
            var checkinEvent = new CheckinEvent { Committer = @"DOMAIN\user.name" };

            var committerName = checkinEvent.GetCommitterName();

            Assert.Equal(@"DOMAIN\user.name", committerName);
        }

        [Fact]
        public void GetCommitterName_ShouldReturnCommitter_WhenCommitterDisplayIsEmpty()
        {
            var checkinEvent = new CheckinEvent
            {
                CommitterDisplay = "",
                Committer = @"DOMAIN\user.name"
            };

            var committerName = checkinEvent.GetCommitterName();

            Assert.Equal(@"DOMAIN\user.name", committerName);
        }

        [Fact]
        public void GetCommitterName_ShouldReturnCommitter_WhenCommitterDisplayIsWhitespace()
        {
            var checkinEvent = new CheckinEvent
            {
                CommitterDisplay = " \t ",
                Committer = @"DOMAIN\user.name"
            };

            var committerName = checkinEvent.GetCommitterName();

            Assert.Equal(@"DOMAIN\user.name", committerName);
        }
    }
}
