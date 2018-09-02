using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using BetaViews.Core.Framework.Extension;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Core.DataBase.ORM;
using BetaViews.Core.DataBase.Entitys;
using BetaViews.Messages.Enums;
using BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.EnviarAvaliacoes.Produto;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.ListarAvaliacoes.Produto;
using BetaViews.Messages.SendReceiver.Avaliacoes.Moderacao.Loja;
using BetaViews.Messages.SendReceiver.Avaliacoes.Moderacao.Produto;
using BetaViews.Messages;
using BetaViews.Core.DataBase.Repository.DataMapper;
using BetaViews.Messages.Dtos;
using BetaViews.Messages.SendReceiver.ModeracaoAuto;

namespace BetaViews.Core.DataBase.Repository
{

    public class AvaliacaoRepository : ServiceBase, IAvaliacaoRepository
	{

        #region AVALIACOES DE PRODUTOS E LOJAS

        /// <summary>
        /// Adicionar avaliação de produto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> EnviarAvaliacaoProduto(EnviarAvaliacaoProdutoRQ entity)
        {
            object[] xParams = {
                new SqlParameter("@RETURN_VALUE",DbType.Int32) { Direction = ParameterDirection.Output},
                new SqlParameter("@APIKey", entity.Authorization),
                new SqlParameter("@ClienteNome", entity.Avaliacao.ClienteNome),
                new SqlParameter("@ClienteEmail", entity.Avaliacao.ClienteEmail),
                new SqlParameter("@ClienteLocalizacao", entity.Avaliacao.ClienteLocalizacao.ToNullString()),
                new SqlParameter("@ClienteTitulo", entity.Avaliacao.ClienteTitulo),
                new SqlParameter("@ClienteComentario", entity.Avaliacao.ClienteComentario),
                new SqlParameter("@ClienteClassificacao", entity.Avaliacao.ClienteClassificacao),
                new SqlParameter("@ClienteRecomenda", entity.Avaliacao.ClienteRecomenda),
                new SqlParameter("@ClienteVerificado", false),
                //dados do produto.
                new SqlParameter("@PrdNome", entity.Produto.PrdNome.ToNullString()),
                new SqlParameter("@PrdCodigo", entity.Produto.PrdCodigo.ToNullString()),
                new SqlParameter("@PrdDepartamento", entity.Produto.PrdDepartamento.ToNullString()),
                new SqlParameter("@PrdCategoria", entity.Produto.PrdCategoria.ToNullString()),
                new SqlParameter("@PrdSubCategoria", entity.Produto.PrdSubCategoria.ToNullString()),
                new SqlParameter("@PrdMarca", entity.Produto.PrdMarca.ToNullString()),
                new SqlParameter("@PrdLink", entity.Produto.PrdLink.ToNullString()),
                new SqlParameter("@PrdImagemProduto", entity.Produto.PrdImagemProduto.ToNullString()),
                //dados da loja.
                new SqlParameter("@NOME_LOJA", entity.Loja.LojaNome.ToNullString()),
                new SqlParameter("@CODIGO_LOJA", entity.Loja.LojaCodigo),
                new SqlParameter("@MKTPLACE", entity.Loja.LojaMarketPlace),

            };

            await DataContext.Database.ExecuteSqlCommandAsync("exec AvaliacaoProdutoAdicionar @RETURN_VALUE OUTPUT, @APIKey,@ClienteNome,@ClienteEmail,@ClienteLocalizacao,@ClienteTitulo,@ClienteComentario,@ClienteClassificacao,@ClienteRecomenda,@ClienteVerificado,@PrdNome,@PrdCodigo,@PrdDepartamento,@PrdCategoria,@PrdSubCategoria,@PrdMarca,@PrdLink,@PrdImagemProduto,@NOME_LOJA,@CODIGO_LOJA,@MKTPLACE", xParams);

            object idRetorno = ((SqlParameter)xParams[0]).Value;

            if (!idRetorno.Equals(DBNull.Value)) return (int)idRetorno;

            return 0;


        }
        /// <summary>
        /// Adiciona avaliação de loja
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> EnviarAvaliacaoLoja(EnviarAvaliacaoLojaRQ entity)
        {

            object[] xParams = {
                new SqlParameter("@RETURN_VALUE",DbType.Int32) { Direction = ParameterDirection.Output},
                new SqlParameter("@APIKey", entity.Authorization),
                new SqlParameter("@ClienteNome", entity.Avaliacao.ClienteNome),
                new SqlParameter("@ClienteEmail", entity.Avaliacao.ClienteEmail),
                new SqlParameter("@ClienteLocalizacao", entity.Avaliacao.ClienteLocalizacao.ToNullString()),
                new SqlParameter("@ClienteTitulo", entity.Avaliacao.ClienteTitulo),
                new SqlParameter("@ClienteComentario", entity.Avaliacao.ClienteComentario),
                new SqlParameter("@ClienteClassificacao", entity.Avaliacao.ClienteClassificacao),
                new SqlParameter("@ClienteRecomenda", entity.Avaliacao.ClienteRecomenda),
                new SqlParameter("@ClienteVerificado", false),
                new SqlParameter("@NOME_LOJA", entity.Loja.LojaNome.ToNullString()),
                new SqlParameter("@CODIGO_LOJA", entity.Loja.LojaCodigo),
                new SqlParameter("@MKTPLACE", entity.Loja.LojaMarketPlace),
            };

            await DataContext.Database.ExecuteSqlCommandAsync("exec AvaliacaoLojaAdicionar @RETURN_VALUE OUTPUT,@APIKey,@ClienteNome,@ClienteEmail,@ClienteLocalizacao,@ClienteTitulo,@ClienteComentario,@ClienteClassificacao,@ClienteRecomenda,@ClienteVerificado,@NOME_LOJA,@CODIGO_LOJA,@MKTPLACE ", xParams);

            object idRetorno = ((SqlParameter)xParams[0]).Value;

            if (!idRetorno.Equals(DBNull.Value)) return (int)idRetorno;

            return 0;


        }




