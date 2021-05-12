using System.Windows;
using System.Collections.ObjectModel;
using FancyCandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using Newtonsoft.Json.Linq;
using IO.Swagger.Model;
using IO.Swagger.Api;
using LuckyCurrency.Models;
using LuckyCurrency.Models.DB;
using System.Linq;
using LuckyCurrency.Hasher;

namespace LuckyCurrency
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            /*using (ApplicationContext db = new ApplicationContext())
            {
                User user1 = new User { Login = "login1", Password = PasswordHasher.GetHash("pass1234") };
                User user2 = new User { Login = "login2", Password = PasswordHasher.GetHash("5678word2") };
                db.Users.AddRange(new List<User> { user1, user2 });
                db.SaveChanges();

                Account account1 = new Account { User = user1 };
                Account account2 = new Account { User = user2 };
                db.Accounts.AddRange(new List<Account> { account1, account2 });
                db.SaveChanges();

                API_Key api_Key1 = new API_Key { PublicKey = "QIqhha0rxn2MsE9RVy", SecretKey = "DdG6XxKhIbchVRvEFmFOyazlyRCnqESGA1Pa", Account = account1 };
                API_Key api_Key2 = new API_Key { PublicKey = "QIqhha0rxn2MsE9RVy", SecretKey = "DdG6XxKhIbchVRvEFmFOyazlyRCnqESGA1Pa", Account = account2 };
                db.API_Keys.AddRange(new List<API_Key> { api_Key1, api_Key2 });
                db.SaveChanges();


                var users = db.Users
                    .Include("Account")
                    .Include("API_Key")
                    .ToList();
                foreach (var user in users)
                {
                    Console.WriteLine($"ID {user.Id}; Login {user.Login}; Password {user.Password};");
                    Console.WriteLine($"ID {user.Account.Id};");
                    Console.WriteLine($"PublicKey {user.Account.API_Key.PublicKey}; SecretKey {user.Account.API_Key.SecretKey};");

                }
            }*/

            InitializeComponent();
        }
    }
}

