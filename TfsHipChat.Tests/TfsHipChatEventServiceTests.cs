using System;
using NSubstitute;
using TfsHipChat.Tfs.Events;
using Xunit;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace TfsHipChat.Tests
{
    public class TfsHipChatEventServiceTests
    {
        private const string TfsIdentityXml = "";

        [Fact]
        public void Notify_ShouldThrowException_WhenInvalidXmlData()
        {
            var notificationHandler = Substitute.For<INotificationHandler>();
            var eventService = new TfsHipChatEventService(notificationHandler);
            const string eventXml = "invalid_xml";

            Assert.Throws<XmlException>(() => eventService.Notify(eventXml, TfsIdentityXml));
        }

        [Fact]
        public void Notify_ShouldThrowException_WhenUnsupportedEvent()
        {
            var notificationHandler = Substitute.For<INotificationHandler>();
            var eventService = new TfsHipChatEventService(notificationHandler);
            const string eventXml = "<EventThatDoesNotExist></EventThatDoesNotExist>";

            Assert.Throws<NotSupportedException>(() => eventService.Notify(eventXml, TfsIdentityXml));
        }

        [Fact]
        public void Notify_ShouldHandleCheckinEvent_WhenCheckinEvent()
        {
            var notificationHandler = Substitute.For<INotificationHandler>();
            var eventService = new TfsHipChatEventService(notificationHandler);
            var eventXml = CreateCheckinEvent();

            eventService.Notify(eventXml, TfsIdentityXml);

            notificationHandler.ReceivedWithAnyArgs().HandleCheckin(null);
        }

        [Fact]
        public void Notify_ShouldHandleBuildCompletionEvent_WhenBuildCompletionEvent()
        {
            var notificationHandler = Substitute.For<INotificationHandler>();
            var eventService = new TfsHipChatEventService(notificationHandler);
            var eventXml = CreateBuildCompletionEvent();

            eventService.Notify(eventXml, TfsIdentityXml);

            notificationHandler.ReceivedWithAnyArgs().HandleBuildCompletion(null);
        }

        private static string CreateCheckinEvent()
        {
            var checkinEvent = new CheckinEvent();

            var serializer = new XmlSerializer(typeof(CheckinEvent));
            var sw = new StringWriter();
            serializer.Serialize(sw, checkinEvent);

            return sw.ToString();
        }

        private static string CreateBuildCompletionEvent()
        {
            var buildEvent = new BuildCompletionEvent();

            var serializer = new XmlSerializer(typeof(BuildCompletionEvent));
            var sw = new StringWriter();
            serializer.Serialize(sw, buildEvent);

            return sw.ToString();
        }
    }
}
