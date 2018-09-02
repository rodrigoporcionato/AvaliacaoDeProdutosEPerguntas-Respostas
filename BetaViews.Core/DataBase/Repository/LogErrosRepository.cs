
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetaViews.Core.DataBase.Entitys;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Core.DataBase.ORM;
using System.Linq;
using System.Data.Entity;

namespace BetaViews.Core.DataBase.Repository
{

	public class LogErrosRepository : ServiceBase, ILogErrosRepository
	{
		public LogErros Add(LogErros entity)
		{
			DataContext.Set<LogErros>().Add(entity);
			DataContext.SaveChanges();
			return entity;
		}

		public async Task<LogErros> AddAsync(LogErros entity)
		{
			DataContext.Set<LogErros>().Add(entity);
			await DataContext.SaveChangesAsync();
			return entity;
		}

		public void Delete(LogErros entity)
		{
			DataContext.Set<LogErros>().Remove(entity);
			DataContext.SaveChanges();
		}

		public async Task DeleteAsync(LogErros entity)
		{
			DataContext.Set<LogErros>().Remove(entity);
			await DataContext.SaveChangesAsync();
		}

		public LogErros Edit(LogErros entity, int key)
		{
			if (entity == null)
				return null;

			LogErros existing = DataContext.Set<LogErros>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				DataContext.SaveChanges();
			}
			return existing;
		}

		public async Task<LogErros> EditAsync(LogErros entity, int key)
		{
			if (entity == null)
				return null;

			LogErros existing = DataContext.Set<LogErros>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				await DataContext.SaveChangesAsync();
			}
			return existing;
		}

		public LogErros Find(Expression<Func<LogErros, bool>> predicate)
		{
			return DataContext.Set<LogErros>().SingleOrDefault(predicate);
		}

        public Task<ICollection<LogErros>> FindAllAsync(Expression<Func<LogErros, bool>> match)
        {
            throw new NotImplementedException();
        }

        public async Task<LogErros> FindAsync(Expression<Func<LogErros, bool>> predicate)
		{
			return await DataContext.Set<LogErros>().SingleOrDefaultAsync(predicate);
		}

		public ICollection<LogErros> GetAll()
		{
			return DataContext.Set<LogErros>().ToList();
		}

		public async Task<ICollection<LogErros>> GetAllAsync()
		{
			return await DataContext.Set<LogErros>().ToListAsync();
		}

		public LogErros GetById(int id)
		{
			return DataContext.Set<LogErros>().Find(id);
		}

		public async Task<LogErros> GetByIdAsync(int id)
		{
			return await DataContext.Set<LogErros>().FindAsync(id);
		}
	}
}
