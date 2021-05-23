using LuckyCurrency.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Data
{
    class UnitOfWork : IDisposable
    {
        private ApplicationContext _db = new ApplicationContext();
        private UserRepository _userRepository;
        private AccountRepository _accountRepository;
        private API_KeyRepository _api_KeyRepository;


        public UserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_db);
                }

                return _userRepository;
            }
        }

        public AccountRepository Accounts
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_db);
                }

                return _accountRepository;
            }
        }

        public API_KeyRepository API_Keys
        {
            get
            {
                if (_api_KeyRepository == null)
                {
                    _api_KeyRepository = new API_KeyRepository(_db);
                }

                return _api_KeyRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
