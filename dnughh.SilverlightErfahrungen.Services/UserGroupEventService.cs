using System;
using System.ServiceModel.Activation;
using dnughh.SilverlightErfahrungen.Contract;

namespace dnughh.SilverlightErfahrungen.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
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