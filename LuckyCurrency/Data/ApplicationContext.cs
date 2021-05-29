using LuckyCurrency.Data.Model;
using System.Data.Entity;

namespace LuckyCurrency.Data
{
    class ApplicationContext : DbContext
    {
        static ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(new MyContextInitializer());
        }
        public ApplicationContext() : base("DefaultConection")
        { }
            
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<API_Key> API_Keys { get; set; }
    }

    class MyContextInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            string createFunction = @"CREATE FUNCTION [dbo].[GetAPI_Key]
                                (
                                    @login NVARCHAR(MAX),
                                    @password NVARCHAR(MAX)
                                )
                                RETURNS @returntable TABLE
                                (
                                    id        INT,
                                    publicKey NVARCHAR(MAX),
                                    secretKey NVARCHAR(MAX),
                                    accountId INT
                                )
                                AS
                                BEGIN

                                    INSERT @returntable

                                    SELECT*
                                    FROM API_Key api

                                    WHERE api.Id = (SELECT top(1) u.Id
                                            FROM Users u

                                            WHERE u.Login = @login AND

                                                u.Password = @password)
	                                RETURN
                                END";
            db.Database.ExecuteSqlCommand(createFunction);
            //db.SaveChanges();
        }
    }
}


