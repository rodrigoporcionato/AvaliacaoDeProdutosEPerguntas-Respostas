using BetaViews.Core.DataBase.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetaViews.Core.DataBase.Entitys;
using BetaViews.Messages.SendReceiver.QA;
using System.Linq.Expressions;
using BetaViews.Core.DataBase.ORM;
using System.Data.SqlClient;
using System.Data;
using BetaViews.Core.DataBase.Repository.DataMapper;
using BetaViews.Core.Framework.Extension;
using BetaViews.Messages;
using BetaViews.Messages.Dtos;
using BetaViews.Messages.Dtos.QA;
using BetaViews.Messages.SendReceiver.QA.ListarPergunta;
using BetaViews.Messages.SendReceiver.QA.Moderacao;
using BetaViews.Messages.SendReceiver.ModeracaoAuto;
using BetaViews.Messages.Enums;
using System.Data.Entity;

namespace BetaViews.Core.DataBase.Repository
{
    public class QARepository : ServiceBase, IQARepository
    {



        public async Task AlterarStatusQA(int IdQA, StatusQAEnum IdQAStatus, string motivo)
        {
            using (var ctx = new DataBaseContext())
            {
                object[] xParams = {
                    new SqlParameter("@IdQa" , IdQA),
                    new SqlParameter("@IdQAStatus" , IdQAStatus),
                    new SqlParameter("@Motivo" , motivo)
                };

                await ctx.Database.ExecuteSqlCommandAsync("exec QAStatusAlterar @IdQa,@IdQAStatus, @Motivo", xParams);

            }
        }


        public async Task<ModeracaoAutoRS> ListarPerguntasPendentes(ModeracaoAutoRQ request)
        {


            var avaliacoes = new ModeracaoAutoRS();
            avaliacoes.Perguntas = new List<QAProdutoDTO>();
            

            using (var ctx = new DataBaseContext())
            {


                object[] xParams = {
                    new SqlParameter("@APIKey" , request.Token),                    
                };

                var db = await ctx.Database.SqlQuery<QAListarPaginaDataMapper>("exec QAModeracaoListar @APIKey", xParams).ToListAsync();

                if (db.Any())
                {
                    foreach (var item in db)
                    {
                        avaliacoes.Perguntas.Add(
                            new QAProdutoDTO
                            {
                                IDQuestion = item.IdQA,
                                ClienteNome = item.ClienteNome,
                                ClienteEmail = item.ClienteEmail,
                                ClientePergunta = item.ClientePergunta,
                                RespondidoPorOutroCliente = item.RespTerceiroStatus == 1 ? true : false,
                                Resposta = item.Resposta,
                                RespostaNome = item.RespTerceiroClienteNome,
                                Produto = new ProdutoDTO
                                {
                                    PrdCodigo = item.ProdCodigo,
                                    PrdNome = item.PrdNome

                                }
                            });
                    }
                }

            }
            return avaliacoes;

        }



