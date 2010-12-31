using System;
using System.Runtime.Serialization;
using dnughh.SilverlightErfahrungen.Helpers;

namespace dnughh.SilverlightErfahrungen.Contract
{
    [DataContract(Namespace = Information.Namespace.UserGroupEventService)]
    public class UserGoupEventWithFault : IContainsFaultDetail
    {
        [DataMember]
        public String Guid { get; set; }

        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public String Description { get; set; }

        [DataMember]
        public FaultDetail FaultDetail { get; set; }
    }
}