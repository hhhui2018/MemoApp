using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using SQLite;
using Android.Media;
using System.IO;

namespace App2
{
    [Activity(Label = "@string/app_name")]
    public class AlarmActivity : Activity
    {
        private const String BC_ACTION = "com.ex.action.BC_ACTION";
        DateTime currentTime = DateTime.Now;
        int alarm_hour;     //用于接收设定的闹铃小时数
        int alarm_minute;   //用于接收设定的闹铃分钟数
        int alarm_date;   //用于接收设定的闹铃天数
        int alarm_year;   //用于接收设定的闹铃年份
        int alarm_month;   //用于接收设定的闹铃月份
        string alarm_time;  //用于显示闹铃时间
        private List<String> time = new List<String>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.alarm);
            Button btn1 = FindViewById<Button>(Resource.Id.btnYAdd);
            Button btn2 = FindViewById<Button>(Resource.Id.btnMAdd);
            Button btn3 = FindViewById<Button>(Resource.Id.btnDAdd);
            Button btn4 = FindViewById<Button>(Resource.Id.btnHAdd);
            Button btn5 = FindViewById<Button>(Resource.Id.btnMinAdd);
            Button btn6 = FindViewById<Button>(Resource.Id.btnYear);
            Button btn7 = FindViewById<Button>(Resource.Id.btnMonth);
            Button btn8 = FindViewById<Button>(Resource.Id.btnDate);
            Button btn9 = FindViewById<Button>(Resource.Id.btnHour);
            Button btn10 = FindViewById<Button>(Resource.Id.btnMinute);
            Button btn11 = FindViewById<Button>(Resource.Id.btnYCut);
            Button btn12 = FindViewById<Button>(Resource.Id.btnMCut);
            Button btn13 = FindViewById<Button>(Resource.Id.btnDCut);
            Button btn14 = FindViewById<Button>(Resource.Id.btnHCut);
            Button btn15 = FindViewById<Button>(Resource.Id.btnMinCut);
            ImageButton btnN = FindViewById<ImageButton>(Resource.Id.btnN);
            ImageButton btnY = FindViewById<ImageButton>(Resource.Id.btnY);
            btn6.Clickable = false;
            btn6.Text = currentTime.Year.ToString();
            btn7.Clickable = false;
            btn7.Text = currentTime.Month.ToString();
            btn8.Clickable = false;
            btn8.Text = currentTime.Day.ToString();
            btn9.Clickable = false;
            btn9.Text = currentTime.Hour.ToString();
            btn10.Clickable = false;
            btn10.Text = currentTime.Minute.ToString();
            string s = Intent.GetStringExtra("id");
            btnN.Click += delegate
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("content", s);
                this.StartActivity(intent);
            };
            btnY.Click += delegate
            {
                currentTime = DateTime.Now;
                DateTime dt = new DateTime(Convert.ToInt32(btn6.Text), Convert.ToInt32(btn7.Text), Convert.ToInt32(btn8.Text), Convert.ToInt32(btn9.Text), Convert.ToInt32(btn10.Text), 0);
                AlarmManager am = (AlarmManager)GetSystemService(AlarmService);
                Intent intent = new Intent(this, typeof(Receiver1));
                intent.SetAction(BC_ACTION);
                intent.PutExtra("msg", "您的日程时间到了！");
                long triggerTime = SystemClock.ElapsedRealtime() + (long)(dt - DateTime.Now).TotalMilliseconds;
                PendingIntent pi = PendingIntent.GetBroadcast(this, 0, intent, 0);
                am.Set(AlarmType.ElapsedRealtimeWakeup, triggerTime, pi);
                Intent intent1 = new Intent(this, typeof(MainActivity));
                intent1.PutExtra("content", s);
                this.StartActivity(intent1);
            };
            btn1.Click += delegate
            {
                btn6.Text = (Convert.ToInt32(btn6.Text) + 1).ToString();
            };
            btn2.Click += delegate
            {
                if (Convert.ToInt32(btn7.Text) == 12)
                {
                    btn7.Text = "1";
                }
                else
                    btn7.Text = (Convert.ToInt32(btn7.Text) + 1).ToString();
            };
            btn3.Click += delegate
            {
                if (IsLeapYear(Convert.ToInt32(btn6.Text)))
                {
                    if (IsSmallM(Convert.ToInt32(btn7.Text)))
                    {
                        if (Convert.ToInt32(btn8.Text) == 30)
                            btn8.Text = "1";
                        else
                            btn8.Text = (Convert.ToInt32(btn8.Text) + 1).ToString();
                    }
                    else if (Convert.ToInt32(btn7.Text) == 2)
                    {
                        if (Convert.ToInt32(btn8.Text) == 29)
                            btn8.Text = "1";
                        else
                            btn8.Text = (Convert.ToInt32(btn8.Text) + 1).ToString();
                    }
                    else
                    {
                        if (Convert.ToInt32(btn8.Text) == 31)
                            btn8.Text = "1";
                        else
                            btn8.Text = (Convert.ToInt32(btn8.Text) + 1).ToString();
                    }
                }
                else
                {
                    if (IsSmallM(Convert.ToInt32(btn7.Text)))
                    {
                        if (Convert.ToInt32(btn8.Text) == 30)
                            btn8.Text = "1";
                        else
                            btn8.Text = (Convert.ToInt32(btn8.Text) + 1).ToString();
                    }
                    else if (Convert.ToInt32(btn7.Text) == 2)
                    {
                        if (Convert.ToInt32(btn8.Text) == 28)
                            btn8.Text = "1";
                        else
                            btn8.Text = (Convert.ToInt32(btn8.Text) + 1).ToString();
                    }
                    else
                    {
                        if (Convert.ToInt32(btn8.Text) == 31)
                            btn8.Text = "1";
                        else
                            btn8.Text = (Convert.ToInt32(btn8.Text) + 1).ToString();
                    }
                }
            };
            btn4.Click += delegate
            {
                if (Convert.ToInt32(btn9.Text) == 23)
                {
                    btn9.Text = "0";
                }
                else
                    btn9.Text = (Convert.ToInt32(btn9.Text) + 1).ToString();
            };
            btn5.Click += delegate
            {
                if (Convert.ToInt32(btn10.Text) == 59)
                {
                    btn10.Text = "0";
                }
                btn10.Text = (Convert.ToInt32(btn10.Text) + 1).ToString();
            };
            btn11.Click += delegate
            {
                if (Convert.ToInt32(btn6.Text) == currentTime.Year)
                    btn6.Text = currentTime.Year.ToString();
                else
                    btn6.Text = (Convert.ToInt32(btn6.Text) - 1).ToString();
            };
            btn12.Click += delegate
            {
                if (Convert.ToInt32(btn7.Text) == 1)
                    btn7.Text = "12";
                else
                    btn7.Text = (Convert.ToInt32(btn7.Text) - 1).ToString();
            };
            btn13.Click += delegate
            {
                if (IsLeapYear(Convert.ToInt32(btn6.Text)))
                {
                    if (IsSmallM(Convert.ToInt32(btn7.Text)))
                    {
                        if (Convert.ToInt32(btn8.Text) == 1)
                            btn8.Text = "30";
                        else
                            btn8.Text = (Convert.ToInt32(btn8.Text) - 1).ToString();
                    }
                    else if (Convert.ToInt32(btn7.Text) == 2)
                    {
                        if (Convert.ToInt32(btn8.Text) == 1)
                            btn8.Text = "29";
                        else
                            btn8.Text = (Convert.ToInt32(btn8.Text) - 1).ToString();
                    }
                    else
                    {
                        if (Convert.ToInt32(btn8.Text) == 1)
                            btn8.Text = "31";
                        else
                            btn8.Text = (Convert.ToInt32(btn8.Text) - 1).ToString();
                    }
                }
                else
                {
                    if (IsSmallM(Convert.ToInt32(btn7.Text)))
                    {
                        if (Convert.ToInt32(btn8.Text) == 1)
                            btn8.Text = "30";
                        else
                            btn8.Text = (Convert.ToInt32(btn8.Text) - 1).ToString();
                    }
                    else if (Convert.ToInt32(btn7.Text) == 2)
                    {
                        if (Convert.ToInt32(btn8.Text) == 1)
                            btn8.Text = "28";
                        else
                            btn8.Text = (Convert.ToInt32(btn8.Text) - 1).ToString();
                    }
                    else
                    {
                        if (Convert.ToInt32(btn8.Text) == 1)
                            btn8.Text = "31";
                        else
                            btn8.Text = (Convert.ToInt32(btn8.Text) - 1).ToString();
                    }
                }
            };
            btn14.Click += delegate
            {
                if (Convert.ToInt32(btn9.Text) == 0)
                {
                    btn9.Text = "23";
                }
                else
                { btn9.Text = (Convert.ToInt32(btn9.Text) - 1).ToString(); }
            };
            btn15.Click += delegate
            {
                if (Convert.ToInt32(btn10.Text) == 0)
                {
                    btn10.Text = "59";
                }
                else
                    btn10.Text = (Convert.ToInt32(btn10.Text) - 1).ToString();
            };
        }
        bool IsLeapYear(int a)
        {
            return ((a % 4 == 0 && a % 100 != 0) || a % 400 == 0);
        }
        bool IsSmallM(int a)
        {
            return (a == 4 || a == 6 || a == 9 || a == 11);
        }

    }
    }