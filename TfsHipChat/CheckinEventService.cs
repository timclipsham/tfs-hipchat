﻿using System.IO;
using System.Xml.Serialization;

namespace TfsHipChat
{
    class CheckinEventService : IEventService
    {
        public void Notify(string eventXml, string tfsIdentityXml)
        {
            var serializer = new XmlSerializer(typeof(CheckinEventService));
            CheckinEvent checkinEvent;


            using (var reader = new StringReader(eventXml))
            {
                checkinEvent = serializer.Deserialize(reader) as CheckinEvent;  // double check "TryCast"...
            }

            if (checkinEvent == null)
            {
                return;
            }

            // check whether the changeset has policy failures
            if (checkinEvent.PolicyFailures.Count > 0)
            {

            }
        }
    }
}