        //public async Task AtualizaTotaisProduto(string codProduto, int idCliente)
        //{

        //    using (var ctx = new DataBaseContext())
        //    {
        //        object[] xParams = {
        //            new SqlParameter("@codProduto" , codProduto),
        //            new SqlParameter("@idCliente" , idCliente)
        //        };

        //        await ctx.Database.ExecuteSqlCommandAsync("exec AtualizaTotaisAvaliacao @codProduto, @idCliente", xParams);

        //    }
        //}

        public async Task AtualizaAvaliacaoQtdes(int idReview, TipoReportIntegracaoEnum tipoReport)
        {

            using (var ctx = new DataBaseContext())
            {
                object[] xParams = {
                    new SqlParameter("@idReview" , idReview),
                    new SqlParameter("@tipoReport" , (int)tipoReport)
                };

                await ctx.Database.ExecuteSqlCommandAsync("exec AvaliacaoAtualizarQtdes @idReview ,@tipoReport ", xParams);
            }
        }
        
       

        /// <summary>
        /// Usado na listagem de avaliacoes de produtos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ListarAvaliacoesProdutosRS> AvaliacoesProdutosListar(ListarAvaliacoesProdutosRQ request)
        {

            var avaliacoes = new ListarAvaliacoesProdutosRS();
            avaliacoes.Mensagens = new List<Mensagem>();
            avaliacoes.ProtocoloRetorno = Guid.NewGuid();
            avaliacoes.Loja = new LojaDTO();
            avaliacoes.Produto = new ProdutoDTO();
            avaliacoes.AvaliacaoGeral = new AvaliacaoTotaisDTO();
            avaliacoes.Avaliacoes = new List<AvaliacaoDTO>();
            avaliacoes.AvaliacaoNegativaMaisUtil = new AvaliacaoDTO();
            avaliacoes.AvaliacaoPositivaMaisUtil = new AvaliacaoDTO();
            avaliacoes.Pagination = new PaginationDTO();

            using (var ctx = new DataBaseContext())
            {


                object[] xParams = {
                            new SqlParameter("@APIKey", request.Authorization),
                            new SqlParameter("@CodProduto",request.PrdCodigo),
                            new SqlParameter("@CodLoja",request.CodigoLoja),
                            new SqlParameter("PageNumber",request.ActualPageNumber),
                            new SqlParameter("@Filtro",request.Filtro.ToNullString()),
                            new SqlParameter("@Busca",request.Busca.ToNullString())
                    };

                var db = await ctx.Database.SqlQuery<AvalialiacaoListarPaginaDataMapper>("exec AvaliacaoProdutoListar @APIKey,@CodProduto,@CodLoja,@PageNumber,@Filtro,@Busca", xParams).ToListAsync();

                if (db.Any())
                {
                    avaliacoes.Loja.LojaCodigo = request.CodigoLoja;
                    avaliacoes.Loja.LojaNome = db.FirstOrDefault().LojaNome;
                    avaliacoes.Produto.PrdCodigo = request.PrdCodigo;
                    avaliacoes.Produto.PrdNome = db.FirstOrDefault().PrdNome;
                    avaliacoes.AvaliacaoGeral.TotalAvaliacoes = db.FirstOrDefault().totalRows;
                    avaliacoes.AvaliacaoGeral.Total1Estrela = db.FirstOrDefault().S1;
                    avaliacoes.AvaliacaoGeral.Total2Estrela = db.FirstOrDefault().S2;
                    avaliacoes.AvaliacaoGeral.Total3Estrela = db.FirstOrDefault().S3;
                    avaliacoes.AvaliacaoGeral.Total4Estrela = db.FirstOrDefault().S4;
                    avaliacoes.AvaliacaoGeral.Total5Estrela = db.FirstOrDefault().S5;
                    avaliacoes.AvaliacaoGeral.TotalRecomendaProduto = db.FirstOrDefault().TotalRecomenda;
                    avaliacoes.AvaliacaoGeral.AvaliacaoGeral = Math.Round(db.FirstOrDefault().AvaliacaoGeral, 1);
                    avaliacoes.AvaliacaoGeral.PercentualRecomenda = (avaliacoes.AvaliacaoGeral.TotalRecomendaProduto > 0? Math.Round(100 * avaliacoes.AvaliacaoGeral.TotalRecomendaProduto / avaliacoes.AvaliacaoGeral.TotalAvaliacoes):0);

                    avaliacoes.Pagination.TotalRows = db.FirstOrDefault().totalRows;
                    avaliacoes.Pagination.ActualPageNumber = request.ActualPageNumber;
                    avaliacoes.Pagination.RowsPerPage = (int)Math.Ceiling(avaliacoes.Pagination.TotalRows / (double)10);
                    avaliacoes.Pagination.BtnPaginationName = "btnAvalicaoGetPage";

                    if (db.FirstOrDefault().AP_IDREVIEW > 0 && db.FirstOrDefault().ANP_IDREVIEW > 0)
                    {
                        avaliacoes.AvaliacaoPositivaMaisUtil.IdAvaliacao = db.FirstOrDefault().AP_IDREVIEW;
                        avaliacoes.AvaliacaoPositivaMaisUtil.ClienteNome = db.FirstOrDefault().AP_NOME;
                        avaliacoes.AvaliacaoPositivaMaisUtil.ClienteComentario = db.FirstOrDefault().AP_COMENT;
                        avaliacoes.AvaliacaoPositivaMaisUtil.ClienteClassificacao = db.FirstOrDefault().AP_CLASS;

                        avaliacoes.AvaliacaoNegativaMaisUtil.IdAvaliacao = db.FirstOrDefault().ANP_IDREVIEW;
                        avaliacoes.AvaliacaoNegativaMaisUtil.ClienteNome = db.FirstOrDefault().ANP_NOME;
                        avaliacoes.AvaliacaoNegativaMaisUtil.ClienteComentario = db.FirstOrDefault().ANP_COMENT;
                        avaliacoes.AvaliacaoNegativaMaisUtil.ClienteClassificacao = db.FirstOrDefault().ANP_CLASS;
                    }

                    foreach (var item in db)
                    {
                        avaliacoes.Avaliacoes.Add(new AvaliacaoDTO
                        {
                            IdAvaliacao = item.IdAvaliacao,
                            ClienteClassificacao = item.ClienteClassificacao,
                            ClienteTitulo = item.ClienteTitulo,
                            ClienteComentario = item.ClienteComentario,
                            DataAvaliacao = item.ClienteDataAvaliacao,
                            ClienteLocalizacao = item.ClienteLocalizacao,
                            ClienteNome = item.ClienteNome,
                            ClienteRecomenda = item.ClienteRecomenda,
                            ClienteVerificado = item.ClienteVerificado,                            
                            QtdAjudou = item.QtdAjudou,
                            QtdNaoAjudou = item.QtdNaoAjudou
                        });
                    }
                }

            }
            return avaliacoes;
        }
        /// <summary>
        /// usado na avaliacoes de lojas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ListarAvaliacoesLojasRS> AvaliacoesLojasListar(ListarAvaliacoesLojasRQ request)
        {
            var avaliacoes = new ListarAvaliacoesLojasRS();
            avaliacoes.Mensagens = new List<Mensagem>();
            avaliacoes.ProtocoloRetorno = Guid.NewGuid();
            avaliacoes.Loja = new LojaDTO();
            avaliacoes.AvaliacaoGeral = new AvaliacaoTotaisDTO();
            avaliacoes.Avaliacoes = new List<AvaliacaoDTO>();
            avaliacoes.AvaliacaoNegativaMaisUtil = new AvaliacaoDTO();
            avaliacoes.AvaliacaoPositivaMaisUtil = new AvaliacaoDTO();
            avaliacoes.Pagination = new PaginationDTO();

            using (var ctx = new DataBaseContext())
            {


                object[] xParams = {
                            new SqlParameter("@APIKey", request.Authorization),
                            new SqlParameter("@CodLoja",request.CodigoLoja),
                            new SqlParameter("PageNumber",request.ActualPageNumber),
                            new SqlParameter("@Filtro",request.Filtro.ToNullString()),
                            new SqlParameter("@Busca",request.Busca.ToNullString())
                    };

                var db = await ctx.Database.SqlQuery<AvalialiacaoListarPaginaDataMapper>("exec AvaliacaoLojaListar @APIKey,@CodLoja,@PageNumber,@Filtro,@Busca", xParams).ToListAsync();

                if (db.Any())
                {
                    //avaliacoes.Loja.IdCliente = request.CodCliente;
                    avaliacoes.Loja.LojaCodigo = request.CodigoLoja;
                    avaliacoes.Loja.LojaNome = db.FirstOrDefault().LojaNome;
                    avaliacoes.AvaliacaoGeral.TotalAvaliacoes = db.FirstOrDefault().totalRows;
                    avaliacoes.AvaliacaoGeral.Total1Estrela = db.FirstOrDefault().S1;
                    avaliacoes.AvaliacaoGeral.Total2Estrela = db.FirstOrDefault().S2;
                    avaliacoes.AvaliacaoGeral.Total3Estrela = db.FirstOrDefault().S3;
                    avaliacoes.AvaliacaoGeral.Total4Estrela = db.FirstOrDefault().S4;
                    avaliacoes.AvaliacaoGeral.Total5Estrela = db.FirstOrDefault().S5;
                    avaliacoes.AvaliacaoGeral.TotalRecomendaProduto = db.FirstOrDefault().TotalRecomenda;
                    avaliacoes.AvaliacaoGeral.PercentualRecomenda = db.FirstOrDefault().PercentualRecomenda;

                    //paginacao
                    avaliacoes.Pagination.TotalRows = db.FirstOrDefault().totalRows;
                    avaliacoes.Pagination.ActualPageNumber = request.ActualPageNumber;
                    avaliacoes.Pagination.RowsPerPage = (int)Math.Ceiling(avaliacoes.Pagination.TotalRows / (double)10);
                    avaliacoes.Pagination.BtnPaginationName = "btnAvalicaoGetPage";

                    if (db.FirstOrDefault().AP_IDREVIEW > 0 && db.FirstOrDefault().ANP_IDREVIEW > 0)
                    {
                        avaliacoes.AvaliacaoPositivaMaisUtil.IdAvaliacao = db.FirstOrDefault().AP_IDREVIEW;
                        avaliacoes.AvaliacaoPositivaMaisUtil.ClienteNome = db.FirstOrDefault().AP_NOME;
                        avaliacoes.AvaliacaoPositivaMaisUtil.ClienteComentario = db.FirstOrDefault().AP_COMENT;
                        avaliacoes.AvaliacaoPositivaMaisUtil.ClienteClassificacao = db.FirstOrDefault().AP_CLASS;

                        avaliacoes.AvaliacaoNegativaMaisUtil.IdAvaliacao = db.FirstOrDefault().ANP_IDREVIEW;
                        avaliacoes.AvaliacaoNegativaMaisUtil.ClienteNome = db.FirstOrDefault().ANP_NOME;
                        avaliacoes.AvaliacaoNegativaMaisUtil.ClienteComentario = db.FirstOrDefault().ANP_COMENT;
                        avaliacoes.AvaliacaoNegativaMaisUtil.ClienteClassificacao = db.FirstOrDefault().ANP_CLASS;
                    }

                    foreach (var item in db)
                    {
                        avaliacoes.Avaliacoes.Add(new AvaliacaoDTO
                        {
                            ClienteClassificacao = item.ClienteClassificacao,
                            ClienteTitulo = item.ClienteTitulo,
                            ClienteComentario = item.ClienteComentario,
                            DataAvaliacao = item.ClienteDataAvaliacao,
                            ClienteLocalizacao = item.ClienteLocalizacao,
                            ClienteNome = item.ClienteNome,
                            ClienteRecomenda = item.ClienteRecomenda,
                            ClienteVerificado = item.ClienteVerificado,
                            IdAvaliacao = item.IdAvaliacao,
                            QtdAjudou = item.QtdAjudou,
                            QtdNaoAjudou = item.QtdNaoAjudou
                        });
                    }
                }

            }
            return avaliacoes;
        }


