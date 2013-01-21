using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.VersionControl.Common;

namespace TfsHipChat
{
    public static class VersionedItemAnalyzer
    {
        public static string GetCommonPath(ICollection<ClientArtifact> versionedItems)
        {
            if (versionedItems.Count == 1)
            {
                return versionedItems.First().ServerItem;
            }

            var commonLength = GetCommonPathLength(versionedItems);
            var commonString = versionedItems.First().ServerItem.Substring(0, commonLength);
            var lastSlashIndex = commonString.LastIndexOf('/');
            
            return (lastSlashIndex > 0) ? commonString.Substring(0, lastSlashIndex) + "/..." : "";
        }

        private static int GetCommonPathLength(ICollection<ClientArtifact> versionedItems)
        {
            var commonPosition = 0;
            var stillCommon = true;

            while (stillCommon)
            {
                char? currentChar = null;

                foreach (var path in versionedItems.Select(versionedItem => versionedItem.ServerItem))
                {
                    if (commonPosition >= path.Length || (currentChar.HasValue && currentChar != path[commonPosition]))
                    {
                        stillCommon = false;
                        break;
                    }
                    
                    if (!currentChar.HasValue)
                    {
                        currentChar = path[commonPosition];
                    }
                }

                if (stillCommon) { commonPosition++; }
            }
            return commonPosition;
        }
    }
}