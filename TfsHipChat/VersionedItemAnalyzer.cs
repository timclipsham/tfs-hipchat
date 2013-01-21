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

            var commonLength = GetCommonStringLength(versionedItems);
            var commonString = versionedItems.First().ServerItem.Substring(0, commonLength);
            var commonPath = commonString.Substring(0, commonString.LastIndexOf('/')) + "/...";
            return commonPath;
        }

        private static int GetCommonStringLength(ICollection<ClientArtifact> versionedItems)
        {
            var commonLength = 0;
            var stillCommon = true;

            while (stillCommon)
            {
                char? currentChar = null;
                var currentPosition = commonLength;

                foreach (var versionedItem in versionedItems)
                {
                    if (!currentChar.HasValue)
                    {
                        currentChar = versionedItem.ServerItem[currentPosition];
                    }
                    else if (currentChar != versionedItem.ServerItem[currentPosition])
                    {
                        stillCommon = false;
                        break;
                    }
                }

                if (stillCommon) commonLength++;
            }
            return commonLength;
        }
    }
}