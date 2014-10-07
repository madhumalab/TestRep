using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Rakuten.ShibuyaPJ.Models
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal ShibuyaDBEntities context;
        internal DbSet<T> dbSet;

        public GenericRepository()
        {
            this.context = new ShibuyaDBEntities();
            dbSet = context.Set<T>();
        }

        public GenericRepository(ShibuyaDBEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T obj)
        {
            dbSet.Add(obj);
        }

        public void Update(T obj)
        {
            dbSet.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(T obj)
        {
            if (context.Entry(obj).State == EntityState.Detached)
            {
                dbSet.Attach(obj);
            }
            dbSet.Remove(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }       
    }
}