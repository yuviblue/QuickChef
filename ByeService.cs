using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

namespace QuickChef
{
    [Service]
    public class ByeService : Service
    {
        ByeHandler h;

        public override void OnCreate()
        {
            base.OnCreate();

            this.h = new ByeHandler(this);
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Thread t = new Thread(Run);
            t.Start();

            return 0;
        }

        private void Run()
        {
            
            Message msg = new Message();
            msg.Arg1 = 0;
            
            Thread.Sleep(3000);
            this.h.SendMessage(msg);

            StopSelf();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}