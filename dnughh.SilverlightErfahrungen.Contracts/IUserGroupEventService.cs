using System.ServiceModel;
using dnughh.SilverlightErfahrungen.Helpers;

namespace dnughh.SilverlightErfahrungen.Contract
{
    [ServiceContract(Namespace = Information.Namespace.UserGroupEventService)]
    public interface IUserGroupEventService
    {

        [OperationContract]
        UserGroupEvent GetUserGroupEvent(string eventGuid);


        #if NETCLR
        [FaultHandlerOperationBehavior]
#endif
        [OperationContract]
        UserGoupEventWithFault GetUserGroupEventWithFault(string eventGuid);

    }
}