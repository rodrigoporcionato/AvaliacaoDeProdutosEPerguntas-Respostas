
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

	public class ClienteAcessoPerfilRepository : ServiceBase, IClienteAcessoPerfilRepository
	{
		public ClienteAcessoPerfil Add(ClienteAcessoPerfil entity)
		{
			DataContext.Set<ClienteAcessoPerfil>().Add(entity);
			DataContext.SaveChanges();
			return entity;
		}

		public async Task<ClienteAcessoPerfil> AddAsync(ClienteAcessoPerfil entity)
		{
			DataContext.Set<ClienteAcessoPerfil>().Add(entity);
			await DataContext.SaveChangesAsync();
			return entity;
		}

		public void Delete(ClienteAcessoPerfil entity)
		{
			DataContext.Set<ClienteAcessoPerfil>().Remove(entity);
			DataContext.SaveChanges();
		}

		public async Task DeleteAsync(ClienteAcessoPerfil entity)
		{
			DataContext.Set<ClienteAcessoPerfil>().Remove(entity);
			await DataContext.SaveChangesAsync();
		}

		public ClienteAcessoPerfil Edit(ClienteAcessoPerfil entity, int key)
		{
			if (entity == null)
				return null;

			ClienteAcessoPerfil existing = DataContext.Set<ClienteAcessoPerfil>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				DataContext.SaveChanges();
			}
			return existing;
		}

		public async Task<ClienteAcessoPerfil> EditAsync(ClienteAcessoPerfil entity, int key)
		{
			if (entity == null)
				return null;

			ClienteAcessoPerfil existing = DataContext.Set<ClienteAcessoPerfil>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				await DataContext.SaveChangesAsync();
			}
			return existing;
		}

		public ClienteAcessoPerfil Find(Expression<Func<ClienteAcessoPerfil, bool>> predicate)
		{
			return DataContext.Set<ClienteAcessoPerfil>().SingleOrDefault(predicate);
		}

        public Task<ICollection<ClienteAcessoPerfil>> FindAllAsync(Expression<Func<ClienteAcessoPerfil, bool>> match)
        {
            throw new NotImplementedException();
        }

        public async Task<ClienteAcessoPerfil> FindAsync(Expression<Func<ClienteAcessoPerfil, bool>> predicate)
		{
			return await DataContext.Set<ClienteAcessoPerfil>().SingleOrDefaultAsync(predicate);
		}

		public ICollection<ClienteAcessoPerfil> GetAll()
		{
			return DataContext.Set<ClienteAcessoPerfil>().ToList();
		}

		public async Task<ICollection<ClienteAcessoPerfil>> GetAllAsync()
		{
			return await DataContext.Set<ClienteAcessoPerfil>().ToListAsync();
		}

		public ClienteAcessoPerfil GetById(int id)
		{
			return DataContext.Set<ClienteAcessoPerfil>().Find(id);
		}

		public async Task<ClienteAcessoPerfil> GetByIdAsync(int id)
		{
			return await DataContext.Set<ClienteAcessoPerfil>().FindAsync(id);
		}
	}
}