        #endregion



        #region MODERAÇÃO DE PRODUTOS E LOJAS & OPERACOES ADMIN

        public async Task<ModeracaoAutoRS> ListarAvaliacoesPendentes(ModeracaoAutoRQ request) {


            var avaliacoes = new ModeracaoAutoRS();
            avaliacoes.Avaliacoes = new List<AvaliacaoDTO>();

            using (var ctx = new DataBaseContext())
            {
                object[] xParams = {new SqlParameter("@APIKey", request.Token)};

                var db = await ctx.Database.SqlQuery<AvalialiacaoListarPaginaDataMapper>("exec AvaliacaoModeracaoListar @APIKey", xParams).ToListAsync();

                if (db.Any())
                {

                    foreach (var item in db)
                    {
                        avaliacoes.Avaliacoes.Add(                        
                            new AvaliacaoDTO
                            {
                                IdAvaliacao = item.IdAvaliacao,
                                DataAvaliacao = item.ClienteDataAvaliacao,
                                ClienteClassificacao = item.ClienteClassificacao,
                                ClienteTitulo = item.ClienteTitulo,
                                ClienteComentario = item.ClienteComentario,                                
                                ClienteLocalizacao = item.ClienteLocalizacao,
                                ClienteNome = item.ClienteNome,
                                ClienteEmail = item.ClienteEmail,
                                ClienteRecomenda = item.ClienteRecomenda,
                                ClienteVerificado = item.ClienteVerificado,
                                QtdAjudou = item.QtdAjudou,
                                QtdNaoAjudou = item.QtdNaoAjudou
                        });
                    }
                }

            }
            return avaliacoes;

        }
        public async Task AlterarStatusAvaliacao(int idAvaliacao, StatusAvaliacaoEnum idStatusAvaliacao, string motivo)
        {
            using (var ctx = new DataBaseContext())
            {
                object[] xParams = {
                    new SqlParameter("@IdAvaliacao" , idAvaliacao),
                    new SqlParameter("@IdAvaliacaoStatus" , idStatusAvaliacao),
                    new SqlParameter("@Motivo" , motivo)
                };

                await ctx.Database.ExecuteSqlCommandAsync("exec AvaliacaoStatusAlterar @IdAvaliacao,@IdAvaliacaoStatus, @Motivo", xParams);

            }
        }
        public async Task<ModeracaoProdutoRS> ModeracaoProdutosListar(ModeracaoProdutoRQ request)
        {
            var avaliacoes = new ModeracaoProdutoRS();
            avaliacoes.Mensagens = new List<Mensagem>();
            avaliacoes.ProtocoloRetorno = Guid.NewGuid();
            avaliacoes.Pagination = new PaginationDTO();
            avaliacoes.Avaliacoes = new List<ModeracaoProdutoAvaliacaoDTO>();

            using (var ctx = new DataBaseContext())
            {
                object[] xParams = {
                            new SqlParameter("@IdCliente", request.IdCliente),
                            new SqlParameter("@PageNumber",request.ActualPageNumber),
                            new SqlParameter("@Busca",request.Busca.IfEmptyOrWhiteSpace("")),
                            new SqlParameter("@IdAvaliacaoStatus",StatusAvaliacaoEnum.PendenteRevisao)
                    };

                var db = await ctx.Database.SqlQuery<AvalialiacaoListarPaginaDataMapper>("exec AvaliacaoProdutoListarAdmin @IdCliente, @PageNumber, @Busca, @IdAvaliacaoStatus", xParams).ToListAsync();

                if (db.Any())
                {
                    avaliacoes.Pagination.TotalRows = db.FirstOrDefault().totalRows;
                    avaliacoes.Pagination.ActualPageNumber = request.ActualPageNumber;
                    avaliacoes.Pagination.RowsPerPage = (int)Math.Ceiling(avaliacoes.Pagination.TotalRows / (double)25);
                    avaliacoes.Pagination.BtnPaginationName = "btnAvalicaoGetPage";


                    foreach (var item in db)
                    {
                        avaliacoes.Avaliacoes.Add(new ModeracaoProdutoAvaliacaoDTO
                        {
                            Avaliacao = new AvaliacaoDTO
                            {
                                IdAvaliacao = item.IdAvaliacao,
                                ClienteClassificacao = item.ClienteClassificacao,
                                ClienteTitulo = item.ClienteTitulo,
                                ClienteComentario = item.ClienteComentario,
                                DataAvaliacao = item.ClienteDataAvaliacao,
                                ClienteLocalizacao = item.ClienteLocalizacao,
                                ClienteNome = item.ClienteNome,
                                ClienteEmail = item.ClienteEmail,
                                ClienteRecomenda = item.ClienteRecomenda,
                                ClienteVerificado = item.ClienteVerificado,
                                QtdAjudou = item.QtdAjudou,
                                QtdNaoAjudou = item.QtdNaoAjudou
                            },
                            Loja = new LojaDTO
                            {
                                IdCliente = request.IdCliente,
                                LojaCodigo = item.LojaCodigo,
                                LojaMarketPlace = item.LojaMarketPlace,
                                LojaNome = item.LojaNome
                            },
                            Produto = new ProdutoDTO
                            {
                                PrdNome = item.PrdNome,
                                PrdCodigo = item.PrdCodigo,
                                PrdLink = item.PrdLink
                            }
                        });


                    }
                }

            }
            return avaliacoes;

        }
        public async Task<ModeracaoLojaRS> ModeracaoLojasListar(ModeracaoLojaRQ request)
        {
            var avaliacoes = new ModeracaoLojaRS();
            avaliacoes.Mensagens = new List<Mensagem>();
            avaliacoes.ProtocoloRetorno = Guid.NewGuid();
            avaliacoes.Pagination = new PaginationDTO();
            avaliacoes.Avaliacoes = new List<ModeracaoLojaAvaliacaoDTO>();

            using (var ctx = new DataBaseContext())
            {
                object[] xParams = {
                            new SqlParameter("@IdCliente", request.IdCliente),
                            new SqlParameter("@ActualPageNumber",request.ActualPageNumber),
                            new SqlParameter("@Busca",request.Busca.IfEmptyOrWhiteSpace("")),
                            new SqlParameter("@IdAvaliacaoStatus", StatusAvaliacaoEnum.PendenteRevisao)
                    };

                var db = await ctx.Database.SqlQuery<AvalialiacaoListarPaginaDataMapper>("exec AvaliacaoLojaListarAdmin @IdCliente, @ActualPageNumber, @Busca, @IdAvaliacaoStatus", xParams).ToListAsync();

                if (db.Any())
                {
                    avaliacoes.Pagination.TotalRows = db.FirstOrDefault().totalRows;
                    avaliacoes.Pagination.ActualPageNumber = request.ActualPageNumber;
                    avaliacoes.Pagination.RowsPerPage = (int)Math.Ceiling(avaliacoes.Pagination.TotalRows / (double)25);
                    avaliacoes.Pagination.BtnPaginationName = "btnAvalicaoGetPage";


                    foreach (var item in db)
                    {
                        avaliacoes.Avaliacoes.Add(new ModeracaoLojaAvaliacaoDTO
                        {
                            Avaliacao = new AvaliacaoDTO
                            {
                                IdAvaliacao = item.IdAvaliacao,
                                ClienteClassificacao = item.ClienteClassificacao,
                                ClienteTitulo = item.ClienteTitulo,
                                ClienteComentario = item.ClienteComentario,
                                DataAvaliacao = item.ClienteDataAvaliacao,
                                ClienteLocalizacao = item.ClienteLocalizacao,
                                ClienteNome = item.ClienteNome,
                                ClienteEmail = item.ClienteEmail,
                                ClienteRecomenda = item.ClienteRecomenda,
                                ClienteVerificado = item.ClienteVerificado,
                                QtdAjudou = item.QtdAjudou,
                                QtdNaoAjudou = item.QtdNaoAjudou
                            },
                            Loja = new LojaDTO
                            {
                                IdCliente = request.IdCliente,
                                LojaCodigo = item.LojaCodigo,
                                LojaMarketPlace = item.LojaMarketPlace,
                                LojaNome = item.LojaNome
                            }
                        });


                    }
                }

            }
            return avaliacoes;
        }