        public async Task<int> AddQAAsync(PerguntasERespostasRQ request)
        {
            object[] xParams = {
                new SqlParameter("@RETURN_VALUE",DbType.Int32) { Direction = ParameterDirection.Output},                
                new SqlParameter("@APIKey", request.Authorization),
                new SqlParameter("@ClienteNome", request.ClienteNome),
                new SqlParameter("@ClienteEmail", request.ClienteEmail),
                new SqlParameter("@ClienteLocalizacao", request.ClienteLocalizacao.ToNullString()),
                new SqlParameter("@ClienteDuvida", request.ClientePergunta),
                new SqlParameter("@LojaCodigo", request.Loja.LojaCodigo),
                new SqlParameter("@LojaNome", request.Loja.LojaNome.ToNullString()),
                new SqlParameter("@LojaMKTP", request.Loja.LojaMarketPlace),
                new SqlParameter("@PrdNome", request.Produto.PrdNome.ToNullString()),
                new SqlParameter("@PrdCodigo", request.Produto.PrdCodigo),
                new SqlParameter("@PrdDepartamento", request.Produto.PrdDepartamento.ToNullString()),
                new SqlParameter("@PrdCategoria", request.Produto.PrdCategoria.ToNullString()),
                new SqlParameter("@PrdSubCategoria", request.Produto.PrdSubCategoria.ToNullString()),
                new SqlParameter("@PrdMarca", request.Produto.PrdMarca.ToNullString()),
                new SqlParameter("@PrdLink", request.Produto.PrdLink.ToNullString()),
                new SqlParameter("@PrdImagemProduto", request.Produto.PrdImagemProduto.ToNullString())
            };
            await DataContext.Database.ExecuteSqlCommandAsync("exec QAAdicionar @RETURN_VALUE OUTPUT, @APIKey, @ClienteNome, @ClienteEmail, @ClienteLocalizacao, @ClienteDuvida,@LojaCodigo,@LojaNome,@LojaMKTP,@PrdNome,@PrdCodigo,@PrdDepartamento,@PrdCategoria,@PrdSubCategoria,@PrdMarca,@PrdLink,@PrdImagemProduto", xParams);

            object idRetorno = ((SqlParameter)xParams[0]).Value;

            if (!idRetorno.Equals(DBNull.Value)) return (int)idRetorno;

            return 0;
        }
        
        public async Task<int> AddQARespostaTerceiroAsync(PerguntasERespostasRespTerceiroRQ request)
        {
            object[] xParams = {
                new SqlParameter("@IdQuestion", request.IdQuestion),
                new SqlParameter("@ClienteNome", request.ClienteNome),
                new SqlParameter("@ClienteEmail", request.ClienteEmail),
                new SqlParameter("@ClienteLocalizacao", request.ClienteLocalizacao.ToNullString()),
                new SqlParameter("@Resposta", request.Resposta)
            };

            await DataContext.Database.ExecuteSqlCommandAsync("exec QAAAdicionarResposta @IdQuestion,@ClienteNome,@ClienteEmail,@ClienteLocalizacao,@Resposta ", xParams);

            return 1;
        }

        /// <summary>
        /// responde a um cliente via moderacao um usuário valido
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task ModeracaoResponderCliente(QAModeracaoResponderClienteRQ request)
        {
            object[] xParams = {
                new SqlParameter("@IdQuestion", request.IdQuestion),
                new SqlParameter("@IdClienteAcesso", request.IdClienteAcesso),
                new SqlParameter("@NomeRespondente", request.NomeRespondente),
                new SqlParameter("@Resposta", request.Resposta),
                new SqlParameter("@IdBadge", request.IdBadge),
            };
                        
            await DataContext.Database.ExecuteSqlCommandAsync("exec QAModeracaoResponderCliente @IdQuestion,@IdClienteAcesso, @NomeRespondente, @Resposta,@IdBadge", xParams);            
        }

        


