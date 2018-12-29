
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.IO;
using SQLite;
using System.Collections.Generic;
using System;
using Android.Runtime;
using Android.Views;

namespace App2
{
    [Activity(Label = "记忆", MainLauncher = true)]
    public class Activity1 : Activity
    {
        public static string txt;
        private SQLiteConnection sqliteConn;
        private const string TableName = "UserInfo";
        private string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "userinfo.db3");
        private ImageButton search1;
        private ImageButton add;
        private ListView serachfor;
        private EditText sedit;
        private List<String> list = new List<String>();
        private ArrayAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main_layout);
            sqliteConn = new SQLiteConnection(dbPath);
            var userInfoTable = sqliteConn.GetTableInfo(TableName);
            if (userInfoTable.Count == 0)
            {
                sqliteConn.CreateTable<UserInfo>();
            }
            sqliteConn.Close();
            // Create your application here
            //搜索内容编辑栏
            search1 = FindViewById<ImageButton>(Resource.Id.button1);
            add = FindViewById<ImageButton>(Resource.Id.button2);
            sedit = FindViewById<EditText>(Resource.Id.content);
            serachfor = FindViewById<ListView>(Resource.Id.listView1);
            //添加
            add.Click += delegate
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                this.StartActivity(intent);
            };
            //查询
            search1.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Activity3));
                intent.PutExtra("id", sedit.Text);
                this.StartActivity(intent);
            };
            //搜索栏
            ShowUser();
            adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, list);
            serachfor.Adapter = adapter;
            serachfor.ItemClick += (s, e) =>
            {
                OnClick(e.Position);
                Intent intent = new Intent(this, typeof(Activity2));
                this.StartActivity(intent);
            };
            //长按删除
            serachfor.ItemLongClick += (s, e) =>
            {
                if (e.Position >= 0)
                {
                    txt = list[e.Position];
                    Del();
                }
            };

        }

        //显示代码
        private void ShowUser()
        {
            sqliteConn = new SQLiteConnection(dbPath);
            var userInfoTable = sqliteConn.Table<UserInfo>();
            foreach (var item in userInfoTable)
            {
                if (item != null)
                    list.Add(item.Text);
            }
            sqliteConn.Close();
        }
        //删除
        private void Del()
        {
            sqliteConn = new SQLiteConnection(dbPath);
            var userInfoTable = sqliteConn.GetTableInfo(TableName);
            if (userInfoTable.Count == 0)
            {
                sqliteConn.CreateTable<UserInfo>();
            }
            String a = "DELETE FROM UserInfo WHERE Text = " + "'" + txt + "'";
            sqliteConn.Execute(a);
            list.Remove(txt);
            adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, list);
            serachfor.Adapter = adapter;
            Toast.MakeText(this, "删除成功", ToastLength.Short).Show();
            sqliteConn.Close();
        }
        public void OnClick(int position)
        {
            txt = list[position];
        }
    }
}