        public async Task<ModeracaoProdutoRS> AvaliacoesAdminProdutosListar(ModeracaoProdutoRQ request)
        {
            var avaliacoes = new ModeracaoProdutoRS();
            avaliacoes.Mensagens = new List<Mensagem>();
            avaliacoes.ProtocoloRetorno = Guid.NewGuid();
            avaliacoes.Pagination = new PaginationDTO();
            avaliacoes.Avaliacoes = new List<ModeracaoProdutoAvaliacaoDTO>();

            using (var ctx = new DataBaseContext())
            {
                object[] xParams = {
                            new SqlParameter("@IdCliente", request.IdCliente),
                            new SqlParameter("@ActualPageNumber",request.ActualPageNumber),
                            new SqlParameter("@Busca",request.Busca.IfEmptyOrWhiteSpace("")),
                            new SqlParameter("@IdAvaliacaoStatus",StatusAvaliacaoEnum.Aprovado),                            
                    };

                var db = await ctx.Database.SqlQuery<AvalialiacaoListarPaginaDataMapper>("exec AvaliacaoProdutoListarAdmin @IdCliente, @ActualPageNumber, @Busca , @IdAvaliacaoStatus", xParams).ToListAsync();

                if (db.Any())
                {
                    avaliacoes.Pagination.TotalRows = db.FirstOrDefault().totalRows;
                    avaliacoes.Pagination.ActualPageNumber = request.ActualPageNumber;
                    avaliacoes.Pagination.RowsPerPage = (int)Math.Ceiling(avaliacoes.Pagination.TotalRows / (double)25);
                    avaliacoes.Pagination.BtnPaginationName = "btnAvalicaoGetPage";


                    foreach (var item in db)
                    {
                        avaliacoes.Avaliacoes.Add(new ModeracaoProdutoAvaliacaoDTO
                        {
                            Avaliacao = new AvaliacaoDTO
                            {
                                IdAvaliacao = item.IdAvaliacao,
                                ClienteClassificacao = item.ClienteClassificacao,
                                ClienteTitulo = item.ClienteTitulo,
                                ClienteComentario = item.ClienteComentario,
                                DataAvaliacao = item.ClienteDataAvaliacao,
                                ClienteLocalizacao = item.ClienteLocalizacao,
                                ClienteNome = item.ClienteNome,
                                ClienteEmail = item.ClienteEmail,
                                ClienteRecomenda = item.ClienteRecomenda,
                                ClienteVerificado = item.ClienteVerificado,
                                QtdAjudou = item.QtdAjudou,
                                QtdNaoAjudou = item.QtdNaoAjudou
                            },
                            Loja = new LojaDTO
                            {
                                IdCliente = request.IdCliente,
                                LojaCodigo = item.LojaCodigo,
                                LojaMarketPlace = item.LojaMarketPlace,
                                LojaNome = item.LojaNome
                            },
                            Produto = new ProdutoDTO
                            {
                                PrdNome = item.PrdNome,
                                PrdCodigo = item.PrdCodigo,
                                PrdLink = item.PrdLink
                            }
                        });


                    }
                }

            }
            return avaliacoes;

        }
        public async Task<ModeracaoLojaRS> AvaliacoesAdminLojasListar(ModeracaoLojaRQ request)
        {
            var avaliacoes = new ModeracaoLojaRS();
            avaliacoes.Mensagens = new List<Mensagem>();
            avaliacoes.ProtocoloRetorno = Guid.NewGuid();
            avaliacoes.Pagination = new PaginationDTO();
            avaliacoes.Avaliacoes = new List<ModeracaoLojaAvaliacaoDTO>();

            using (var ctx = new DataBaseContext())
            {
                object[] xParams = {
                            new SqlParameter("@IdCliente", request.IdCliente),
                            new SqlParameter("@ActualPageNumber",request.ActualPageNumber),
                            new SqlParameter("@Busca",request.Busca.IfEmptyOrWhiteSpace("")),
                            new SqlParameter("@IdAvaliacaoStatus", StatusAvaliacaoEnum.Aprovado)
                    };

                var db = await ctx.Database.SqlQuery<AvalialiacaoListarPaginaDataMapper>("exec AvaliacaoLojaListarAdmin @IdCliente, @ActualPageNumber, @Busca, @IdAvaliacaoStatus", xParams).ToListAsync();

                if (db.Any())
                {
                    avaliacoes.Pagination.TotalRows = db.FirstOrDefault().totalRows;
                    avaliacoes.Pagination.ActualPageNumber = request.ActualPageNumber;
                    avaliacoes.Pagination.RowsPerPage = (int)Math.Ceiling(avaliacoes.Pagination.TotalRows / (double)25);
                    avaliacoes.Pagination.BtnPaginationName = "btnAvalicaoGetPage";


                    foreach (var item in db)
                    {
                        avaliacoes.Avaliacoes.Add(new ModeracaoLojaAvaliacaoDTO
                        {
                            Avaliacao = new AvaliacaoDTO
                            {
                                IdAvaliacao = item.IdAvaliacao,
                                ClienteClassificacao = item.ClienteClassificacao,
                                ClienteTitulo = item.ClienteTitulo,
                                ClienteComentario = item.ClienteComentario,
                                DataAvaliacao = item.ClienteDataAvaliacao,
                                ClienteLocalizacao = item.ClienteLocalizacao,
                                ClienteNome = item.ClienteNome,
                                ClienteEmail = item.ClienteEmail,
                                ClienteRecomenda = item.ClienteRecomenda,
                                ClienteVerificado = item.ClienteVerificado,
                                QtdAjudou = item.QtdAjudou,
                                QtdNaoAjudou = item.QtdNaoAjudou
                            },
                            Loja = new LojaDTO
                            {
                                IdCliente = request.IdCliente,
                                LojaCodigo = item.LojaCodigo,
                                LojaMarketPlace = item.LojaMarketPlace,
                                LojaNome = item.LojaNome
                            }
                        });


                    }
                }

            }
            return avaliacoes;
        }
        #endregion



