using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BetaViews.Core.Framework
{
    public interface IGenericRepository<T> where T : class
    {

        ICollection<T> GetAll();        
        T Find(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        T Add(T entity);
        void Delete(T entity);
        T Edit(T entity, int key);

        Task<ICollection<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> EditAsync(T entity, int key);        

    }
}
