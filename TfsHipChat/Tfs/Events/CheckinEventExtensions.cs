using System.Collections.Generic;
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

        public static ICollection<ClientArtifact> GetVersionedItems(this CheckinEvent checkinEvent)
        {
            return (from ClientArtifact ca in checkinEvent.Artifacts
                    where ca.Type == "VersionedItem"
                    select ca).ToList();
        }
    }
}
