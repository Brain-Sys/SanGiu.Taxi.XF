﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin;
using Android.Content;
using SanGiu.Taxi.XF.Droid.Implementations;

namespace SanGiu.Taxi.XF.Droid
{
    [Activity(Label = "SanGiu.Taxi.XF", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            FormsMaps.Init(this, bundle);

            var app = new App();
            LoadApplication(app);
        }

        protected override void OnStart()
        {
            base.OnStart();

            var intent = new Intent(this, typeof(AndroidConnectionService));
            StartService(intent);
        }
    }
}

