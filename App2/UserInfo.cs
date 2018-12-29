using SQLite;


namespace App2
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
    }
}