
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

	public class PalavraRecusadaPadraoRepository : ServiceBase, IPalavraRecusadaPadraoRepository
	{

        public PalavraRecusadaPadrao Add(PalavraRecusadaPadrao entity)
		{
			DataContext.Set<PalavraRecusadaPadrao>().Add(entity);
			DataContext.SaveChanges();
			return entity;
		}

		public async Task<PalavraRecusadaPadrao> AddAsync(PalavraRecusadaPadrao entity)
		{
			DataContext.Set<PalavraRecusadaPadrao>().Add(entity);
			await DataContext.SaveChangesAsync();
			return entity;
		}

		public void Delete(PalavraRecusadaPadrao entity)
		{
			DataContext.Set<PalavraRecusadaPadrao>().Remove(entity);
			DataContext.SaveChanges();
		}

		public async Task DeleteAsync(PalavraRecusadaPadrao entity)
		{
			DataContext.Set<PalavraRecusadaPadrao>().Remove(entity);
			await DataContext.SaveChangesAsync();
		}

		public PalavraRecusadaPadrao Edit(PalavraRecusadaPadrao entity, int key)
		{
			if (entity == null)
				return null;

			PalavraRecusadaPadrao existing = DataContext.Set<PalavraRecusadaPadrao>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				DataContext.SaveChanges();
			}
			return existing;
		}

		public async Task<PalavraRecusadaPadrao> EditAsync(PalavraRecusadaPadrao entity, int key)
		{
			if (entity == null)
				return null;

			PalavraRecusadaPadrao existing = DataContext.Set<PalavraRecusadaPadrao>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				await DataContext.SaveChangesAsync();
			}
			return existing;
		}

		public PalavraRecusadaPadrao Find(Expression<Func<PalavraRecusadaPadrao, bool>> predicate)
		{
			return DataContext.Set<PalavraRecusadaPadrao>().SingleOrDefault(predicate);
		}

        public Task<ICollection<PalavraRecusadaPadrao>> FindAllAsync(Expression<Func<PalavraRecusadaPadrao, bool>> match)
        {
            throw new NotImplementedException();
        }

        public async Task<PalavraRecusadaPadrao> FindAsync(Expression<Func<PalavraRecusadaPadrao, bool>> predicate)
		{
			return await DataContext.Set<PalavraRecusadaPadrao>().SingleOrDefaultAsync(predicate);
		}

		public ICollection<PalavraRecusadaPadrao> GetAll()
		{
			return DataContext.Set<PalavraRecusadaPadrao>().ToList();
		}

		public async Task<ICollection<PalavraRecusadaPadrao>> GetAllAsync()
		{            
			return await DataContext.Set<PalavraRecusadaPadrao>().ToListAsync();
		}

		public PalavraRecusadaPadrao GetById(int id)
		{
			return DataContext.Set<PalavraRecusadaPadrao>().Find(id);
		}

		public async Task<PalavraRecusadaPadrao> GetByIdAsync(int id)
		{
			return await DataContext.Set<PalavraRecusadaPadrao>().FindAsync(id);
		}
	}
}
