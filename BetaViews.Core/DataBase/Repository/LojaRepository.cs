
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

	public class LojaRepository : ServiceBase, ILojaRepository
	{
		public Loja Add(Loja entity)
		{
			DataContext.Set<Loja>().Add(entity);
			DataContext.SaveChanges();
			return entity;
		}

		public async Task<Loja> AddAsync(Loja entity)
		{
			DataContext.Set<Loja>().Add(entity);
			await DataContext.SaveChangesAsync();
			return entity;
		}

		public void Delete(Loja entity)
		{
			DataContext.Set<Loja>().Remove(entity);
			DataContext.SaveChanges();
		}

		public async Task DeleteAsync(Loja entity)
		{
			DataContext.Set<Loja>().Remove(entity);
			await DataContext.SaveChangesAsync();
		}

		public Loja Edit(Loja entity, int key)
		{
			if (entity == null)
				return null;

			Loja existing = DataContext.Set<Loja>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				DataContext.SaveChanges();
			}
			return existing;
		}

		public async Task<Loja> EditAsync(Loja entity, int key)
		{
			if (entity == null)
				return null;

			Loja existing = DataContext.Set<Loja>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				await DataContext.SaveChangesAsync();
			}
			return existing;
		}

		public Loja Find(Expression<Func<Loja, bool>> predicate)
		{
			return DataContext.Set<Loja>().SingleOrDefault(predicate);
		}

      
        public async Task<ICollection<Loja>> FindAllAsync(Expression<Func<Loja, bool>> match)
        {
            return await DataContext.Set<Loja>().Where(match).ToListAsync();
        }

        public async Task<Loja> FindAsync(Expression<Func<Loja, bool>> predicate)
		{
			return await DataContext.Set<Loja>().SingleOrDefaultAsync(predicate);
		}

		public ICollection<Loja> GetAll()
		{
			return DataContext.Set<Loja>().ToList();
		}

		public async Task<ICollection<Loja>> GetAllAsync()
		{
			return await DataContext.Set<Loja>().ToListAsync();
		}

		public Loja GetById(int id)
		{
			return DataContext.Set<Loja>().Find(id);
		}

		public async Task<Loja> GetByIdAsync(int id)
		{
			return await DataContext.Set<Loja>().FindAsync(id);
		}
	}
}
