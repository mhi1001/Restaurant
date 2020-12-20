using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Data.Repository.IRepository;

namespace Restaurant.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //Database
        private DbContext Context;
        private DbSet<T> dbSet;
        public Repository(DbContext context)
        {
            //Dependecy injection to get database connections 
            Context = context;
            dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            //Add whatever object into the db
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
            //searches the object through the database and returns it based on the ID
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Func<System.Linq.IQueryable<T>, System.Linq.IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
            Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
