using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Restaurant.DataAccess.Data.Repository.IRepository
{
    //Generic so it accepts any object, in this case accept several classes
    public interface IRepository<T> where T : class
    {
        //returns which ever object calls it since its generic, based on the ID
        T Get(int id);

        //Get all categories

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
        );

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
        );

        void Add(T entity);

        //Remove based on id OR the full object 
        void Remove(int id);
        void Remove(T entity);
    }
}
