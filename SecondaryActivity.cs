using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Webkit;
using SQLite;
using ExampleXamarin.model;
using System.IO;
using Android.Content;

namespace ExampleXamarin
{
    [Activity(Label = "SecondaryActivity")]
   // public class SecondaryActivity : ListActivity
        public class SecondaryActivity : Activity
    {
        // string[] items;

        private class Callback : WebViewClient
        {    
                public override bool ShouldOverrideUrlLoading(WebView view, String url) {
                view.LoadUrl(url);
                return (false);
            }
        }


        private string createDatabase(string path)
        {
            try
            {
                var connection = new SQLiteConnection(path);
                connection.CreateTable<Person>();


                Toast.MakeText(this, "Database created", ToastLength.Long).Show();
                return "Database created";
            }
            catch (SQLiteException ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                return ex.Message;
            }
        }




        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create an Sqlitedatabase
            var pt = Android.OS.Environment.ExternalStorageDirectory.Path;
            var filePath = System.IO.Path.Combine(pt, "db.db");
            createDatabase(filePath);
            // This is a webcontainer which can be used to host a web page
            SetContentView(Resource.Layout.Secondary);
            WebView webView = FindViewById<WebView>(Resource.Id.webviewtest);
            webView.SetWebViewClient(new Callback());
            webView.Settings.JavaScriptEnabled = true;
            webView.SetWebChromeClient(new WebChromeClient());
            webView.Settings.DomStorageEnabled = true;
            webView.LoadUrl("http://dmm888.com");

        }      



    }
}