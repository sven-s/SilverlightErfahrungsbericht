using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using dnughh.SilverlightErfahrungen.Contract;

namespace dnughh.SilverlightErfahrungen.Proxies
{
    public class UserGroupEventServiceProxy : Proxy<IAsyncUserGroupEventService>
    {
        public UserGroupEventServiceProxy(string configuration) : base(configuration)
        {
        }
    }
}