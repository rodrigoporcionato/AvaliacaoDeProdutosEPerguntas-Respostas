
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetaViews.Core.DataBase.Entitys;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Core.DataBase.ORM;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Runtime.Caching;
using System.Collections;
using BetaViews.Messages.SendReceiver.Clientes;
using BetaViews.Messages;
using BetaViews.Messages.Dtos;
using BetaViews.Core.Framework.Extension;

namespace BetaViews.Core.DataBase.Repository
{

	public class ClienteRepository : ServiceBase, IClienteRepository
	{
        
        private const string CacheKey = "CheckTokenAuthorization";        
        public bool CheckTokenAuthorization(string token)
        {

            if (!string.IsNullOrWhiteSpace(token))
            {
                ObjectCache cache = MemoryCache.Default;
                if (cache.Contains(CacheKey))
                {
                    var resultCache = (string)cache.Get(CacheKey);

                    if(resultCache.Split('|')[0]  == token && Convert.ToBoolean(resultCache.Split('|')[1]))
                    return true;
                }
                using (var ctx = new DataBaseContext())
                {
                    object[] xParams = { new SqlParameter("@token", token) };
                    var result = ctx.Database.SqlQuery<int>("exec ValidarTokenAuthorization @token", xParams).FirstOrDefault();

                    if (result == 1)
                    {
                        CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                        cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(2);
                        cache.Add(CacheKey, string.Format("{0}|{1}", token, true), cacheItemPolicy);

                        return true;
                    }

                }
            }

            return false;
        }






        public async Task<ListarBadgeRS> ListarBadgeCliente(ListarBadgeRQ request)
        {
            var badge = new ListarBadgeRS();
            badge.Mensagens = new List<Mensagem>();
            badge.ProtocoloRetorno = Guid.NewGuid();
            badge.Badges = new List<BadgeDTO>();
            badge.Valido = false;

            using (var ctx = new DataBaseContext())
            {
                object[] xParams = {
                            new SqlParameter("@IdCliente", request.IdCliente),
                            new SqlParameter("@Busca",request.Busca.IfEmptyOrWhiteSpace(""))
                    };

                var db = await ctx.Database.SqlQuery<BadgeDTO>("exec BadgeListar @IdCliente, @Busca", xParams).ToListAsync();

                if (db.Any())
                {
                    badge.Valido = true;
                    badge.Badges = db;
                }

            }            
            return badge;

        }







        public Clientes Add(Clientes entity)
		{
			DataContext.Set<Clientes>().Add(entity);
			DataContext.SaveChanges();
			return entity;
		}

		public async Task<Clientes> AddAsync(Clientes entity)
		{
			DataContext.Set<Clientes>().Add(entity);
			await DataContext.SaveChangesAsync();
			return entity;
		}

       

        public void Delete(Clientes entity)
		{
			DataContext.Set<Clientes>().Remove(entity);
			DataContext.SaveChanges();
		}

		public async Task DeleteAsync(Clientes entity)
		{
			DataContext.Set<Clientes>().Remove(entity);
			await DataContext.SaveChangesAsync();
		}

		public Clientes Edit(Clientes entity, int key)
		{
			if (entity == null)
				return null;

			Clientes existing = DataContext.Set<Clientes>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				DataContext.SaveChanges();
			}
			return existing;
		}

		public async Task<Clientes> EditAsync(Clientes entity, int key)
		{
			if (entity == null)
				return null;

			Clientes existing = DataContext.Set<Clientes>().Find(key);
			if (existing != null)
			{
				DataContext.Entry(existing).CurrentValues.SetValues(entity);
				await DataContext.SaveChangesAsync();
			}
			return existing;
		}

		public Clientes Find(Expression<Func<Clientes, bool>> predicate)
		{
			return DataContext.Set<Clientes>().SingleOrDefault(predicate);
		}

        public async Task<ICollection<Clientes>> FindAllAsync(Expression<Func<Clientes, bool>> match)
        {
            return await DataContext.Set<Clientes>().Where(match).ToListAsync();
        }

        public async Task<Clientes> FindAsync(Expression<Func<Clientes, bool>> predicate)
		{
			return await DataContext.Set<Clientes>().SingleOrDefaultAsync(predicate);
		}

		public ICollection<Clientes> GetAll()
		{
			return DataContext.Set<Clientes>().Include("ClienteAcessoLoja").ToList();

        }

		public async Task<ICollection<Clientes>> GetAllAsync()
		{
			return await DataContext.Set<Clientes>().ToListAsync();
		}

		public Clientes GetById(int id)
		{
			return DataContext.Set<Clientes>().Find(id);
		}

		public async Task<Clientes> GetByIdAsync(int id)
		{
			return await DataContext.Set<Clientes>().FindAsync(id);
		}

        public async Task<List<PalavraRecusadaPadrao>> PalavraRecusadaCliente(int idCliente)
        {
            var palavras = await  DataContext.PalavraRecusadaPadraoCliente.Where(x => x.IdCliente == idCliente).ToListAsync();

            var prp = new List<PalavraRecusadaPadrao>();

            palavras.ForEach(x=> prp.Add(new PalavraRecusadaPadrao
            {
                 CodigoPais = "",
                 Id=x.Id,
                 Nome = x.Nome
            }));

            

            return prp;
            
        }

        
    }
}
