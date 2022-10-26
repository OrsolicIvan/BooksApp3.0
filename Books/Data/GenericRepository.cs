using Books.Interfaces;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Books.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public IEnumerable<T> GetAllUsers()
        {
            return _context.Set<T>().ToList();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>()
                .Where(expression).AsNoTracking();
        }

        public T GetAuthorById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public T GetBookById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public T GetUserById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
