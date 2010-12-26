//+ Copyright © Jampad Technology, Inc. 2008
//++ Lead Architect: David Betz [MVP] <dfb/davidbetz/net>

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace dnughh.SilverlightErfahrungen.Proxies
{
    public class ObjectClient<TServiceContract> where TServiceContract : class
    {
        //- @ClientEventArgs -//
        public class ClientEventArgs : EventArgs
        {
            //- @Object -//
            public Object Object { get; set; }

            //- @UserState -//
            public Object UserState { get; set; }

            //+
            //- @LoadResult -//
            public TResult LoadResult<TResult>()
            {
                if (this.Object is TResult)
                {
                    return (TResult)this.Object;
                }
                //+
                return default(TResult);
            }
        }

        //- $ObjectClientState -//
        private class ObjectClientState
        {
            //- @Callback -//
            public EventHandler<ClientEventArgs> Callback { get; set; }

            //- @MethodName -//
            public String MethodName { get; set; }

            //- @UserState -//
            public Object UserState { get; set; }
        }

        //+
        //- $Channel -//
        private TServiceContract Channel { get; set; }

        //- $ContractType -//
        private Type ContractType { get; set; }

        //+
        //- @Ctor
        private ObjectClient()
        {
            ContractType = typeof(TServiceContract);
        }
        public ObjectClient(Binding binding, EndpointAddress address)
            : this()
        {
            Channel = new ChannelFactory<TServiceContract>(binding, address).CreateChannel();
        }
        public ObjectClient(String endpointConfigurationName)
            : this()
        {
            Channel = new ChannelFactory<TServiceContract>(endpointConfigurationName).CreateChannel();
        }

        //+
        //- @Begin -//
        public void Begin(String methodName, Object state, params Object[] parameterArray)
        {
            Begin(methodName, null, state, parameterArray);
        }
        public void Begin(String methodName, EventHandler<ClientEventArgs> callback, Object state, params Object[] parameterArray)
        {
            if (parameterArray != null)
            {
                Array.Resize<Object>(ref parameterArray, parameterArray.Length + 2);
            }
            else
            {
                parameterArray = new Object[2];
            }
            parameterArray[parameterArray.Length - 1] = new ObjectClientState
            {
                Callback = callback,
                MethodName = methodName,
                UserState = state
            };
            parameterArray[parameterArray.Length - 2] = new AsyncCallback(OnCallback);
            ContractType.InvokeMember("Begin" + methodName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public, null, Channel, parameterArray);
        }

        //- $OnCallback -//
        private void OnCallback(IAsyncResult result)
        {
            ObjectClientState state = result.AsyncState as ObjectClientState;
            if (state == null)
            {
                return;
            }
            Object obj = ContractType.InvokeMember("End" + state.MethodName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public, null, Channel, new Object[] { result });
            if (state.Callback != null)
            {
                state.Callback(this, new ClientEventArgs
                {
                    Object = obj,
                    UserState = state.UserState
                });
            }
        }
    }
}
