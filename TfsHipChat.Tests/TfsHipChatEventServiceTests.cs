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
        public void Notify_ShouldHandleCheckinEvent()
        {
            var notificationHandler = Substitute.For<INotificationHandler>();
            var eventService = new TfsHipChatEventService(notificationHandler);
            var eventXml = CreateEvent<CheckinEvent>();

            eventService.Notify(eventXml, TfsIdentityXml);

            notificationHandler.ReceivedWithAnyArgs().HandleCheckin(null);
        }

        [Fact]
        public void Notify_ShouldHandleBuildCompletionEvent()
        {
            var notificationHandler = Substitute.For<INotificationHandler>();
            var eventService = new TfsHipChatEventService(notificationHandler);
            var eventXml = CreateEvent<BuildCompletionEvent>();

            eventService.Notify(eventXml, TfsIdentityXml);

            notificationHandler.ReceivedWithAnyArgs().HandleBuildCompletion(null);
        }

        private static string CreateEvent<T>() where T : new()
        {
            var eventObject = new T();
            var serializer = new XmlSerializer(typeof(T));
            var sw = new StringWriter();
            serializer.Serialize(sw, eventObject);
            return sw.ToString();
        }
    }
}
