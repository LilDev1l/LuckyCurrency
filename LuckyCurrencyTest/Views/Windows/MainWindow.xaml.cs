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
using LuckyCurrencyTest.Models;


namespace LuckyCurrencyTest
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            using (UserContext db = new UserContext())
            {
                // создаем два объекта User
                User user1 = new User { Name = "Tom", Age = 33 };
                User user2 = new User { Name = "Sam", Age = 26 };

                // добавляем их в бд
                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

                // получаем объекты из бд и выводим на консоль
                var users = db.Users;
                Console.WriteLine("Список объектов:");
                foreach (User u in users)
                {
                    Console.WriteLine("{0}.{1} - {2}", u.Id, u.Name, u.Age);
                }
            }
            InitializeComponent();
        }
    }
}