        #region OPERACOES GENERICAS

       
        public Avaliacao Add(Avaliacao entity)
        {
            DataContext.Set<Avaliacao>().Add(entity);
            DataContext.SaveChanges();
            return entity;
        }

        public async Task<Avaliacao> AddAsync(Avaliacao entity)
        {
            DataContext.Set<Avaliacao>().Add(entity);
            await DataContext.SaveChangesAsync();
            return entity;
        }


        public void Delete(Avaliacao entity)
        {
            DataContext.Set<Avaliacao>().Remove(entity);
            DataContext.SaveChanges();
        }

        public async Task DeleteAsync(Avaliacao entity)
        {
            DataContext.Set<Avaliacao>().Remove(entity);
            await DataContext.SaveChangesAsync();
        }

        public Avaliacao Edit(Avaliacao entity, int key)
        {
            if (entity == null)
                return null;

            Avaliacao existing = DataContext.Set<Avaliacao>().Find(key);
            if (existing != null)
            {
                DataContext.Entry(existing).CurrentValues.SetValues(entity);
                DataContext.SaveChanges();
            }
            return existing;
        }

        public async Task<Avaliacao> EditAsync(Avaliacao entity, int key)
        {
            if (entity == null)
                return null;

            Avaliacao existing = DataContext.Set<Avaliacao>().Find(key);
            if (existing != null)
            {
                DataContext.Entry(existing).CurrentValues.SetValues(entity);
                await DataContext.SaveChangesAsync();
            }
            return existing;
        }

        public Avaliacao Find(Expression<Func<Avaliacao, bool>> predicate)
        {
            return DataContext.Set<Avaliacao>().SingleOrDefault(predicate);
        }

        public async Task<Avaliacao> FindAsync(Expression<Func<Avaliacao, bool>> predicate)
        {
            return await DataContext.Set<Avaliacao>().SingleOrDefaultAsync(predicate);
        }

        public ICollection<Avaliacao> GetAll()
        {
            return DataContext.Set<Avaliacao>().ToList();
        }

        public async Task<ICollection<Avaliacao>> GetAllAsync()
        {
            return await DataContext.Set<Avaliacao>().ToListAsync();
        }

        public Avaliacao GetById(int id)
        {
            return DataContext.Set<Avaliacao>().Find(id);
        }

        public async Task<Avaliacao> GetByIdAsync(int id)
        {
            return await DataContext.Set<Avaliacao>().FindAsync(id);
        }

        public async Task<ICollection<Avaliacao>> FindAllAsync(Expression<Func<Avaliacao, bool>> match)
        {
            return await DataContext.Set<Avaliacao>().Where(match).ToListAsync();
        }
        #endregion



    }
}
