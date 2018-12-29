using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App2
{
    [BroadcastReceiver]
    public class Receiver1 : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            String msg = intent.GetStringExtra("msg");
            Toast.MakeText(context, msg, ToastLength.Short).Show();
        }
    }
}