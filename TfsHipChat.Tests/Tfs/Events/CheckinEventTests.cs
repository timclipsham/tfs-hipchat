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
                CommitterDisplay = "Committer Display",
                Committer = "Committer"
            };

            var committerName = checkinEvent.GetCommitterName();

            Assert.Equal("Committer Display", committerName);
        }

        [Fact]
        public void GetCommitterName_ShouldReturnCommitter_WhenCommitterDisplayIsNull()
        {
            var checkinEvent = new CheckinEvent { Committer = "Committer" };

            var committerName = checkinEvent.GetCommitterName();

            Assert.Equal("Committer", committerName);
        }

        [Fact]
        public void GetCommitterName_ShouldReturnCommitter_WhenCommitterDisplayIsEmpty()
        {
            var checkinEvent = new CheckinEvent
            {
                CommitterDisplay = "",
                Committer = "Committer"
            };

            var committerName = checkinEvent.GetCommitterName();

            Assert.Equal("Committer", committerName);
        }

        [Fact]
        public void GetCommitterName_ShouldReturnCommitter_WhenCommitterDisplayIsWhitespace()
        {
            var checkinEvent = new CheckinEvent
                                   {
                                       CommitterDisplay = " \t ",
                                       Committer = "Committer"
                                   };

            var committerName = checkinEvent.GetCommitterName();

            Assert.Equal("Committer", committerName);
        }

        [Fact]
        public void GetCommitterName_ShouldReturnFormattedCommitter_WhenNoCommitterDisplayAndStandardCommitterFormat()
        {
            var checkinEvent = new CheckinEvent { Committer = @"DOMAIN\user.name" };

            var displayName = checkinEvent.GetCommitterName();

            Assert.Equal("User Name", displayName);
        }

        [Fact]
        public void GetCommitterName_ShouldReturnCommitter_WhenNoDisplayNameAndNonStanardCommitterFormat()
        {
            var checkinEvent = new CheckinEvent { Committer = @"DOMAIN\uname1" };

            var displayName = checkinEvent.GetCommitterName();

            Assert.Equal(@"DOMAIN\uname1", displayName);
        }
    }
}
