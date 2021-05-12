using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Models.DB
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConection")
        { }
            
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<API_Key> API_Keys { get; set; }
    }
}
