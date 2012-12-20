using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace TfsHipChat.Tests
{
    public class CheckinEventServiceTests
    {
        [Fact]
        public void Notify_ShouldThrowException_WhenInvalidXmlData()
        {
            var notifier = Substitute.For<INotifier>();
            var checkinEventService = new CheckinEventService(notifier);
            const string eventXml = "invalid_xml";
            const string tfsIdentityXml = "invalid_xml";

            Assert.Throws<InvalidOperationException>(() => {
                checkinEventService.Notify(eventXml, tfsIdentityXml);
            });
        }
    }
}
