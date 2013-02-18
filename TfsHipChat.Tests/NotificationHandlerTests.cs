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
        public void HandleBuildCompletion_ShouldSendBuildFailed_WhenBuildBroken()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent { TeamProject = "TestProject" };

            notificationHandler.HandleBuildCompletion(buildEvent);

            notifier.ReceivedWithAnyArgs().SendBuildFailedNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletion_ShouldSendBuildSuccess_WhenBuildSuccessful()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent
                                 {
                                     CompletionStatus = "Successfully Completed",
                                     TeamProject = "TestProject"
                                 };

            notificationHandler.HandleBuildCompletion(buildEvent);

            notifier.ReceivedWithAnyArgs().SendBuildSuccessNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletion_ShouldNotSendBuildFailed_WhenBuildSuccessful()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent
                                 {
                                     CompletionStatus = "Successfully Completed",
                                     TeamProject = "TestProject"
                                 };

            notificationHandler.HandleBuildCompletion(buildEvent);

            notifier.DidNotReceiveWithAnyArgs().SendBuildFailedNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletion_ShouldNotSendBuildSuccess_WhenBuildBroken()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent { TeamProject = "TestProject" };

            notificationHandler.HandleBuildCompletion(buildEvent);

            notifier.DidNotReceiveWithAnyArgs().SendBuildSuccessNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletion_ShouldSendBuildSuccessToCorrectRoom_WhenBuildSuccessful()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent
                                 {
                                     CompletionStatus = "Successfully Completed",
                                     TeamProject = "AnotherTestProject"
                                 };

            notificationHandler.HandleBuildCompletion(buildEvent);

            notifier.Received().SendBuildSuccessNotification(buildEvent, 456);
        }

        [Fact]
        public void HandleBuildCompletion_ShouldSendBuildFailedToCorrectRoom_WhenBuildBroken()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent { TeamProject = "AnotherTestProject" };

            notificationHandler.HandleBuildCompletion(buildEvent);

            notifier.Received().SendBuildFailedNotification(buildEvent, 456);
        }

        [Fact]
        public void HandleBuildCompletion_ShouldNotSendBuildFailed_WhenBuildBrokenAndMappingNotDefined()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent{ TeamProject = "ProjectWithNoMapping" };

            notificationHandler.HandleBuildCompletion(buildEvent);

            notifier.DidNotReceiveWithAnyArgs().SendBuildFailedNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletion_ShouldNotSendBuildSuccess_WhenBuildSuccessfulAndMappingNotDefined()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent
                                 {
                                     CompletionStatus = "Successfully Completed",
                                     TeamProject = "ProjectWithNoMapping"
                                 };

            notificationHandler.HandleBuildCompletion(buildEvent);

            notifier.DidNotReceiveWithAnyArgs().SendBuildSuccessNotification(null, 0);
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
