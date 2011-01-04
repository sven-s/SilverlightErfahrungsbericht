using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Controls;
using dnughh.SilverlightErfahrungen.Contract;
using dnughh.SilverlightErfahrungen.Proxies;

namespace dnughh.SilverlightErfahrungen.SilverlightApp
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPageLoaded;
        }

        void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            
            // Simple Version
            var basicHttpBinding = new BasicHttpBinding();
            var endpointAddress = new EndpointAddress("http://localhost:50738/UserGroupEvent.svc");
            var userGroupEventService = new ChannelFactory<IAsyncUserGroupEventService>(basicHttpBinding, endpointAddress).CreateChannel();

            AsyncCallback asyncCallBack = delegate(IAsyncResult result)
            {
                var response = ((IAsyncUserGroupEventService)result.AsyncState).EndGetUserGroupEvent(result);
                Dispatcher.BeginInvoke(() => SetUserGroupEventData(response));
            };
            userGroupEventService.BeginGetUserGroupEvent("123", asyncCallBack, userGroupEventService);


            // Deluxe Variante mit eigenem Proxy
            var channel = new UserGroupEventServiceProxy("BasicHttpBinding_IAsyncUserGroupEventService").Channel;
            channel.BeginGetUserGroupEvent("123", ProcessResult, channel);


            // Variante mit Faulthandler 
            using (var scope = new OperationContextScope((IContextChannel)channel))
            {
                var messageHeadersElement = OperationContext.Current.OutgoingMessageHeaders;
                messageHeadersElement.Add(MessageHeader.CreateHeader("DoesNotHandleFault", "", true));
                channel.BeginGetUserGroupEventWithFault("123", ProcessResultWithFault, channel);
            }


            //var objectClient = new ObjectClient<IAsyncUserGroupEventService>("BasicHttpBinding_IAsyncUserGroupEventService");
            //objectClient.Begin("GetUserGroupEvent", OnObjectServiceCallback, null, "F488D20B-FC27-4631-9FB9-83AF616AB5A6");
        }


        private void OnObjectServiceCallback(Object sender, ObjectClient<IAsyncUserGroupEventService>.ClientEventArgs ea)
        {
            var data = ea.LoadResult<UserGroupEvent>();
            Dispatcher.BeginInvoke(() => this.TxInfo.Text = data.Description);

        }

        private void ProcessResult(IAsyncResult asyncResult)
        {
            var proxy = new UserGroupEventServiceProxy("BasicHttpBinding_IAsyncUserGroupEventService");

            var response = proxy.Result(asyncResult).EndGetUserGroupEvent(asyncResult);
            Dispatcher.BeginInvoke(() => SetUserGroupEventData(response));
            proxy.Close();
        }

        private void ProcessResultWithFault(IAsyncResult asyncResult)
        {
            var proxy = new UserGroupEventServiceProxy("BasicHttpBinding_IAsyncUserGroupEventService");
           
            var response = proxy.Result(asyncResult).EndGetUserGroupEventWithFault(asyncResult);

            if (response.FaultDetail == null)
            {
                Dispatcher.BeginInvoke(() => SetUserGroupEventData(response));
            }
            else
            {
               // Log.WarnFormat("LoadSerie() {0}", response.FaultDetail.Message);
            }

            proxy.Close();

        }

        private void SetUserGroupEventData(UserGoupEventWithFault data)
        {
            TxInfo.Text = data.Description;
        }

        private void SetUserGroupEventData(UserGroupEvent data)
        {
            TxInfo.Text = data.Description;
        }
    }
}
