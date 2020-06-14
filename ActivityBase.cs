using Android.Content;
using Android.Support.V7.App;

namespace QuickChef
{
    public class ActivityBase : AppCompatActivity
    {
        private bool showGoodbyeMessage;

        protected override void OnResume()
        {
            base.OnResume();
            showGoodbyeMessage = true;
        }

        public override void StartActivity(Intent intent)
        {
            showGoodbyeMessage = false;
            base.StartActivity(intent);
        }

        public void OnBackPressedOriginal()
        {
            base.OnBackPressed();
        }

        public override void OnBackPressed()
        {
            showGoodbyeMessage = false;
            base.OnBackPressed();
        }
        protected override void OnStop()
        {
            base.OnStop();

            if (showGoodbyeMessage)
            {
                Intent intent = new Intent(this, typeof(ByeService));
                StartService(intent);
            }
        }
    }
}