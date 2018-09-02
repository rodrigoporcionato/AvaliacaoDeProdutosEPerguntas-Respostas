
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

	public class AvaliacaoStatusRepository : ServiceBase, IAvaliacaoStatusRepository
	{
		public AvaliacaoStatus Add(AvaliacaoStatus entity)
		{
			DataContext.Set<AvaliacaoStatus>().Add(entity);
			DataContext.SaveChanges();
			return entity;
		}

		public async Task<AvaliacaoStatus> AddAsync(AvaliacaoStatus entity)
		{
			DataContext.Set<AvaliacaoStatus>().Add(entity);
			await DataContext.SaveChangesAsync();
			return entity;
		}

		public void Delete(AvaliacaoStatus entity)
		{
			DataContext.Set<AvaliacaoStatus>().Remove(entity);
			DataContext.SaveChanges();
		}

		public async Task DeleteAsync(AvaliacaoStatus entity)
		{
			DataContext.Set<AvaliacaoStatus>().Remove(entity);
			await DataContext.SaveChangesAsync();
		}

		public AvaliacaoStatus Edit(AvaliacaoStatus entity, int key)
		{
			if (entity == null)
				return null;

			AvaliacaoStatus existing = DataContext.Set<AvaliacaoStatus>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				DataContext.SaveChanges();
			}
			return existing;
		}

		public async Task<AvaliacaoStatus> EditAsync(AvaliacaoStatus entity, int key)
		{
			if (entity == null)
				return null;

			AvaliacaoStatus existing = DataContext.Set<AvaliacaoStatus>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				await DataContext.SaveChangesAsync();
			}
			return existing;
		}

		public AvaliacaoStatus Find(Expression<Func<AvaliacaoStatus, bool>> predicate)
		{
			return DataContext.Set<AvaliacaoStatus>().SingleOrDefault(predicate);
		}

        public async Task<ICollection<AvaliacaoStatus>> FindAllAsync(Expression<Func<AvaliacaoStatus, bool>> match)
        {
            return await DataContext.Set<AvaliacaoStatus>().Where(match).ToListAsync();
        }

        


        public async Task<AvaliacaoStatus> FindAsync(Expression<Func<AvaliacaoStatus, bool>> predicate)
		{
			return await DataContext.Set<AvaliacaoStatus>().SingleOrDefaultAsync(predicate);
		}

		public ICollection<AvaliacaoStatus> GetAll()
		{
			return DataContext.Set<AvaliacaoStatus>().ToList();
		}

		public async Task<ICollection<AvaliacaoStatus>> GetAllAsync()
		{
			return await DataContext.Set<AvaliacaoStatus>().ToListAsync();
		}

		public AvaliacaoStatus GetById(int id)
		{
			return DataContext.Set<AvaliacaoStatus>().Find(id);
		}

		public async Task<AvaliacaoStatus> GetByIdAsync(int id)
		{
			return await DataContext.Set<AvaliacaoStatus>().FindAsync(id);
		}
	}
}
