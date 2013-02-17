using System.Collections.Generic;
using NSubstitute;
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
            var teamProjectMap = new Dictionary<string, int> { { "testproject", 1234 } };
            var notificationHandler = new NotificationHandler(notifier, teamProjectMap);
            var buildEvent = new BuildCompletionEvent { TeamProject = "TestProject" };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.ReceivedWithAnyArgs().SendBuildCompletionFailedNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletionEvent_ShouldSendBuildSuccessNotification_WhenBuildIsSuccessful()
        {
            var notifier = Substitute.For<INotifier>();
            var teamProjectMap = new Dictionary<string, int> { { "testproject", 1234 } };
            var notificationHandler = new NotificationHandler(notifier, teamProjectMap);
            var buildEvent = new BuildCompletionEvent { CompletionStatus = "Successfully Completed", TeamProject = "TestProject" };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.ReceivedWithAnyArgs().SendBuildCompletionSuccessNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletionEvent_ShouldNotSendBuildFailedNotification_WhenBuildIsSuccessful()
        {
            var notifier = Substitute.For<INotifier>();
            var teamProjectMap = new Dictionary<string, int> { { "testproject", 1234 } };
            var notificationHandler = new NotificationHandler(notifier, teamProjectMap);
            var buildEvent = new BuildCompletionEvent { CompletionStatus = "Successfully Completed", TeamProject = "TestProject" };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.DidNotReceiveWithAnyArgs().SendBuildCompletionFailedNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletionEvent_ShouldNotSendBuildSuccessNotification_WhenBuildIsBroken()
        {
            var notifier = Substitute.For<INotifier>();
            var teamProjectMap = new Dictionary<string, int> { { "testproject", 1234 } };
            var notificationHandler = new NotificationHandler(notifier, teamProjectMap);
            var buildEvent = new BuildCompletionEvent { TeamProject = "TestProject" };

            notificationHandler.HandleBuildCompletionEvent(buildEvent);

            notifier.DidNotReceiveWithAnyArgs().SendBuildCompletionSuccessNotification(null, 0);
        }
    }
}
