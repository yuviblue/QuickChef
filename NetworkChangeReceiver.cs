using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace QuickChef
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    public class NetworkChangeReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            ConnectivityManager cm = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeNetwork = cm.ActiveNetworkInfo;
            bool isConnected = (activeNetwork != null && activeNetwork.IsConnectedOrConnecting);

            if (isConnected)
            {
                Toast.MakeText(context, "Connected!", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(context, "No Connection!", ToastLength.Short).Show();
            }

            
        }
    }
}