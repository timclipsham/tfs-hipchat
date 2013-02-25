using System.Collections.Generic;

namespace TfsHipChat.Configuration
{
    public class TeamProjectMapping
    {
        /// <summary>
        /// Name of the TFS team project that events will be captured from.
        /// </summary>
        public string TeamProjectName { get; set; }
        
        /// <summary>
        /// The HipChat room the events will be sent to.
        /// </summary>
        /// <remarks>Can easily be obtained through the URL of the HipChat web interface</remarks>
        public int HipChatRoomId { get; set; }
        
        /// <summary>
        /// Notifications this mapping is subscribed to.
        /// </summary>
        /// <remarks>When null, it is subscribed to all events.</remarks>
        public List<Notification> Notifications { get; set; }

        public TeamProjectMapping(string teamProjectName, int hipChatRoomId)
        {
            TeamProjectName = teamProjectName;
            HipChatRoomId = hipChatRoomId;
        }
    }

    public enum Notification
    {
        Checkin,
        BuildCompletionSuccess,
        BuildCompletionFailure
    }
}