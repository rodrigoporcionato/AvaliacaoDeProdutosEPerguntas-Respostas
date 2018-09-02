
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

	public class PalavraRecusadaPadraoClienteRepository : ServiceBase, IPalavraRecusadaPadraoClienteRepository
	{
		public PalavraRecusadaPadraoCliente Add(PalavraRecusadaPadraoCliente entity)
		{
			DataContext.Set<PalavraRecusadaPadraoCliente>().Add(entity);
			DataContext.SaveChanges();
			return entity;
		}

		public async Task<PalavraRecusadaPadraoCliente> AddAsync(PalavraRecusadaPadraoCliente entity)
		{
			DataContext.Set<PalavraRecusadaPadraoCliente>().Add(entity);
			await DataContext.SaveChangesAsync();
			return entity;
		}

		public void Delete(PalavraRecusadaPadraoCliente entity)
		{
			DataContext.Set<PalavraRecusadaPadraoCliente>().Remove(entity);
			DataContext.SaveChanges();
		}

		public async Task DeleteAsync(PalavraRecusadaPadraoCliente entity)
		{
			DataContext.Set<PalavraRecusadaPadraoCliente>().Remove(entity);
			await DataContext.SaveChangesAsync();
		}

		public PalavraRecusadaPadraoCliente Edit(PalavraRecusadaPadraoCliente entity, int key)
		{
			if (entity == null)
				return null;

			PalavraRecusadaPadraoCliente existing = DataContext.Set<PalavraRecusadaPadraoCliente>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				DataContext.SaveChanges();
			}
			return existing;
		}

		public async Task<PalavraRecusadaPadraoCliente> EditAsync(PalavraRecusadaPadraoCliente entity, int key)
		{
			if (entity == null)
				return null;

			PalavraRecusadaPadraoCliente existing = DataContext.Set<PalavraRecusadaPadraoCliente>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				await DataContext.SaveChangesAsync();
			}
			return existing;
		}

		public PalavraRecusadaPadraoCliente Find(Expression<Func<PalavraRecusadaPadraoCliente, bool>> predicate)
		{
			return DataContext.Set<PalavraRecusadaPadraoCliente>().SingleOrDefault(predicate);
		}

        public Task<ICollection<PalavraRecusadaPadraoCliente>> FindAllAsync(Expression<Func<PalavraRecusadaPadraoCliente, bool>> match)
        {
            throw new NotImplementedException();
        }

        public async Task<PalavraRecusadaPadraoCliente> FindAsync(Expression<Func<PalavraRecusadaPadraoCliente, bool>> predicate)
		{
			return await DataContext.Set<PalavraRecusadaPadraoCliente>().SingleOrDefaultAsync(predicate);
		}

		public ICollection<PalavraRecusadaPadraoCliente> GetAll()
		{
			return DataContext.Set<PalavraRecusadaPadraoCliente>().ToList();
		}

		public async Task<ICollection<PalavraRecusadaPadraoCliente>> GetAllAsync()
		{
			return await DataContext.Set<PalavraRecusadaPadraoCliente>().ToListAsync();
		}

		public PalavraRecusadaPadraoCliente GetById(int id)
		{
			return DataContext.Set<PalavraRecusadaPadraoCliente>().Find(id);
		}

		public async Task<PalavraRecusadaPadraoCliente> GetByIdAsync(int id)
		{
			return await DataContext.Set<PalavraRecusadaPadraoCliente>().FindAsync(id);
		}
	}
}
