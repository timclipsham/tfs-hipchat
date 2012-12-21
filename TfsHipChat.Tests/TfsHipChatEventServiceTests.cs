using System;
using NSubstitute;
using Xunit;
using Microsoft.TeamFoundation.VersionControl.Common;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using TfsHipChat.Events;
using System.Xml;

namespace TfsHipChat.Tests
{
    public class TfsHipChatEventServiceTests
    {
        private const string TfsIdentityXml = "";

        [Fact]
        public void Notify_ShouldThrowException_WhenInvalidXmlData()
        {
            var notifier = Substitute.For<INotifier>();
            var eventService = new TfsHipChatEventService(notifier);
            const string eventXml = "invalid_xml";

            Assert.Throws<XmlException>(() => eventService.Notify(eventXml, TfsIdentityXml));
        }

        [Fact]
        public void Notify_ShouldThrowException_WhenUnsupportedEvent()
        {
            var notifier = Substitute.For<INotifier>();
            var eventService = new TfsHipChatEventService(notifier);
            const string eventXml = "<EventThatDoesNotExist></EventThatDoesNotExist>";

            Assert.Throws<NotSupportedException>(() => eventService.Notify(eventXml, TfsIdentityXml));
        }

        [Fact]
        public void Notify_ShouldSendCheckinNotification_WhenCheckinEvent()
        {
            var notifier = Substitute.For<INotifier>();
            var eventService = new TfsHipChatEventService(notifier);
            string eventXml = CreateCheckinEvent();

            eventService.Notify(eventXml, TfsIdentityXml);

            notifier.ReceivedWithAnyArgs().SendCheckinNotification(null);
        }

        [Fact]
        public void Notify_ShouldSendBuildCompletionFailedNotification_WhenBuildIsBroken()
        {
            var notifier = Substitute.For<INotifier>();
            var eventService = new TfsHipChatEventService(notifier);
            string eventXml = CreateFailedBuildCompletion();

            eventService.Notify(eventXml, TfsIdentityXml);

            notifier.ReceivedWithAnyArgs().SendBuildCompletionFailedNotification(null);
        }

        [Fact]
        public void Notify_ShouldNotSendBuildCompletionFailedNotification_WhenBuildIsSuccessful()
        {
            var notifier = Substitute.For<INotifier>();
            var eventService = new TfsHipChatEventService(notifier);
            string eventXml = CreateSuccessfulBuildCompletion();

            eventService.Notify(eventXml, TfsIdentityXml);

            notifier.DidNotReceiveWithAnyArgs().SendBuildCompletionFailedNotification(null);
        }

        private string CreateCheckinEvent()
        {
            var checkinEvent = new CheckinEvent(1000, new DateTime(), "owner", "commiter", "some comment")
                                   {
                                       Artifacts = new ArrayList() // serialization fails without this
                                   };

            var serializer = new XmlSerializer(typeof(CheckinEvent));
            var sw = new StringWriter();
            serializer.Serialize(sw, checkinEvent);

            return sw.ToString();
        }

        private string CreateSuccessfulBuildCompletion()
        {
            var buildEvent = new BuildCompletionEvent { CompletionStatus = "Successfully Completed" };
            var serializer = new XmlSerializer(typeof(BuildCompletionEvent));
            var sw = new StringWriter();
            serializer.Serialize(sw, buildEvent);

            return sw.ToString();
        }

        private string CreateFailedBuildCompletion()
        {
            var buildEvent = new BuildCompletionEvent();

            var serializer = new XmlSerializer(typeof(BuildCompletionEvent));
            var sw = new StringWriter();
            serializer.Serialize(sw, buildEvent);

            return sw.ToString();
        }
    }
}
