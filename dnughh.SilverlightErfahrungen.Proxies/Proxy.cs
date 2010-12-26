using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace dnughh.SilverlightErfahrungen.Proxies
{
    public class Proxy<T> where T : class
    {
        private static readonly object ChannelLock = new object();

        private static ChannelFactory<T> _factory;

        private static T _channel;

        private readonly string _configuration;

        public Proxy(string configuration)
        {
            _configuration = configuration;
        }

        public bool CloseFactoryAfterCall { get; set; }

        public T Channel
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
                            _factory = new ChannelFactory<T>(_configuration);
                        }

                        if (_channel == null)
                        {
                            _channel = _factory.CreateChannel();
                            if (CloseFactoryAfterCall)
                            {
                                ((IClientChannel)_channel).Closed += delegate { _factory.Close(); };
                            }
                        }
                        return _channel;
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

        private T _proxy;

        public T Result(IAsyncResult asyncResult)
        {
            _proxy = ((T)asyncResult.AsyncState);
            return _proxy;
        }

        public void Close()
        {
            ((IClientChannel)_proxy).BeginClose(new AsyncCallback(CloseCallback), _proxy);
        }

        private static void CloseCallback(IAsyncResult asyncResult)
        {
            var proxy = (T)asyncResult.AsyncState;
            ((IClientChannel)proxy).EndClose(asyncResult);
        }
    }
}