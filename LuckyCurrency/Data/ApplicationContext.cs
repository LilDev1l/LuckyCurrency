using LuckyCurrency.Data.Model;
using System.Data.Entity;

namespace LuckyCurrency.Data
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
