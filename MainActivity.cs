using System;
using Android.App;
using Android.Widget;

using Android.OS;
using Android.Media;
using System.IO;
using Android.Graphics;
using Java.Net;
using System.Net;
using Android.Content;

namespace ExampleXamarin
{
    [Activity(Label = "ExampleXamarin", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {



        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return imageBitmap;
        }     

      

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);           
           
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            ImageView img = FindViewById<ImageView>(Resource.Id.demoImageView);
            var imageBitmap = GetImageBitmapFromUrl("http://weknowyourdreams.com/images/rose/rose-08.jpg");
            img.SetImageBitmap(imageBitmap);           

            Button buttonFile = FindViewById<Button>(Resource.Id.ButtonFile);
            Button buttonPlayAudio = FindViewById<Button>(Resource.Id.ButtonAudio);
            Button buttonAssetFile = FindViewById<Button>(Resource.Id.ButtonAsset);
            EditText tv = FindViewById<EditText>(Resource.Id.ShowFile);
            Button buttonStartActivity = FindViewById<Button>(Resource.Id.ButtonActivity);

            buttonFile.Click += delegate {
                // The commented following lines read a file from the root of externalstorage
                //var pt = Android.OS.Environment.ExternalStorageDirectory.Path;               
                //var filePath = System.IO.Path.Combine(pt, "text.txt");
                //buttonFile.Text = filePath;         
                //tv.Text= File.ReadAllText(filePath);

                //the following lines open a pdf file based on sd card
                Intent intentUrl = new Intent(Intent.ActionView);
                var pt = Android.OS.Environment.ExternalStorageDirectory.Path;               
                var filePath = System.IO.Path.Combine(pt, "lastcv.pdf");
                Android.Net.Uri pdfFile = Android.Net.Uri.FromFile(new Java.IO.File(filePath));
                intentUrl.SetDataAndType(pdfFile, "application/pdf");
                intentUrl.SetFlags(ActivityFlags.ClearTop);
                StartActivity(intentUrl);


            };

            buttonAssetFile.Click += delegate {
                using (StreamReader sr = new StreamReader(Assets.Open("AboutAssets.txt")))
                {
                    tv.Text = sr.ReadToEnd();
                }
            };

            buttonPlayAudio.Click += delegate
            {
                var pt = Android.OS.Environment.ExternalStorageDirectory.Path;
                var filePath = System.IO.Path.Combine(pt, "test.3gpp");
                MediaPlayer player;
                player = new MediaPlayer();
                player.SetDataSource(filePath);
                player.Prepare();
                player.Start();
                
            };

            buttonStartActivity.Click += delegate {
                StartActivity(typeof(SecondaryActivity));
            };




        }
    }
}

