using AutoMapper;
using BetaViews.Admin.App_Start;
using BetaViews.Admin.Filters;
using BetaViews.Core.DataBase.Entitys;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Core.Framework;
using BetaViews.Messages.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace BetaViews.Admin.Controllers.Cadastro
{

    [CustomAuthorize(PaginaAcessoEnum.Cadastro_CadastroDeUsuarios)]
    public class Cadastro_CadastroDeUsuariosController : Controller
    {
        public IMapper Mapper;
        private readonly ClienteAcessoModel clienteAcessoModel;
        private readonly IClienteAcessoRepository clienteAcessoRepo;
        private readonly ILojaRepository lojaRepo;
        public Cadastro_CadastroDeUsuariosController(IClienteAcessoRepository clienteAcessoRepo, ClienteAcessoModel clienteAcessoModel, ILojaRepository lojaRepo)
        {
            this.clienteAcessoRepo = clienteAcessoRepo;
            this.clienteAcessoModel = clienteAcessoModel;
            this.lojaRepo = lojaRepo;
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }
        [OutputCache(Duration = 360, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
        public async Task<ActionResult> Index(int? page, string Busca, string currentFilter)
        {            
            if (Busca != null)
                page = 1;
            else            
                Busca = currentFilter;
                        

            var db = await clienteAcessoRepo.FindAllAsync(x => x.IdCliente == AppUserManager.Usuario.IdCliente && x.FlagStatus);
            //var model = clienteAcessoModel.MapperDBToModelToList(db.ToList(), clienteAcessoModel);
            var model = Mapper.Map<List<ClienteAcessoModel>>(db.ToList());


            if (!string.IsNullOrEmpty(Busca))
            {
                model = model.Where(s => s.Nome.Contains(Busca) || s.Email.Contains(Busca)).ToList();
            }

            ViewBag.CurrentFilter = Busca;

            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }

        public async Task<ActionResult> Create()
        {
            var model = new ClienteAcessoModel();
            model.PaginaAcesso = new List<PaginaAcessoModel>();

            await CheckBoxPaginasAcesso(model);

            await DropDownLojasAcesso();

            CriacaoDeUsuarioNovo(model);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClienteAcessoModel model)
        {
            await CheckBoxPaginasAcesso(model);
            await DropDownLojasAcesso();

            try
            {

                if (model.Email != null)
                { 
                    var doubleCheckEmail = clienteAcessoRepo.GetAll().Where(x => x.IdCliente == AppUserManager.Usuario.IdCliente && x.Email.ToLower() == model.Email.ToLower() && x.FlagStatus);
                    if (doubleCheckEmail.Any())
                    {
                        ModelState.AddModelError("Email", "Já existe este e-mail cadastrado com outro usuário.");
                        return View(model);
                    }
                }

                if (!model.UsuarioMaster)
                {
                    if (model.IdsClienteAcessoPerfil == null)
                    {
                        ModelState.AddModelError("IdsClienteAcessoPerfil", "Você precisa selecionar pelo menos uma página de acesso para o usuário.");
                        return View(model);
                    }
                }
                else
                {
                    //se não selecionado é um usuário master e term acesso full.
                    model.IdsClienteAcessoPerfil = model.PaginaAcesso.Select(x => x.Id).ToArray();
                }

                if (ModelState.IsValid)
                {
                    var novoUsuario = clienteAcessoRepo.Add(new ClienteAcesso
                    {
                        IdCliente = model.IdCliente,
                        Nome = model.Nome,
                        Email = model.Email,
                        Senha = model.Senha,
                        Departamento = model.Departamento,
                        DtUltimoAcesso = model.DtUltimoAcesso,
                        FlagStatus = model.FlagStatus,
                        PrimeiroAcesso = model.PrimeiroAcesso,
                        UsuarioMaster = model.UsuarioMaster
                    });

                    //ClienteAcessoLoja
                    if (novoUsuario.Id > 0)
                    {
                        await clienteAcessoRepo.RemoverClienteAcessoLoja(model.Id);

                        //restringe usuário para lojas especificas
                        if (model.IdsClienteAcessoLoja != null && model.IdsClienteAcessoLoja.Any())
                        {
                            await clienteAcessoRepo.AdicionarClienteAcessoLoja(novoUsuario.Id, model.IdsClienteAcessoLoja);
                        }
                        else
                        {
                            //se nulo, então tem acesso a todas as lojas e é necessário adicionar.
                            var idsLojas = await lojaRepo.FindAllAsync(x => x.IdCliente == 1);
                            if (idsLojas.Any())
                            {
                                await clienteAcessoRepo.AdicionarClienteAcessoLoja(novoUsuario.Id, idsLojas.Select(x => x.Id).ToArray());
                            }
                        }
                    }

                    //paginas de acesso
                    //ClienteAcessoPerfil
                    await clienteAcessoRepo.AdicionarClienteAcessoPerfilPaginas(model.IdsClienteAcessoPerfil.Distinct().ToArray(), novoUsuario.Id);

                    //paginas de acesso
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View(model);
            }

            return View(model);
        }


        public async Task<ActionResult> Edit(int id)
        {
            var model = new ClienteAcessoModel();
            model.PaginaAcesso = new List<PaginaAcessoModel>();

            await DropDownLojasAcesso();
            await CheckBoxPaginasAcesso(model);


            var usuarioDB = await clienteAcessoRepo.GetByIdAsync(id);

            if (usuarioDB != null)
            {
                model = Mapper.Map(usuarioDB, model); //model.MapperDBToModel(usuarioDB, model);


                //paginas que o usuario temn acesso
                var idsPagina = await clienteAcessoRepo.PaginasAcessoListAsync(model.Id);
                if (idsPagina.Any())
                {
                    model.IdsClienteAcessoPerfil = idsPagina.Select(x => x.Id).ToArray();
                }

                var idsLoja = await clienteAcessoRepo.ClienteAcessoLojaListAsync(model.Id);
                if (idsLoja.Any())
                {
                    model.IdsClienteAcessoLoja = idsLoja.Select(x => x.IdLoja).ToArray();
                }


            }

            return View(model);
        }

        // POST: ClienteAcesso/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(ClienteAcessoModel model)
        {
            await CheckBoxPaginasAcesso(model);
            await DropDownLojasAcesso();

            try
            {

                if (model.Email != null)
                {
                    var doubleCheckEmail = clienteAcessoRepo.GetAll().Where(x => 
                        x.IdCliente == AppUserManager.Usuario.IdCliente
                        && x.Email.ToLower() == model.Email.ToLower() 
                        && x.Id != model.Id && x.FlagStatus);
                    if (doubleCheckEmail.Any())
                    {
                        ModelState.AddModelError("Email", "Já existe este e-mail cadastrado com outro usuário.");
                        return View(model);
                    }
                }
                    

                if (!model.UsuarioMaster)
                {
                    if (!model.IdsClienteAcessoPerfil.Any())
                    {
                        ModelState.AddModelError("IdsClienteAcessoPerfil", "Você precisa selecionar pelo menos um perfil de acesso.");
                        return View(model);
                    }
                }
                else
                {
                    //se não selecionado é um usuário master e term acesso full.
                    model.IdsClienteAcessoPerfil = model.PaginaAcesso.Select(x => x.Id).ToArray();
                }

                if (ModelState.IsValid)
                {

                    var edicaoUsuario = await clienteAcessoRepo.GetByIdAsync(model.Id);
                    edicaoUsuario.IdCliente = model.IdCliente;
                    edicaoUsuario.Nome = model.Nome;
                    edicaoUsuario.Email = model.Email;
                    edicaoUsuario.Departamento = model.Departamento;
                    edicaoUsuario.DtUltimoAcesso = model.DtUltimoAcesso;
                    edicaoUsuario.FlagStatus = model.FlagStatus;
                    edicaoUsuario.PrimeiroAcesso = model.PrimeiroAcesso;
                    edicaoUsuario.UsuarioMaster = model.UsuarioMaster;
                    await clienteAcessoRepo.EditAsync(edicaoUsuario, model.Id);


                    //ClienteAcessoLoja
                    if (edicaoUsuario.Id > 0)
                    {
                        await clienteAcessoRepo.RemoverClienteAcessoLoja(model.Id);

                        //restringe usuário para lojas especificas
                        if (model.IdsClienteAcessoLoja != null && model.IdsClienteAcessoLoja.Any())
                        {
                            await clienteAcessoRepo.AdicionarClienteAcessoLoja(edicaoUsuario.Id, model.IdsClienteAcessoLoja);
                        }
                        //else
                        //{
                        //    //se nulo, então tem acesso a todas as lojas e é necessário adicionar.
                        //    var idsLojas = await lojaRepo.FindAllAsync(x => x.IdCliente == 1);
                        //    if (idsLojas.Any())
                        //    {
                        //        await clienteAcessoRepo.AdicionarClienteAcessoLoja(edicaoUsuario.Id, idsLojas.Select(x => x.Id).ToArray());
                        //    }
                        //}
                    }

                    //paginas de acesso
                    //ClienteAcessoPerfil
                    await clienteAcessoRepo.RemoverClienteAcessoPerfilPaginas(model.Id);
                    await clienteAcessoRepo.AdicionarClienteAcessoPerfilPaginas(model.IdsClienteAcessoPerfil.Distinct().ToArray(), edicaoUsuario.Id);

                    //paginas de acesso
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }

            return View(model);
        }



        private void CriacaoDeUsuarioNovo(ClienteAcessoModel model)
        {
            model.IdCliente = AppUserManager.Usuario.IdCliente;
            model.Senha = "123456";
            model.UsuarioMaster = true;
            model.FlagStatus = true;
            model.PrimeiroAcesso = false;
        }

        private async Task DropDownLojaPrincipal(int idLoja)
        {
            var lojaPrincipalList = await lojaRepo.FindAllAsync(x => x.IdCliente == 1);
            if (lojaPrincipalList.Any())
            {
                ViewBag.lojaPrincipal = new SelectList(lojaPrincipalList.Select(x => new { x.Id, x.Nome }), "Id", "Nome", idLoja);
            }
        }

        private async Task DropDownLojasAcesso()
        {
            var lojaPrincipalList = await lojaRepo.FindAllAsync(x => x.IdCliente == 1);
            if (lojaPrincipalList.Any())
            {
                ViewBag.lojasAcesso = new MultiSelectList(lojaPrincipalList.Select(x => new { x.Id, x.Nome }).ToList(), "Id", "Nome");
            }
        }

        private async Task CheckBoxPaginasAcesso(ClienteAcessoModel model)
        {
            model.PaginaAcesso = new List<PaginaAcessoModel>();

            var paginas = await clienteAcessoRepo.PaginasAcessoListAsync();
            if (paginas.Any())
            {
                paginas.ToList().ForEach(x =>
                {
                    model.PaginaAcesso.Add(new PaginaAcessoModel { Id = x.Id, Nome = x.Nome, Descricao = x.Descricao, Modulo = x.Modulo });
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> SendNewPassword(int id)
        {

            var user = await clienteAcessoRepo.GetByIdAsync(id);

            if (user.Id > 0)
            {
                var html = string.Format("Olá {0}!<br/> <p>Para acessar o sistema de avaliações PushStars, <a href='http://pushstars.effectlab.com.br'>clique aqui!</a> e digite a senha 123456.</p> <br/> Obrigado!!!", user.Nome);

                await MailSend.SendGridAsync(user.Email.Trim(), user.Nome, "Lembrete de senha", html);

                return Json(new { erro = false });
            }
            else
            {
                return Json(new { erro = true });
            }
        }







        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var edicaoUsuario = await clienteAcessoRepo.GetByIdAsync(id);
                edicaoUsuario.FlagStatus = false;
                await clienteAcessoRepo.EditAsync(edicaoUsuario, id);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


    }
}