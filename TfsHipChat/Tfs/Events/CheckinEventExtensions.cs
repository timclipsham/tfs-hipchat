using Microsoft.TeamFoundation.VersionControl.Common;
using System.Linq;

namespace TfsHipChat.Tfs.Events
{
    public static class CheckinEventExtensions
    {
        public static string GetChangesetUrl(this CheckinEvent checkinEvent)
        {
            return (from ClientArtifact ca in checkinEvent.Artifacts
                    where ca.Type == "Changeset"
                    select ca.Url).FirstOrDefault();
        }
    }
}
