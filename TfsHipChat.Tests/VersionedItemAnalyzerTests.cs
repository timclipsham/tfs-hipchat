using System.Collections.Generic;
using Microsoft.TeamFoundation.VersionControl.Common;
using Xunit;

namespace TfsHipChat.Tests
{
    public class VersionedItemAnalyzerTests
    {
        [Fact]
        public void GetCommonPath_ShouldReturnCommonPathIncludingFileName_WhenGivenOneVersionedItem()
        {
            const string filePath = "$/SomeTeamProject/TfsPathTo/MyFile.cs";
            var versionedItems = new List<ClientArtifact> { CreateVersionedItem(filePath) };

            var commonPath = VersionedItemAnalyzer.GetCommonPath(versionedItems);

            Assert.Equal(filePath, commonPath);
        }

        [Fact]
        public void GetCommonPath_ShouldReturnCommonPath_WhenGivenMultipleVersionedItems()
        {
            var versionedItems = new List<ClientArtifact>
                                     {
                                         CreateVersionedItem("$/SomeTeamProject/TfsPathTo/MyFirstFile.cs"),
                                         CreateVersionedItem("$/SomeTeamProject/TfsPathTo/SomeOther/FileChanged.cs")
                                     };

            var commonPath = VersionedItemAnalyzer.GetCommonPath(versionedItems);

            Assert.Equal("$/SomeTeamProject/TfsPathTo/...", commonPath);
        }

        [Fact]
        public void GetCommonPath_ShouldReturnCommonPath_WhenGivenOnePathContainedWithinAnother()
        {
            var versionedItems = new List<ClientArtifact>
                                     {
                                         CreateVersionedItem("$/SomeTeamProject/TfsPathTo/MyFirstFile.c"),
                                         CreateVersionedItem("$/SomeTeamProject/TfsPathTo/MyFirstFile.cpp")
                                     };

            var commonPath = VersionedItemAnalyzer.GetCommonPath(versionedItems);

            Assert.Equal("$/SomeTeamProject/TfsPathTo/...", commonPath);
        }

        [Fact]
        public void GetCommonPath_ShouldReturnEmptyString_WhenNoCommonPath()
        {
            var versionedItems = new List<ClientArtifact>
                                     {
                                         CreateVersionedItem("abc/def.cs"),
                                         CreateVersionedItem("def/abc.cs")
                                     };

            var commonPath = VersionedItemAnalyzer.GetCommonPath(versionedItems);

            Assert.Equal("", commonPath);
        }

        [Fact]
        public void GetCommonPath_ShouldReturnEmptyString_WhenNoPracticalCommonPath()
        {
            var versionedItems = new List<ClientArtifact>
                                     {
                                         CreateVersionedItem("$/ProjectGreen/File.rb"),
                                         CreateVersionedItem("$/ProjectBlue/File.rb")
                                     };

            var commonPath = VersionedItemAnalyzer.GetCommonPath(versionedItems);

            Assert.Equal("", commonPath);
        }

        private static ClientArtifact CreateVersionedItem(string serverItem)
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
                ServerItem = serverItem
            };
        }
    }
}
