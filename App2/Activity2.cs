using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System.IO;
using SQLite;
using Android.Content;
using Android.Views;
using System;
using System.Text;
using System.Collections.Generic;


namespace App2
{
    [Activity(Label = "编辑")]
    public class Activity2 : Activity
    {
        public int id;
        private SQLiteConnection sqliteConn;
        private const string TableName = "UserInfo";
        private string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "userinfo.db3");
        EditText editText1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            sqliteConn = new SQLiteConnection(dbPath);
            //编辑栏，输入备忘录
            editText1 = FindViewById<EditText>(Resource.Id.editText1);
            if(Activity1.txt != null)
                editText1.Text = Activity1.txt;
            else
                editText1.Text = Activity3.txt;
            if (Activity3.txt != null)
                editText1.Text = Activity3.txt;
            else
                editText1.Text = Activity1.txt;
            //返回按钮，返回主界面
            ImageButton btnReturn = FindViewById<ImageButton>(Resource.Id.btnReturn);
            btnReturn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Activity1));
                this.StartActivity(intent);

            };

            //添加按钮，添加备忘录
            ImageButton btnAdd = FindViewById<ImageButton>(Resource.Id.btnAdd);
            btnAdd.Click += delegate
            {

                sqliteConn = new SQLiteConnection(dbPath);
                var text = FindViewById<EditText>(Resource.Id.editText1).Text;
                Register(text);
                Intent intent = new Intent(this, typeof(Activity1));
                this.StartActivity(intent);

            };
            //闹钟按钮，定时提醒
            ImageButton btnAlarm = FindViewById<ImageButton>(Resource.Id.btnAlarm);
            btnAlarm.Click += delegate
            {
                Intent intent = new Intent(this, typeof(AlarmActivity));
                intent.PutExtra("id", editText1.Text);
                this.StartActivity(intent);
            };
            sqliteConn.Close();
        }
        private void Register(string text)
        {

            sqliteConn = new SQLiteConnection(dbPath);
            var userInfoTable1 = sqliteConn.GetTableInfo(TableName);
            if (userInfoTable1.Count == 0)
            {
                sqliteConn.CreateTable<UserInfo>();
            }
            String a = "UPDATE UserInfo SET Text=" + "'" + text + "'" + " WHERE Text = " + "'" + Activity1.txt + "'";
            sqliteConn.Execute(a);
            Toast.MakeText(this, "成功", ToastLength.Short).Show();
            sqliteConn.Close();
        }
    }
}