using System.ServiceModel;
using System.ServiceModel.Channels;
using dnughh.SilverlightErfahrungen.Contract;

namespace dnughh.SilverlightErfahrungen.Proxies
{
    public class UserGroupEventServiceProxy
    {
        private static object ChannelLock = new object();

        private static ChannelFactory<IAsyncUserGroupEventService> _factory;

        private static IAsyncUserGroupEventService _channel;

        public IAsyncUserGroupEventService Channel
        {
            get
            {
                var localChannel = _channel;
                if (localChannel == null)
                {
                    lock (ChannelLock)
                    {
                        if (_factory == null)
                        {
                            _factory = new ChannelFactory<IAsyncUserGroupEventService>("BasicHttpBinding_IAsyncUserGroupEventService");
                        }

                        return _channel ?? (_channel = _factory.CreateChannel());
                    }
                }
                return localChannel;
            }

            set
            {
                lock (ChannelLock)
                {
                    if (((IChannel)_channel).State != CommunicationState.Opened)
                    {
                        ((IChannel)_channel).Abort();
                        _channel = null;

                    }

                    if (_factory.State != CommunicationState.Opened)
                    {
                        _factory.Abort();
                        _factory = null;
                    }
                }
            }
        }

    }
}