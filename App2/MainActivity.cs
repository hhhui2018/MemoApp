using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System.IO;
using SQLite;
using Android.Content;
using Android.Views;

namespace App2
{
    [Activity(Label = "编辑")]
    public class MainActivity : Activity
    {
        private SQLiteConnection sqliteConn;
        private const string TableName = "UserInfo";
        private string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "userinfo.db3");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //编辑栏，输入备忘录
            EditText editText1 = FindViewById<EditText>(Resource.Id.editText1);
            //返回按钮，返回主界面
            ImageButton btnReturn = FindViewById<ImageButton>(Resource.Id.btnReturn);
            string s = Intent.GetStringExtra("content");
            if (s != null) editText1.Text = s;
            btnReturn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Activity1));
                this.StartActivity(intent);
            };

            //添加按钮，添加备忘录
            ImageButton btnAdd = FindViewById<ImageButton>(Resource.Id.btnAdd);
            btnAdd.Click += delegate
            {
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
        }
        private void Register(string text)
        {
            sqliteConn = new SQLiteConnection(dbPath);
            var userInfoTable = sqliteConn.Table<UserInfo>();
            UserInfo model = new UserInfo()
            {

                Text = text
            };
            sqliteConn.Insert(model);
            Toast.MakeText(this, "成功", ToastLength.Short).Show();
            sqliteConn.Close();
        }
    }
}