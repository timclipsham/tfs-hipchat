using System;
using System.Collections.Generic;
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
            var notifier = Substitute.For<INotifier>();
            var eventService = new TfsHipChatEventService(notifier, new Dictionary<string, int>());
            const string eventXml = "invalid_xml";

            Assert.Throws<XmlException>(() => eventService.Notify(eventXml, TfsIdentityXml));
        }

        [Fact]
        public void Notify_ShouldThrowException_WhenUnsupportedEvent()
        {
            var notifier = Substitute.For<INotifier>();
            var eventService = new TfsHipChatEventService(notifier, new Dictionary<string, int>());
            const string eventXml = "<EventThatDoesNotExist></EventThatDoesNotExist>";

            Assert.Throws<NotSupportedException>(() => eventService.Notify(eventXml, TfsIdentityXml));
        }

        [Fact]
        public void Notify_ShouldSendCheckinNotification_WhenCheckinEvent()
        {
            var notifier = Substitute.For<INotifier>();
            var teamProjectMap = new Dictionary<string, int> { { "testproject", 1234 } };
            var eventService = new TfsHipChatEventService(notifier, teamProjectMap);
            var eventXml = CreateCheckinEvent();

            eventService.Notify(eventXml, TfsIdentityXml);

            notifier.ReceivedWithAnyArgs().SendCheckinNotification(null, 0);
        }

        [Fact]
        public void Notify_ShouldSendBuildCompletionFailedNotification_WhenBuildIsBroken()
        {
            var notifier = Substitute.For<INotifier>();
            var teamProjectMap = new Dictionary<string, int> { { "testproject", 1234 } };
            var eventService = new TfsHipChatEventService(notifier, teamProjectMap);
            var eventXml = CreateFailedBuildCompletion();

            eventService.Notify(eventXml, TfsIdentityXml);

            notifier.ReceivedWithAnyArgs().SendBuildCompletionFailedNotification(null, 0);
        }

        [Fact]
        public void Notify_ShouldSendBuildCompletionSuccessNotification_WhenBuildIsSuccessful()
        {
            var notifier = Substitute.For<INotifier>();
            var teamProjectMap = new Dictionary<string, int> { { "testproject", 1234 } };
            var eventService = new TfsHipChatEventService(notifier, teamProjectMap);
            var eventXml = CreateSuccessfulBuildCompletion();

            eventService.Notify(eventXml, TfsIdentityXml);

            notifier.ReceivedWithAnyArgs().SendBuildCompletionSuccessNotification(null, 0);
        }

        [Fact]
        public void Notify_ShouldNotSendBuildCompletionFailedNotification_WhenBuildIsSuccessful()
        {
            var notifier = Substitute.For<INotifier>();
            var teamProjectMap = new Dictionary<string, int> { { "testproject", 1234 } };
            var eventService = new TfsHipChatEventService(notifier, teamProjectMap);
            var eventXml = CreateSuccessfulBuildCompletion();

            eventService.Notify(eventXml, TfsIdentityXml);

            notifier.DidNotReceiveWithAnyArgs().SendBuildCompletionFailedNotification(null, 0);
        }

        [Fact]
        public void Notify_ShouldNotSendBuildCompletionSuccessNotification_WhenBuildIsFailed()
        {
            var notifier = Substitute.For<INotifier>();
            var teamProjectMap = new Dictionary<string, int> { { "testproject", 1234 } };
            var eventService = new TfsHipChatEventService(notifier, teamProjectMap);
            var eventXml = CreateFailedBuildCompletion();

            eventService.Notify(eventXml, TfsIdentityXml);

            notifier.DidNotReceiveWithAnyArgs().SendBuildCompletionSuccessNotification(null, 0);
        }

        private static string CreateCheckinEvent()
        {
            var checkinEvent = new CheckinEvent { TeamProject = "TestProject" };
            var serializer = new XmlSerializer(typeof(CheckinEvent));
            var sw = new StringWriter();
            serializer.Serialize(sw, checkinEvent);

            return sw.ToString();
        }

        private static string CreateSuccessfulBuildCompletion()
        {
            var buildEvent = new BuildCompletionEvent { CompletionStatus = "Successfully Completed", TeamProject = "TestProject" };

            var serializer = new XmlSerializer(typeof(BuildCompletionEvent));
            var sw = new StringWriter();
            serializer.Serialize(sw, buildEvent);

            return sw.ToString();
        }

        private static string CreateFailedBuildCompletion()
        {
            var buildEvent = new BuildCompletionEvent { TeamProject = "TestProject" };

            var serializer = new XmlSerializer(typeof(BuildCompletionEvent));
            var sw = new StringWriter();
            serializer.Serialize(sw, buildEvent);

            return sw.ToString();
        }
    }
}
