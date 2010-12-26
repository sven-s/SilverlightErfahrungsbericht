using System;
using dnughh.SilverlightErfahrungen.Contract;

namespace dnughh.SilverlightErfahrungen.Services
{
    public class UserGroupEventService : IUserGroupEventService
    {
        public UserGroupEvent GetUserGroupEvent(string eventGuid)
        {
            return new UserGroupEvent
                       {
                           Description = "Meine Silverlight Erfahrungen...",
                           Title = "WCF & SL"
                       };
        }
    }
}