using System;
using System.Runtime.Serialization;

namespace dnughh.SilverlightErfahrungen.Helpers
{
    [DataContract]
    public class FaultDetail
    {
        [DataMember]
        public String Message { get; set; }

        public static FaultDetail CreateFaultDetail(Exception exception)
        {
            return CreateFaultDetail(exception.Message);
        }
        public static FaultDetail CreateFaultDetail(String message)
        {
            return new FaultDetail
            {
                Message = message
            };
        }
    }
}
