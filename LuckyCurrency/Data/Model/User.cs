namespace LuckyCurrency.Data.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public Account Account { get; set; }
    }
}
