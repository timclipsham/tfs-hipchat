using System.Collections.Generic;
using NSubstitute;
using TfsHipChat.Configuration;
using TfsHipChat.Tfs.Events;
using Xunit;

namespace TfsHipChat.Tests
{
    public class NotificationHandlerTests
    {
        [Fact]
        public void HandleBuildCompletionEvent_ShouldSendBuildFailedNotification_WhenBuildIsBroken()
        {
            var notifier = Substitute.For<INotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent { TeamProject = "TestProject" };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.ReceivedWithAnyArgs().SendBuildCompletionFailedNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletionEvent_ShouldSendBuildSuccessNotification_WhenBuildIsSuccessful()
        {
            var notifier = Substitute.For<INotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent
                                 {
                                     CompletionStatus = "Successfully Completed",
                                     TeamProject = "TestProject"
                                 };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.ReceivedWithAnyArgs().SendBuildCompletionSuccessNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletionEvent_ShouldNotSendBuildFailedNotification_WhenBuildIsSuccessful()
        {
            var notifier = Substitute.For<INotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent
                                 {
                                     CompletionStatus = "Successfully Completed",
                                     TeamProject = "TestProject"
                                 };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.DidNotReceiveWithAnyArgs().SendBuildCompletionFailedNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletionEvent_ShouldNotSendBuildSuccessNotification_WhenBuildIsBroken()
        {
            var notifier = Substitute.For<INotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent { TeamProject = "TestProject" };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.DidNotReceiveWithAnyArgs().SendBuildCompletionSuccessNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletionEvent_ShouldSendBuildSuccessNotificationToCorrectRoom_WhenBuildIsSuccessful()
        {
            var notifier = Substitute.For<INotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent
                                 {
                                     CompletionStatus = "Successfully Completed",
                                     TeamProject = "AnotherTestProject"
                                 };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.Received().SendBuildCompletionSuccessNotification(buildEvent, 456);
        }

        [Fact]
        public void HandleBuildCompletionEvent_ShouldSendBuildFailedNotificationToCorrectRoom_WhenBuildIsBroken()
        {
            var notifier = Substitute.For<INotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent { TeamProject = "AnotherTestProject" };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.Received().SendBuildCompletionFailedNotification(buildEvent, 456);
        }

        [Fact]
        public void HandleBuildCompletionEvent_ShouldNotSendBuildFailedNotification_WhenBuildIsBrokenAndTeamProjectMappingNotDefined()
        {
            var notifier = Substitute.For<INotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent{ TeamProject = "ProjectWithNoMapping" };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.DidNotReceiveWithAnyArgs().SendBuildCompletionFailedNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletionEvent_ShouldNotSendBuildSuccessNotification_WhenBuildIsSuccessfulAndTeamProjectMappingNotDefined()
        {
            var notifier = Substitute.For<INotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent
                                 {
                                     CompletionStatus = "Successfully Completed",
                                     TeamProject = "ProjectWithNoMapping"
                                 };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.DidNotReceiveWithAnyArgs().SendBuildCompletionSuccessNotification(null, 0);
        }

        private static IConfigurationProvider CreateFakeConfigurationProvider()
        {
            var config = new TfsHipChatConfig
                             {
                                 TeamProjectMappings = new List<TeamProjectMapping>
                                                           {
                                                               new TeamProjectMapping("TestProject", 123),
                                                               new TeamProjectMapping("AnotherTestProject", 456)
                                                           }
                             };

            var configProvider = Substitute.For<IConfigurationProvider>();
            configProvider.Config.ReturnsForAnyArgs(config);
            return configProvider;
        }
    }
}