        public async Task<PerguntasERespostasDisplayRS> ObterDuvidasAsync(PerguntasERespostasDisplayRQ request)
        {

            PerguntasERespostasDisplayRS response = new PerguntasERespostasDisplayRS();
            response.Mensagens = new List<Mensagem>();
            response.ProtocoloRetorno = Guid.NewGuid();
            response.Loja = new LojaDTO();
            response.Produto = new ProdutoDTO();
            response.QA = new List<QADTO>();
            response.Pagination = new PaginationDTO();
            response.CodigoRetorno = 400;
            response.Valido = false;
            using (var ctx = new DataBaseContext())
            {

                object[] xParams = {
                        new SqlParameter("@APIKey", request.CodCliente) ,
                        new SqlParameter("@CodProduto",request.PrdCodigo),
                        new SqlParameter("@CodLoja",request.CodigoLoja),
                        new SqlParameter("@ActualPageNumber",request.PageNumber),
                        new SqlParameter("@Filtro",request.Filter.DefaultIfNullOrEmpty("")),
                        new SqlParameter("@Busca",request.Busca.DefaultIfNullOrEmpty(""))
                };

                var db = await ctx.Database.SqlQuery<QAListarPaginaDataMapper>("exec QAListarPagina @APIKey,@CodProduto,@CodLoja,@ActualPageNumber,@Filtro,@Busca", xParams).ToListAsync();

                if (db.Any())
                {
                    response.CodigoRetorno = 200;
                    //response.Loja.CodigoCliente = request.CodCliente;
                    response.Loja.LojaCodigo = request.CodigoLoja;
                    response.Produto.PrdCodigo = request.PrdCodigo;
                    response.Produto.PrdNome = db.FirstOrDefault().PrdNome;
                    //paginacao                    
                    response.Pagination.TotalRows = db.FirstOrDefault().TotalRows;
                    response.Pagination.ActualPageNumber = request.PageNumber;
                    response.Pagination.RowsPerPage = (int)Math.Ceiling(response.Pagination.TotalRows / (double)10);
                    response.Pagination.BtnPaginationName = "btnQAGetPage";

                    foreach (var item in db)
                    {
                        bool respTerceiro = false;
                        if (item.RespTerceiroStatus==1)
                        {
                            respTerceiro = true;
                            item.NomeModerador = $"{item.RespTerceiroClienteNome}, nosso cliente.";
                        }
                        response.QA.Add(new QADTO
                        {
                            IDQuestion = item.IdQA,
                            ClienteNome = item.ClienteNome,
                            ClienteEmail = item.ClienteEmail,
                            ClienteLocalizacao = item.ClienteLocalizacao,
                            Badge = item.Badge,
                            ClientePergunta = item.ClientePergunta,
                            DtPergunta = item.DtPergunta,
                            DTResposta = item.DtResposta.HasValue? item.DtResposta.Value: DateTime.MinValue,
                            QtdRespAjudou = item.QtdAjudou,
                            QtdRespNaoAjudou = item.QtdNaoAjudou,
                            Resposta = item.Resposta,                            
                            RespostaNome = item.NomeModerador,
                            RespondidoPorOutroCliente = respTerceiro,
                            IdQAStatus = (StatusQAEnum)item.IdQAStatus
                        });



                    }

                }

            }

            return response;
        }




        public async Task<ListarPerguntaRS> ListarPerguntaAdmin(ListarPerguntaRQ request)
        {
            var qa = new ListarPerguntaRS();
            qa.Mensagens = new List<Mensagem>();
            qa.ProtocoloRetorno = Guid.NewGuid();
            qa.Pagination = new PaginationDTO();
            qa.Perguntas = new List<QAProdutoDTO>();

            using (var ctx = new DataBaseContext())
            {
                object[] xParams = {
                            new SqlParameter("@IdCliente", request.IdCliente),
                            new SqlParameter("@ActualPageNumber",request.ActualPageNumber),
                            new SqlParameter("@Busca",request.Busca.IfEmptyOrWhiteSpace("")),
                            new SqlParameter("@IdQAStatus", StatusQAEnum.PendenteRevisao)
                    };
                var db = await ctx.Database.SqlQuery<QAListarPaginaDataMapper>("exec QAListarAdmin @IdCliente, @ActualPageNumber, @Busca,@IdQAStatus", xParams).ToListAsync();

                if (db.Any())
                {
                    qa.Pagination.TotalRows = db.FirstOrDefault().TotalRows;
                    qa.Pagination.ActualPageNumber = request.ActualPageNumber;
                    qa.Pagination.RowsPerPage = (int)Math.Ceiling(qa.Pagination.TotalRows / (double)25);
                    qa.Pagination.BtnPaginationName = "btnQAGetPage";

                    foreach (var item in db)
                    {
                        qa.Perguntas.Add(new QAProdutoDTO
                        {
                            IDQuestion = item.IdQA,
                            ClienteNome = item.ClienteNome.ToUpperFirstLetter(),
                            ClienteEmail = item.ClienteEmail,
                            ClienteLocalizacao = item.ClienteLocalizacao,
                            Badge = item.Badge,
                            ClientePergunta = item.ClientePergunta,
                            DtPergunta = item.DtPergunta,
                            DTResposta = item.DtPergunta,
                            QtdRespAjudou = item.QtdAjudou,
                            QtdRespNaoAjudou = item.QtdNaoAjudou,
                            Resposta = item.Resposta,
                            RespostaNome = item.NomeModerador,
                            RespondidoPorOutroCliente = item.RespTerceiroStatus==1 ? true: false,
                            RespTerceiroClienteEmail = item.RespTerceiroClienteEmail,
                            RespTerceiroClienteLocalizacao = item.RespTerceiroClienteLocalizacao,
                            RespTerceiroClienteNome = item.RespTerceiroClienteNome,
                            Produto = new ProdutoDTO {
                                 PrdCodigo = item.ProdCodigo,
                                 PrdNome = item.PrdNome,
                                 PrdLink = item.PrdLink
                            },
                            Loja = new LojaDTO
                            {
                                LojaCodigo = item.CodigoLoja,
                                LojaNome = item.LojaNome,
                                LojaMarketPlace = item.LojaMktPlace
                            }
                        });
                    }
                }

            }

            return qa;
        }





















