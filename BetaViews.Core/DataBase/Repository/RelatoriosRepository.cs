using BetaViews.Core.DataBase.ORM;
using BetaViews.Core.DataBase.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetaViews.Messages.Models;
using System.Data.Entity.Infrastructure;
using BetaViews.Core.Framework.Extension;

namespace BetaViews.Core.DataBase.Repository
{
    public class RelatoriosRepository : ServiceBase, IRelatoriosRepository
    {


        public RelatoriosDeLojas RelTopLojas(int? idCliente, int page)
        {
            var model = new RelatoriosDeLojas();
            model.Lojas = new List<LojaModel>();
            model.TopMaisAvaliado = new List<LojaModel>();
            model.TopMenosAvaliado = new List<LojaModel>();
            
            using (var ctx = new DataBaseContext())
            {
                var cmd = ctx.Database.Connection.CreateCommand();

                cmd.CommandText = "exec RelLojas 1,1";

                ctx.Database.Connection.Open();
                var reader = cmd.ExecuteReader();

               
                model.TopMaisAvaliado = ((IObjectContextAdapter)ctx).ObjectContext.Translate<LojaModel>(reader).ToList();
                model.TopMaisAvaliado.ForEach(x =>
                {
                    x.MediaTotal = StringExtensions.RetornaClassificacaoGeral(x.s1, x.s2, x.s3, x.s4, x.s5, x.TotalAvaliacoes);
                });

                reader.NextResult();
                model.TopMenosAvaliado = ((IObjectContextAdapter)ctx).ObjectContext.Translate<LojaModel>(reader).ToList();
                model.TopMenosAvaliado.ForEach(x =>
                {
                    x.MediaTotal = StringExtensions.RetornaClassificacaoGeral(x.s1, x.s2, x.s3, x.s4, x.s5, x.TotalAvaliacoes);
                });

                reader.NextResult();
                model.Lojas = ((IObjectContextAdapter)ctx).ObjectContext.Translate<LojaModel>(reader).ToList();
                model.Lojas.ForEach(x =>
                {
                    x.MediaTotal = StringExtensions.RetornaClassificacaoGeral(x.s1, x.s2, x.s3, x.s4, x.s5, x.TotalAvaliacoes);
                });

                ctx.Database.Connection.Close();

            }
            return model;
        }


        public RelDepartamentos RelDepartamentos(int idCliente, int IdLoja)
        {
            var model = new RelDepartamentos();
            model.TopMaisAvaliados = new List<DepartamentosModel>();
            model.TopMenosAvaliados = new List<DepartamentosModel>();


            using (var ctx = new DataBaseContext())
            {
                var cmd = ctx.Database.Connection.CreateCommand();

                cmd.CommandText = "exec RelDepartamentos 1,1"; ///mudar no futuro!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                ctx.Database.Connection.Open();
                var reader = cmd.ExecuteReader();

                model.TopMaisAvaliados = ((IObjectContextAdapter)ctx).ObjectContext
                    .Translate<DepartamentosModel>(reader).ToList();


                model.TopMaisAvaliados.ForEach(x =>
                {
                    x.MediaTotal = StringExtensions.RetornaClassificacaoGeral(x.s1, x.s2, x.s3, x.s4, x.s5, x.TotalAvaliacoes);
                });

                reader.NextResult();

                model.TopMenosAvaliados = ((IObjectContextAdapter)ctx).ObjectContext
                   .Translate<DepartamentosModel>(reader).ToList();

                model.TopMenosAvaliados.ForEach(x =>
                {
                    x.MediaTotal = StringExtensions.RetornaClassificacaoGeral(x.s1, x.s2, x.s3, x.s4, x.s5, x.TotalAvaliacoes);
                });


                reader.NextResult();

                ctx.Database.Connection.Close();
            }


            return model;
        }


        public RelMarcas RelMarcas(int idCliente, int IdLoja)
        {
            var model = new RelMarcas();
            model.TopMaisAvaliados = new List<MarcasModel>();
            model.TopMenosAvaliados = new List<MarcasModel>();


            using (var ctx = new DataBaseContext())
            {
                var cmd = ctx.Database.Connection.CreateCommand();

                cmd.CommandText = "exec RelMarcas 1,1"; ///mudar no futuro!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                ctx.Database.Connection.Open();
                var reader = cmd.ExecuteReader();

                model.TopMaisAvaliados = ((IObjectContextAdapter)ctx).ObjectContext
                    .Translate<MarcasModel>(reader).ToList();


                model.TopMaisAvaliados.ForEach(x =>
                {
                    x.MediaTotal = StringExtensions.RetornaClassificacaoGeral(x.s1, x.s2, x.s3, x.s4, x.s5, x.TotalAvaliacoes);
                });

                reader.NextResult();

                model.TopMenosAvaliados = ((IObjectContextAdapter)ctx).ObjectContext
                   .Translate<MarcasModel>(reader).ToList();

                model.TopMenosAvaliados.ForEach(x =>
                {
                    x.MediaTotal = StringExtensions.RetornaClassificacaoGeral(x.s1, x.s2, x.s3, x.s4, x.s5, x.TotalAvaliacoes);
                });


                reader.NextResult();

                ctx.Database.Connection.Close();
            }


            return model;
        }
    }
}
