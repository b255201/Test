using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Library.Dao;

namespace Library
{
    public interface IRepository<T>
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
