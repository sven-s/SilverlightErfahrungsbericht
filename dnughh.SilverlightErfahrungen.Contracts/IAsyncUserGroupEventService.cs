using System;
using System.ServiceModel;

namespace dnughh.SilverlightErfahrungen.Contract
{
    [ServiceContract(Namespace = Information.Namespace.UserGroupEventService, Name = "IUserGroupEventService")]
    public interface IAsyncUserGroupEventService
    {
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetUserGroupEvent(string eventGuid, AsyncCallback callback, object asyncState);
        UserGroupEvent EndGetUserGroupEvent(IAsyncResult result);



        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetUserGroupEventWithFault(string eventGuid, AsyncCallback callback, object asyncState);
        UserGoupEventWithFault EndGetUserGroupEventWithFault(IAsyncResult result);

    }
}