using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using Newtonsoft.Json;
using Android.Graphics;

namespace QuickChef
{
    class RequestHandler<T> where T : class
    {
        public static T GetDataFromRequest(string url)
        {
            Task<T> task = GetDataFromRequestAsync(url);
            task.Wait();

            return task.Result;
        }

        private static async Task<T> GetDataFromRequestAsync(string url)
        {
            HttpClient httpClient;
            T result = null;

            using (httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;

                    string jsonString = await content.ReadAsStringAsync().ConfigureAwait(false);

                    result = JsonConvert.DeserializeObject<T>(jsonString);
                }
            }
            return result;
        }

        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new System.Net.WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}