using System;
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
            var buildEvent = new BuildCompletionEvent { TeamProject = "ProjectWithNoMapping" };

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

        [Fact]
        public void HandleBuildCompletion_ShouldNotSendBuildSuccess_WhenNotificationNotSubscribed()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent
                                 {
                                     CompletionStatus = "Successfully Completed",
                                     TeamProject = "ProjectWithOnlyCheckin"
                                 };

            notificationHandler.HandleBuildCompletion(buildEvent);

            notifier.DidNotReceiveWithAnyArgs().SendBuildSuccessNotification(null, 0);
        }

        [Fact]
        public void HandleBuildCompletion_ShouldNotSendBuildFailed_WhenNotificationNotSubscribed()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var buildEvent = new BuildCompletionEvent { TeamProject = "ProjectWithOnlyCheckin" };

            notificationHandler.HandleBuildCompletion(buildEvent);

            notifier.DidNotReceiveWithAnyArgs().SendBuildFailedNotification(null, 0);
        }

        [Fact]
        public void HandleCheckin_ShouldSendNotification_WhenSubscribedImplicitly()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var checkinEvent = new CheckinEvent { TeamProject = "TestProject" };

            notificationHandler.HandleCheckin(checkinEvent);

            notifier.ReceivedWithAnyArgs().SendCheckinNotification(null, 0);
        }

        [Fact]
        public void HandleCheckin_ShouldSendNotification_WhenSubscribedExplicitly()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var checkinEvent = new CheckinEvent { TeamProject = "ProjectWithOnlyCheckin" };

            notificationHandler.HandleCheckin(checkinEvent);

            notifier.ReceivedWithAnyArgs().SendCheckinNotification(null, 0);
        }

        [Fact]
        public void HandleCheckin_ShouldNotSendNotification_WhenNotSubscribed()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var checkinEvent = new CheckinEvent { TeamProject = "ProjectWithOnlyBuild" };

            notificationHandler.HandleCheckin(checkinEvent);

            notifier.DidNotReceiveWithAnyArgs().SendCheckinNotification(null, 0);
        }

        [Fact]
        public void HandleCheckin_ShouldSendNotificationToCorrectRoom_WhenCheckinOccurs()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var checkinEvent = new CheckinEvent { TeamProject = "AnotherTestProject" };

            notificationHandler.HandleCheckin(checkinEvent);

            notifier.Received().SendCheckinNotification(checkinEvent, 456);
        }

        [Fact]
        public void HandleCheckin_ShouldNotSendNotification_WhenMappingNotDefined()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var checkinEvent = new CheckinEvent { TeamProject = "ProjectWithNoMapping" };

            notificationHandler.HandleCheckin(checkinEvent);

            notifier.DidNotReceiveWithAnyArgs().SendCheckinNotification(null, 0);
        }

        [Fact]
        public void HandleWorkItemChanged_ShouldNotSendNotification_WhenWorkItemTypeIsNotTask()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var changedEvent = new WorkItemChangedEvent() { PortfolioProject = "TestProject", ChangeType = "foo"};

            notificationHandler.HandleWorkItemChanged(changedEvent);

            notifier.DidNotReceiveWithAnyArgs().SendTaskChangedRemainingNotification(null, 0);
            notifier.DidNotReceiveWithAnyArgs().SendTaskOwnerChangedNotification(null, 0);
            notifier.DidNotReceiveWithAnyArgs().SendTaskStateChangedNotification(null, 0);
            notifier.DidNotReceiveWithAnyArgs().SendTaskHistoryCommentNotification(null, 0);
        }

        [Fact]
        public void HandleWorkItemChanged_ShouldNotSendNotification_WhenChangeTypeIsNotChange()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var changedEvent = new WorkItemChangedEvent()
                {
                    PortfolioProject = "TestProject", 
                    ChangeType = "foo",
                    CoreFields = { StringFields = new[] { new Field() { Name = "Work Item Type", NewValue = "Product Backlog Item" } } },
                    ChangedFields = { StringFields = new[] { new Field() { Name = "State", NewValue = "In Progress" } } }
                };

            notificationHandler.HandleWorkItemChanged(changedEvent);

            notifier.DidNotReceiveWithAnyArgs().SendTaskChangedRemainingNotification(null, 0);
            notifier.DidNotReceiveWithAnyArgs().SendTaskOwnerChangedNotification(null, 0);
            notifier.DidNotReceiveWithAnyArgs().SendTaskStateChangedNotification(null, 0);
            notifier.DidNotReceiveWithAnyArgs().SendTaskHistoryCommentNotification(null, 0);
        }

        [Fact]
        public void HandleWorkItemChanged_ShouldSendNotification_WhenTaskStateIsChanged()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var changedEvent = new WorkItemChangedEvent
                {
                    PortfolioProject = "TestProject",
                    ChangeType = "Change",
                    CoreFields = { StringFields = new[] {new Field() {Name = "Work Item Type", NewValue = "Task"}} },
                    ChangedFields = { StringFields = new[] {new Field() {Name = "State", NewValue = "In Progress"}} }
                };

            notificationHandler.HandleWorkItemChanged(changedEvent);

            notifier.ReceivedWithAnyArgs().SendTaskStateChangedNotification(null, 0);
        }

        [Fact]
        public void HandleWorkItemChanged_ShouldSendNotification_WhenTaskIsInProgressAndRemainingWorkIsChanged()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var changedEvent = new WorkItemChangedEvent
                {
                    PortfolioProject = "TestProject",
                    ChangeType = "Change",
                    CoreFields =
                        {
                            StringFields = new[]
                                {
                                    new Field() {Name = "Work Item Type", NewValue = "Task"},
                                    new Field() {Name = "State", NewValue = "In Progress"}
                                }
                        },
                    ChangedFields = {IntegerFields = new[] {new Field() {Name = "Remaining Work", NewValue = "3"}}}
                };

            notificationHandler.HandleWorkItemChanged(changedEvent);

            notifier.ReceivedWithAnyArgs().SendTaskChangedRemainingNotification(null, 0);
        }

        [Fact]
        public void HandleWorkItemChanged_ShouldSendNotification_WhenTaskIsInProgressAndAssignedToChanged()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var changedEvent = new WorkItemChangedEvent
            {
                PortfolioProject = "TestProject",
                ChangeType = "Change",
                CoreFields =
                {
                    StringFields = new[]
                                {
                                    new Field() {Name = "Work Item Type", NewValue = "Task"},
                                    new Field() {Name = "State", NewValue = "In Progress"}
                                }
                },
                ChangedFields = { StringFields = new[] { new Field { Name = "Assigned To", NewValue = "Brock Lee" } } }
            };

            notificationHandler.HandleWorkItemChanged(changedEvent);

            notifier.ReceivedWithAnyArgs().SendTaskOwnerChangedNotification(null, 0);
        }

        [Fact]
        public void HandleWorkItemChanged_ShouldSendNotification_WhenTaskHistoryChanged()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var changedEvent = new WorkItemChangedEvent
            {
                PortfolioProject = "TestProject",
                ChangeType = "Change",
                CoreFields =
                {
                    StringFields = new[]
                                {
                                    new Field() {Name = "Work Item Type", NewValue = "Task"},
                                    new Field() {Name = "State", NewValue = "In Progress"}
                                }
                },
                TextFields = new [] { new TextField() { Name = "History", Value = "The rest is" } } 
            };

            notificationHandler.HandleWorkItemChanged(changedEvent);

            notifier.ReceivedWithAnyArgs().SendTaskHistoryCommentNotification(null, 0);
        }

        [Fact]
        public void HandleWorkItemChanged_ShouldNotSendNotification_WhenTaskHistoryChangedIsAutomaticChangesetComment()
        {
            var notifier = Substitute.For<IHipChatNotifier>();
            var configProvider = CreateFakeConfigurationProvider();
            var notificationHandler = new NotificationHandler(notifier, configProvider);
            var changedEvent = new WorkItemChangedEvent
            {
                PortfolioProject = "TestProject",
                ChangeType = "Change",
                CoreFields =
                {
                    StringFields = new[]
                                {
                                    new Field() {Name = "Work Item Type", NewValue = "Task"},
                                    new Field() {Name = "State", NewValue = "In Progress"}
                                }
                },
                TextFields = new[] { new TextField() { Name = "History", Value = "Associated with changeset 123456" } }
            };

            notificationHandler.HandleWorkItemChanged(changedEvent);

            notifier.DidNotReceiveWithAnyArgs().SendTaskHistoryCommentNotification(null, 0);
        }

        private static IConfigurationProvider CreateFakeConfigurationProvider()
        {
            var config = new TfsHipChatConfig
                             {
                                 TeamProjectMappings = new List<TeamProjectMapping>
                                                           {
                                                               new TeamProjectMapping("TestProject", 123),
                                                               new TeamProjectMapping("AnotherTestProject", 456),
                                                               new TeamProjectMapping("ProjectWithOnlyCheckin", 789)
                                                                   {
                                                                       Notifications = new List<Notification> { Notification.Checkin }
                                                                   },
                                                               new TeamProjectMapping("ProjectWithOnlyBuild", 789)
                                                                   {
                                                                       Notifications = new List<Notification> { Notification.BuildCompletionSuccess }
                                                                   }
                                                           }
                             };

            var configProvider = Substitute.For<IConfigurationProvider>();
            configProvider.Config.ReturnsForAnyArgs(config);
            return configProvider;
        }
    }
}
