﻿using System;
using System.Runtime.Serialization;

namespace dnughh.SilverlightErfahrungen.Contract
{
    [DataContract(Namespace = Information.Namespace.UserGroupEventService)]
    public class UserGroupEvent
    {

        [DataMember]
        public String Guid { get; set; }

        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public String Description { get; set; }

    }
}
