using System.ServiceModel;

namespace dnughh.SilverlightErfahrungen.Contract
{
    [ServiceContract(Namespace = Information.Namespace.UserGroupEventService)]
    public interface IUserGroupEventService
    {

        [OperationContract]
        UserGroupEvent GetUserGroupEvent(string eventGuid);

    }
}