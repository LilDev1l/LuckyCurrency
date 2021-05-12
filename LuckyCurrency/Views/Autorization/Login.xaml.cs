using LuckyCurrency.Hasher;
using LuckyCurrency.Models.DB;
using LuckyCurrency.ViewModels.Autorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LuckyCurrency.Views.Autorization
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            /*            using (ApplicationContext db = new ApplicationContext())
                        {
                            User user1 = new User { Login = "login1", Password = PasswordHasher.GetHash("pass1234") };
                            User user2 = new User { Login = "login2", Password = PasswordHasher.GetHash("5678word2") };
                            db.Users.AddRange(new List<User> { user1, user2 });
                            db.SaveChanges();

                            Account account1 = new Account { User = user1 };
                            Account account2 = new Account { User = user2 };
                            db.Accounts.AddRange(new List<Account> { account1, account2 });
                            db.SaveChanges();

                            API_Key api_Key1 = new API_Key { PublicKey = "nMtZ68DOpAAVWrnJba", SecretKey = "5PKGos787uTBxbjZ7Tn7fcfbZanNbZQTecCa", Account = account1 };
                            API_Key api_Key2 = new API_Key { PublicKey = "G2syp3HDJxLmq6lPOI", SecretKey = "Q2MwQQPHzqoxfKjkxFtVYmAYqn7KMF14HKfj", Account = account2 };
                            db.API_Keys.AddRange(new List<API_Key> { api_Key1, api_Key2 });
                            db.SaveChanges();


                            var users = db.Users
                                .Include("Account")
                                .ToList();
                            foreach (var user in users)
                            {
                                Console.WriteLine($"ID {user.Id}; Login {user.Login}; Password {user.Password};");
                                Console.WriteLine($"ID {user.Account.Id};");
                                Console.WriteLine($"PublicKey {user.Account.API_Key.PublicKey}; SecretKey {user.Account.API_Key.SecretKey};");

                            }
                        }*/

            
            
            InitializeComponent();

            LoginViewModel vm = new LoginViewModel();
            DataContext = vm;
            vm.Close = new Action(this.Close);
        }
    }
}