        #region Não implementado


        
        public QA Find(Expression<Func<QA, bool>> predicate)
        {            
            return DataContext.Set<QA>().Include("Produto").SingleOrDefault(predicate);
        }

        public QA Add(QA entity)
        {
            DataContext.Set<QA>().Add(entity);
            DataContext.SaveChanges();
            return entity;
        }

        public async Task<QA> AddAsync(QA entity)
        {
            DataContext.Set<QA>().Add(entity);
            await DataContext.SaveChangesAsync();
            return entity;
        }



        public void Delete(QA entity)
        {
            DataContext.Set<QA>().Remove(entity);
            DataContext.SaveChanges();
        }

        public async Task DeleteAsync(QA entity)
        {
            DataContext.Set<QA>().Remove(entity);
            await DataContext.SaveChangesAsync();
        }

        public QA Edit(QA entity, int key)
        {
            if (entity == null)
                return null;

            QA existing = DataContext.Set<QA>().Find(key);
            if (existing != null)
            {
                DataContext.Entry(existing).CurrentValues.SetValues(entity);
                DataContext.SaveChanges();
            }
            return existing;
        }

        public async Task<QA> EditAsync(QA entity, int key)
        {
            if (entity == null)
                return null;

            QA existing = DataContext.Set<QA>().Find(key);
            if (existing != null)
            {
                DataContext.Entry(existing).CurrentValues.SetValues(entity);
                await DataContext.SaveChangesAsync();
            }
            return existing;
        }

        

        public async Task<ICollection<QA>> FindAllAsync(Expression<Func<QA, bool>> match)
        {
            return await DataContext.Set<QA>().Where(match).ToListAsync();
        }

        public async Task<QA> FindAsync(Expression<Func<QA, bool>> predicate)
        {
            return await DataContext.Set<QA>().SingleOrDefaultAsync(predicate);
        }

        public ICollection<QA> GetAll()
        {
            return DataContext.Set<QA>().Include("ClienteAcessoLoja").ToList();

        }

        public async Task<ICollection<QA>> GetAllAsync()
        {
            return await DataContext.Set<QA>().ToListAsync();
        }

        public QA GetById(int id)
        {
            return DataContext.Set<QA>().Find(id);
        }

        public async Task<QA> GetByIdAsync(int id)
        {
            return await DataContext.Set<QA>().FindAsync(id);
        }






        #endregion
    }
}
