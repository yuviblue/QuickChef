using Android.Content;
using Android.Net;
using Android.OS;
using Android.Widget;

namespace QuickChef
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    public class NetworkChangeReceiver : BroadcastReceiver
    {
        private bool? networkConnected = null;
        private bool connected = false;

        public NetworkChangeReceiver() { }
        public override void OnReceive(Context context, Intent intent)
        {
            ConnectivityManager conMgr = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);

            if (conMgr.GetNetworkInfo(ConnectivityType.Mobile).GetState() == NetworkInfo.State.Connected
                || conMgr.GetNetworkInfo(ConnectivityType.Wifi).GetState() == NetworkInfo.State.Connected)
            {
                connected = true;
            }

            if (conMgr.GetNetworkInfo(ConnectivityType.Mobile).GetState() != NetworkInfo.State.Connected
                && conMgr.GetNetworkInfo(ConnectivityType.Wifi).GetState() != NetworkInfo.State.Connected)
            {
                connected = false;
            }

            System.Timers.Timer timer = new System.Timers.Timer
            {
                Interval = 100,
                Enabled = true,
                AutoReset = false,
            };
            timer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
            {
                ReportConnectionStatus(context);
            };
            timer.Start();
        }

        private void ReportConnectionStatus(Context context)
        {
            MainActivity.Instance.RunOnUiThread(() =>
            {
                if (networkConnected != connected && networkConnected != null)
                {
                    if (Looper.MyLooper() == null)
                        Looper.Prepare();
                    string message = "Network is " + (connected ? "connected" : "disconnected");
                    Toast.MakeText(context, message, ToastLength.Long).Show();
                }
                networkConnected = connected;
            });
        }
    }
}