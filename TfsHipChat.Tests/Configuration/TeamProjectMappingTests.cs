using System.IO;
using Newtonsoft.Json;
using TfsHipChat.Configuration;
using Xunit;

namespace TfsHipChat.Tests.Configuration
{
    public class TeamProjectMappingTests
    {
        [Fact]
        public void TeamProjectName_ShouldReturnTeamProjectName_WhenValidJson()
        {
            var mapping = GetTeamProjectMapping();
            Assert.Equal("TestProject", mapping.TeamProjectName);
        }

        [Fact]
        public void HipChatRoomId_ShouldReturnHipChatRoomId_WhenValidJson()
        {
            var mapping = GetTeamProjectMapping();
            Assert.Equal(1234, mapping.HipChatRoomId);
        }

        private static TeamProjectMapping GetTeamProjectMapping()
        {
            using (var reader = new JsonTextReader(new StreamReader(@"Configuration\TestConfig.json")))
            {
                return (new JsonSerializer()).Deserialize<TeamProjectMapping>(reader);
            }
        }
    }
}
