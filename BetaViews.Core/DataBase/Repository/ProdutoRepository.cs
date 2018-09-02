using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BetaViews.Core.DataBase.Entitys;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Core.DataBase.ORM;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BetaViews.Messages.Models;
using BetaViews.Core.Framework.Extension;

namespace BetaViews.Core.DataBase.Repository
{
    public class ProdutoRepository : ServiceBase, IProdutoRepository
    {
                

        public RelatoriosDeProdutosModel RelTopProdutos(int? idCliente, int page)
        {
            var model = new RelatoriosDeProdutosModel();
            model.Produtos = new List<ProdutoModel>();
            model.TopMaisAvaliado = new List<ProdutoModel>();
            model.TopMenosAvaliado = new List<ProdutoModel>();

            using (var ctx = new DataBaseContext())
            {
                var cmd = ctx.Database.Connection.CreateCommand();

                cmd.CommandText = "exec RelProdutos 1,1,1," + page; ///mudar no futuro!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                ctx.Database.Connection.Open();
                var reader = cmd.ExecuteReader();

                model.TopMaisAvaliado = ((IObjectContextAdapter)ctx).ObjectContext
                    .Translate<ProdutoModel>(reader).ToList();

                
                model.TopMaisAvaliado.ForEach(x =>
                {
                    x.MediaTotal = StringExtensions.RetornaClassificacaoGeral(x.s1, x.s2, x.s3, x.s4, x.s5, x.TotalAvaliacoes);
                });

                reader.NextResult();

                model.TopMenosAvaliado = ((IObjectContextAdapter)ctx).ObjectContext
                   .Translate<ProdutoModel>(reader).ToList();

                model.TopMenosAvaliado.ForEach(x =>
                {
                    x.MediaTotal = StringExtensions.RetornaClassificacaoGeral(x.s1, x.s2, x.s3, x.s4, x.s5, x.TotalAvaliacoes);
                });


                reader.NextResult();

                model.Produtos = ((IObjectContextAdapter)ctx).ObjectContext
                   .Translate<ProdutoModel>(reader).ToList();

                model.Produtos.ForEach(x =>
                {
                    x.MediaTotal = StringExtensions.RetornaClassificacaoGeral(x.s1, x.s2, x.s3, x.s4, x.s5, x.TotalAvaliacoes);
                });





                ctx.Database.Connection.Close();

            }
            return model;
        }





        #region Generics

        public Produto Add(Produto entity)
        {
            DataContext.Set<Produto>().Add(entity);
            DataContext.SaveChanges();
            return entity;
        }

        public async Task<Produto> AddAsync(Produto entity)
        {
            DataContext.Set<Produto>().Add(entity);
            await DataContext.SaveChangesAsync();
            return entity;
        }

        public void Delete(Produto entity)
        {
            DataContext.Set<Produto>().Remove(entity);
            DataContext.SaveChanges();
        }

        public async Task DeleteAsync(Produto entity)
        {
            DataContext.Set<Produto>().Remove(entity);
            await DataContext.SaveChangesAsync();
        }

        public Produto Edit(Produto entity, int key)
        {
            if (entity == null)
                return null;

            Produto existing = DataContext.Set<Produto>().Find(key);
            if (existing != null)
            {
                DataContext.Entry(existing).CurrentValues.SetValues(entity);
                DataContext.SaveChanges();
            }
            return existing;
        }

        public async Task<Produto> EditAsync(Produto entity, int key)
        {
            if (entity == null)
                return null;

            Produto existing = DataContext.Set<Produto>().Find(key);
            if (existing != null)
            {
                DataContext.Entry(existing).CurrentValues.SetValues(entity);
                await DataContext.SaveChangesAsync();
            }
            return existing;
        }

        public Produto Find(Expression<Func<Produto, bool>> predicate)
        {
            return DataContext.Set<Produto>().SingleOrDefault(predicate);
        }


        public async Task<ICollection<Produto>> FindAllAsync(Expression<Func<Produto, bool>> match)
        {
            return await DataContext.Set<Produto>().Where(match).ToListAsync();
        }

        public async Task<Produto> FindAsync(Expression<Func<Produto, bool>> predicate)
        {
            return await DataContext.Set<Produto>().Include("Loja").SingleOrDefaultAsync(predicate);
        }

        public ICollection<Produto> GetAll()
        {
            return DataContext.Set<Produto>().ToList();
        }

        public async Task<ICollection<Produto>> GetAllAsync()
        {
            return await DataContext.Set<Produto>().ToListAsync();
        }

        public Produto GetById(int id)
        {
            return DataContext.Set<Produto>().Find(id);
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await DataContext.Set<Produto>().FindAsync(id);
        }

       
        #endregion


    }
}
