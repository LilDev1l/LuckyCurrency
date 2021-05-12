using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services
{
    interface IDataService<T>
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        T Create(T entity);

        T Update(T oldEntity, T entity);

        bool Delete(int id);

        void Save();
    }
}
