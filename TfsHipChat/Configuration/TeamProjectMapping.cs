namespace TfsHipChat.Configuration
{
    public class TeamProjectMapping
    {
        public string TeamProjectName { get; set; }
        public int HipChatRoomId { get; set; }

        public TeamProjectMapping(string teamProjectName, int hipChatRoomId)
        {
            TeamProjectName = teamProjectName;
            HipChatRoomId = hipChatRoomId;
        }
    }
}