using LuckyCurrency.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services
{
    class UserRepository : IDataService<User>
    {
        private ApplicationContext _db;

        public UserRepository(ApplicationContext db)
        {
            _db = db;
        }
        public User Create(User entity)
        {
            return _db.Users.Add(entity);
        }

        public bool Delete(int id)
        {
            bool flag = false;

            var entity = _db.Users.Find(id);
            if (entity != null)
            {
                _db.Users.Remove(entity);
                flag = true;
            }

            return flag;
        }

        public User Get(int id)
        {
            return _db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public User Update(User oldEntity, User entity)
        {
            var dest = _db.Users.Find(oldEntity);

            dest.Login = entity.Login;
            dest.Password = entity.Password;
            dest.Account = entity.Account;

            return dest;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
