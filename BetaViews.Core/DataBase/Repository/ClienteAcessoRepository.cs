
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

	public class ClienteAcessoRepository : ServiceBase, IClienteAcessoRepository
	{

        /// <summary>
        /// lista todas as páginas de acesso
        /// </summary>
        /// <returns></returns>
        public async Task<List<PaginaAcesso>> PaginasAcessoListAsync()
        {

            var paginas = await DataContext.Set<PaginaAcesso>().ToListAsync();

            return paginas;
        }

        /// <summary>
        /// lista todas as páginas de acesso de um usuário especifico
        /// </summary>
        /// <param name="idClienteAcesso"></param>
        /// <returns></returns>
        public async Task<List<PaginaAcesso>> PaginasAcessoListAsync(int idClienteAcesso)
        {

            var paginas = await DataContext.Set<PaginaAcesso>().Include("ClienteAcessoPerfil").Where(x => x.ClienteAcessoPerfil.Where(ss => ss.IdClienteAcesso == idClienteAcesso).Any()).ToListAsync();

            return paginas;
        }



        public ClienteAcesso Add(ClienteAcesso entity)
		{
			DataContext.Set<ClienteAcesso>().Add(entity);
			DataContext.SaveChanges();
			return entity;
		}

		public async Task<ClienteAcesso> AddAsync(ClienteAcesso entity)
		{
			DataContext.Set<ClienteAcesso>().Add(entity);
			await DataContext.SaveChangesAsync();
			return entity;
		}

		public void Delete(ClienteAcesso entity)
		{
			DataContext.Set<ClienteAcesso>().Remove(entity);
			DataContext.SaveChanges();
		}

		public async Task DeleteAsync(ClienteAcesso entity)
		{
			DataContext.Set<ClienteAcesso>().Remove(entity);
			await DataContext.SaveChangesAsync();
		}

		public ClienteAcesso Edit(ClienteAcesso entity, int key)
		{
			if (entity == null)
				return null;

			ClienteAcesso existing = DataContext.Set<ClienteAcesso>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				DataContext.SaveChanges();
			}
			return existing;
		}

		public async Task<ClienteAcesso> EditAsync(ClienteAcesso entity, int key)
		{
			if (entity == null)
				return null;

			ClienteAcesso existing = DataContext.Set<ClienteAcesso>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				await DataContext.SaveChangesAsync();
			}
			return existing;
		}

		public ClienteAcesso Find(Expression<Func<ClienteAcesso, bool>> predicate)
		{
			return DataContext.Set<ClienteAcesso>().SingleOrDefault(predicate);
		}

		public async Task<ClienteAcesso> FindAsync(Expression<Func<ClienteAcesso, bool>> predicate)
		{
			return await DataContext.Set<ClienteAcesso>().SingleOrDefaultAsync(predicate);
		}

		public ICollection<ClienteAcesso> GetAll()
		{
			return DataContext.Set<ClienteAcesso>().ToList();
		}

		public async Task<ICollection<ClienteAcesso>> GetAllAsync()
		{
			return await DataContext.Set<ClienteAcesso>().Include("ClienteAcessoPerfil").ToListAsync();
		}

		public ClienteAcesso GetById(int id)
		{
			return DataContext.Set<ClienteAcesso>().Find(id);
		}

		public async Task<ClienteAcesso> GetByIdAsync(int id)
		{
			return await DataContext.Set<ClienteAcesso>().FindAsync(id);
		}

        public ClienteAcesso ValidateUser(string user, string senha)
        {
            var acesso = DataContext.ClienteAcesso.Include("ClienteAcessoLoja").Include("ClienteAcessoPerfil").Where(x => x.Email.Trim().ToLower() == user.Trim().ToLower() && x.Senha.Trim().ToLower() == senha.Trim().ToLower() && x.FlagStatus);

            if (acesso.Any())
            {
                return acesso.FirstOrDefault();
            }
            return null;
        }

        public async Task<ICollection<ClienteAcesso>> FindAllAsync(Expression<Func<ClienteAcesso, bool>> match)
        {
            return await DataContext.Set<ClienteAcesso>().Include("ClienteAcessoPerfil").Where(match).ToListAsync();

        }

        public async Task AdicionarClienteAcessoPerfilPaginas(int[] IdsPaginaAcesso, int IdClienteAcesso)
        {
            if (IdsPaginaAcesso.Any() && IdClienteAcesso>0)
            {
            var clienteAcessoPerfil = new List<ClienteAcessoPerfil>();

            IdsPaginaAcesso.ToList().ForEach(x=>
                clienteAcessoPerfil.Add(new ClienteAcessoPerfil { IdClienteAcesso=IdClienteAcesso, IdPaginaAcesso=x})
                );

            DataContext.Set<ClienteAcessoPerfil>().AddRange(clienteAcessoPerfil);
            await DataContext.SaveChangesAsync();
            }
        }

        public async Task RemoverClienteAcessoPerfilPaginas(int IdClienteAcesso)
        {
            var IdsClienteAcessoPerfil = await DataContext.ClienteAcessoPerfil.Where(x => x.IdClienteAcesso == IdClienteAcesso).ToListAsync();
            if (IdsClienteAcessoPerfil.Any())
            {
                IdsClienteAcessoPerfil.ForEach(x =>
                {
                    DataContext.Set<ClienteAcessoPerfil>().Remove(x);
                    DataContext.SaveChanges();
                });
            }
        }

        public async Task AdicionarClienteAcessoLoja(int IdClienteAcesso, int[] IdsLoja)
        {
            if (IdsLoja.Any() && IdClienteAcesso > 0)
            {
                var clienteAcessoLoja = new List<ClienteAcessoLoja>();

                IdsLoja.ToList().ForEach(x =>
                    clienteAcessoLoja.Add(new ClienteAcessoLoja { IdClienteAcesso = IdClienteAcesso, IdLoja = x })
                    );

                if (clienteAcessoLoja.Any())
                {
                    DataContext.Set<ClienteAcessoLoja>().AddRange(clienteAcessoLoja);
                    await DataContext.SaveChangesAsync();
                }
            }
        }

        public async Task RemoverClienteAcessoLoja(int IdClienteAcesso)
        {
            var idsClienteAcessoLoja = await DataContext.ClienteAcessoLoja.Where(x => x.IdClienteAcesso == IdClienteAcesso).ToListAsync();
            if(idsClienteAcessoLoja.Any())
            idsClienteAcessoLoja.ForEach(x => 
            {
                DataContext.Set<ClienteAcessoLoja>().Remove(x);
                DataContext.SaveChanges();
            });
        }

        public async Task<List<ClienteAcessoLoja>> ClienteAcessoLojaListAsync(int idClienteAcesso)
        {            
            return await DataContext.ClienteAcessoLoja.Where(x => x.IdClienteAcesso == idClienteAcesso).ToListAsync();            
        }
    }
}
