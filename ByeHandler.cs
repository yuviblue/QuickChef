using Android.Content;
using Android.OS;
using Android.Widget;

namespace QuickChef
{
    class ByeHandler : Handler
    {
        private Context context;

        public ByeHandler(Context context)
        {
            this.context = context;
        }

        public override void HandleMessage(Message msg)
        {
            if (msg.Arg1 == 0)
                Toast.MakeText(this.context, "See you next time!", ToastLength.Long).Show();
        }
    }
}