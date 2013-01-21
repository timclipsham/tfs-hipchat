using System.Collections;
using Microsoft.TeamFoundation.VersionControl.Common;
using TfsHipChat.Tfs.Events;
using Xunit;

namespace TfsHipChat.Tests.Tfs.Events
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

        [Fact]
        public void GetVersionedItems_ShouldReturnVersionedItems_WhenTheyExist()
        {
            var versionedItem = CreateVersionedItem();
            var checkinEvent = new CheckinEvent { Artifacts = new ArrayList() };
            checkinEvent.Artifacts.Add(versionedItem);

            var versionedItems = checkinEvent.GetVersionedItems();

            Assert.Contains(versionedItem, versionedItems);
        }

        [Fact]
        public void GetVersionedItems_ShouldReturnCorrectNumberOfItems_WhenArtifactsOfDifferentTypesExist()
        {
            var versionedItem = CreateVersionedItem();
            var otherItem = new ClientArtifact("http://some-tfs-server.com/url_to_other_item", "Changeset");
            var checkinEvent = new CheckinEvent { Artifacts = new ArrayList() };
            checkinEvent.Artifacts.Add(versionedItem);
            checkinEvent.Artifacts.Add(otherItem);

            var versionedItems = checkinEvent.GetVersionedItems();

            Assert.Equal(1, versionedItems.Count);
        }

        [Fact]
        public void GetVersionedItems_ShouldReturnAnEmptyCollection_WhenNoVersionedItemsExist()
        {
            var checkinEvent = new CheckinEvent { Artifacts = new ArrayList() };

            var versionedItems = checkinEvent.GetVersionedItems();

            Assert.Empty(versionedItems);
        }

        private static ClientArtifact CreateVersionedItem()
        {
            return new ClientArtifact
                       {
                           Url = "http://some-tfs-server.com/url_to_versioned_item",
                           Type = "VersionedItem",
                           Item = "MyFile.cs",
                           Folder = "$/SomeTeamProject/TfsPathTo",
                           TeamProject = "SomeTeamProject",
                           ItemVersion = "10001",
                           ChangeType = "edit",
                           ServerItem = "$/SomeTeamProject/TfsPathTo/MyFile.cs"
                       };
        }
    }
}
