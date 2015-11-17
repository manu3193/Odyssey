using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace GUIOdyssey.DAL.Persistence.Repositories
{
    public class Repository<T> : IDisposable where T : class
    {
        private bool disposed = false;
        private OdysseyContext context = null;

        protected DbSet<T> DbSet
        {
            get; set;   
        }

        public Repository()
        {
            context = new OdysseyContext();
            DbSet = context.Set<T>();
        }

        public Repository(OdysseyContext context)
        {
            this.context = context;
        }
        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public T Add(T entity)
        {
           return DbSet.Add(entity);
        }

        public T Attach(T entity)
        {
            return DbSet.Attach(entity);
        }

        public virtual void Update(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                context.Dispose();
                disposed = true;
            }
        }
    }

}