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
            var buildEvent = new BuildCompletionEvent { CompletionStatus = "Successfully Completed", TeamProject = "TestProject" };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.ReceivedWithAnyArgs().SendBuildCompletionSuccessNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletionEvent_ShouldNotSendBuildFailedNotification_WhenBuildIsSuccessful()
        {
            var notifier = Substitute.For<INotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent { CompletionStatus = "Successfully Completed", TeamProject = "TestProject" };

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

        private static IConfigurationProvider CreateFakeConfigurationProvider()
        {
            var teamProjectMapping = new TeamProjectMapping
                                         {
                                             TeamProjectName = "TestProject",
                                             HipChatRoomId = 1234
                                         };

            var config = new TfsHipChatConfig
                             {
                                 TeamProjectMappings = new List<TeamProjectMapping> { teamProjectMapping }
                             };

            var configProvider = Substitute.For<IConfigurationProvider>();
            configProvider.Config.ReturnsForAnyArgs(config);
            return configProvider;
        }
    }
}
