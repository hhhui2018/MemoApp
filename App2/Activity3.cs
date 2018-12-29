using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using SQLite;

namespace App2
{
    [Activity(Label = "Activity3")]
    public class Activity3 : Activity
    {
        public static string txt;
        private SQLiteConnection sqliteConn;
        private string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "userinfo.db3");
        private ImageButton back;
        private List<String> list = new List<String>();
        private ArrayAdapter adapter;
        private ListView serachfor;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout1);

            // Create your application here
            back = FindViewById<ImageButton>(Resource.Id.button1);
            serachfor = FindViewById<ListView>(Resource.Id.listView1);
            //返回
            SearchText();
            back.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Activity1));
                this.StartActivity(intent);
            };

            adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, list);
            serachfor.Adapter = adapter;
            //搜索
            serachfor.ItemClick += (s, e) =>
            {
                OnClick(e.Position);
                Intent intent = new Intent(this, typeof(Activity2));
                this.StartActivity(intent);
            };

        }
        private void SearchText()
        {
            sqliteConn = new SQLiteConnection(dbPath);
            var userInfoTable = sqliteConn.Table<UserInfo>();
            string pattern = Intent.GetStringExtra("id");
            foreach (var item in userInfoTable)
            {
                if (Regex.IsMatch(item.Text, pattern))
                    list.Add(item.Text);
            }
            sqliteConn.Close();
        }
        public void OnClick(int position)
        {
            txt = list[position];
        }
    }
}