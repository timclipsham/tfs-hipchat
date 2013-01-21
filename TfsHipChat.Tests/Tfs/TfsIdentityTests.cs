using System.IO;
using TfsHipChat.Tfs;
using Xunit;

namespace TfsHipChat.Tests.Tfs
{
    public class TfsIdentityTests
    {
        [Fact]
        public void Url_ShouldReturnTheServerUrl_WhenDeserializedUsingValidXml()
        {
            const string serverUrl = "http://some-tfs-server.com";
            const string tfsIdentityXml = "<TeamFoundationServer url=\"" + serverUrl + "\" />";
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof (TfsIdentity));
            string url = null;

            using (var reader = new StringReader(tfsIdentityXml))
            {
                var tfsIdentity = serializer.Deserialize(reader) as TfsIdentity;
                if (tfsIdentity != null) url = tfsIdentity.Url;
            }

            Assert.Equal(serverUrl, url);
        }
    }
}
