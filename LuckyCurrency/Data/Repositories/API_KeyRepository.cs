using LuckyCurrency.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Data.Repositories
{
    class API_KeyRepository : IDataService<API_Key>
    {
        private ApplicationContext _db;

        public API_KeyRepository(ApplicationContext db)
        {
            _db = db;
        }
        public API_Key Create(API_Key entity)
        {
            return _db.API_Keys.Add(entity);
        }

        public bool Delete(int id)
        {
            bool flag = false;

            var entity = _db.API_Keys.Find(id);
            if (entity != null)
            {
                _db.API_Keys.Remove(entity);
                flag = true;
            }

            return flag;
        }

        public API_Key Get(int id)
        {
            return _db.API_Keys.Find(id);
        }

        public IEnumerable<API_Key> GetAll()
        {
            return _db.API_Keys;
        }

        public API_Key Update(API_Key oldEntity, API_Key entity)
        {
            var dest = _db.API_Keys.Find(oldEntity);

            dest.PublicKey = entity.PublicKey;
            dest.SecretKey = entity.SecretKey;
            dest.Account = entity.Account;

            return dest;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
