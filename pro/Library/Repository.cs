using Library.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Library
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private NorthwindEntities db = null;
        private DbSet<T> dbset = null;

        public Repository()
        {
            db = new NorthwindEntities();
            dbset = db.Set<T>();
        }
        public void Create(T entity)
        {
            dbset.Add(entity);
            db.SaveChanges();
        }

        public void Delete(T entity)
        {
            dbset.Remove(entity);
            db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return dbset;
        }

        public T GetById(int id)
        {
            return dbset.Find(id);
        }

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }   
    }
}
