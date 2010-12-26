using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using dnughh.SilverlightErfahrungen.Contract;
using dnughh.SilverlightErfahrungen.Proxies;

namespace dnughh.SilverlightErfahrungen.SilverlightApp
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            AsyncCallback asyncCallBack = delegate(IAsyncResult result)
            {
                var response = ((IAsyncUserGroupEventService)result.AsyncState).EndGetUserGroupEvent(result);
                Dispatcher.BeginInvoke(() => SetUserGroupEventData(response));

            };
            var channel = new UserGroupEventServiceProxy().Channel;

            channel.BeginGetUserGroupEvent("123", asyncCallBack, channel);
            */
              
            var objectClient = new ObjectClient<IAsyncUserGroupEventService>("BasicHttpBinding_IAsyncUserGroupEventService");
            objectClient.Begin("GetUserGroupEvent", OnObjectServiceCallback, null, "F488D20B-FC27-4631-9FB9-83AF616AB5A6");



        }


        private void OnObjectServiceCallback(Object sender, ObjectClient<IAsyncUserGroupEventService>.ClientEventArgs ea)
        {
            var data = ea.LoadResult<UserGroupEvent>();
            Dispatcher.BeginInvoke(() => this.TxInfo.Text = data.Description);
   
        }
        /*
        private void SetUserGroupEventData(UserGroupEvent data)
        {
            this.TxInfo.Text = data.Description;
        }*/
    }
}
