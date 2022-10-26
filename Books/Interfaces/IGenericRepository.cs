using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Books.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetAuthorById(int id);
        T GetUserById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllUsers();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
