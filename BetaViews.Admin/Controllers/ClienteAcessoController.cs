using BetaViews.Admin.Filters;
using BetaViews.Core.DataBase.Repository.Interface;
using BetaViews.Core.Framework.Extension;
using BetaViews.Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BetaViews.Admin.Controllers
{
    public class ClienteAcessoController : Controller
    {
        private readonly ClienteAcessoModel clienteAcessoModel;
        private readonly IClienteAcessoRepository clienteAcessoRepo;
        private readonly ILojaRepository lojaRepo;
        public ClienteAcessoController(IClienteAcessoRepository clienteAcessoRepo, ClienteAcessoModel clienteAcessoModel, ILojaRepository lojaRepo)
        {
            this.clienteAcessoRepo = clienteAcessoRepo;
            this.clienteAcessoModel = clienteAcessoModel;
            this.lojaRepo = lojaRepo;
        }




        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userDBAccess = clienteAcessoRepo.ValidateUser(model.Usuario, model.Senha);
                

                if (userDBAccess !=null)
                {                    
                    var userPerfilAcesso = new PerfilAcessoLogado
                    {
                        IdUsuario = userDBAccess.Id,
                        Nome = userDBAccess.Nome.ToUpperFirstLetter(),
                        Email = userDBAccess.Email,
                        IdCliente = userDBAccess.IdCliente,
                        UsuarioMaster = userDBAccess.UsuarioMaster,
                        LojaRestricao = new List<int>(),
                        PaginaAcesso = new List<int>()
                    };
                    //define as lojas que pode ver
                    if (userDBAccess.ClienteAcessoLoja.Any())
                    {
                        userDBAccess.ClienteAcessoLoja.ToList().ForEach(x =>
                        {
                            userPerfilAcesso.LojaRestricao.Add(x.IdLoja);
                        });
                    }
                    //define as paginas que pode ver.
                    userPerfilAcesso.PaginaAcesso.Add((int)PaginaAcessoEnum.AcessoAutorizado);
                    if (userDBAccess.ClienteAcessoPerfil.Any())
                    {
                        userDBAccess.ClienteAcessoPerfil.ToList().ForEach(x =>
                        {
                            userPerfilAcesso.PaginaAcesso.Add(x.IdPaginaAcesso);                            
                        });
                    }

                    
                    if (model.LembrarSenha)
                    {
                        var serialize = new JavaScriptSerializer();
                        var jsonUser = serialize.Serialize(userPerfilAcesso);
                        LimpaCookies();
                        var cookie = new HttpCookie("ClientePerfilAcessoCookie", jsonUser)
                        {
                            Expires = DateTime.Now.AddDays(15)
                        };
                        HttpContext.Response.Cookies.Add(cookie);                        
                    }
                    //guarda os dados do usuario na sessao e cookie
                    Session["ClientePerfilAcesso"] = userPerfilAcesso;

                    AppUserManager.Usuario = userPerfilAcesso;

                    
                    userDBAccess.PrimeiroAcesso = false;
                    userDBAccess.DtUltimoAcesso = DateTime.Now;
                    clienteAcessoRepo.Edit(userDBAccess, userDBAccess.Id);

                    return RedirectToAction("Index", "Principal_DASHBOARD");
                }
                else
                {
                    ModelState.AddModelError("Usuario","Usuário ou senha invalida.");
                }
            }

            return View(model);
        }





        //[RedirectOnError]
        [HttpGet]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session["ClientePerfilAcesso"] = null;//limpa a sessao
            Session.Abandon();
            LimpaCookies();

            return RedirectToAction("login", "clienteacesso");
        }

        private void LimpaCookies()
        {
            if (Request.Cookies["ClientePerfilAcessoCookie"] != null)
            {
                Response.Cookies["ClientePerfilAcessoCookie"].Expires = DateTime.Now.AddDays(-1);
            }
        }

        [AllowAnonymous]
        public ActionResult PaginaNaoAutorizada()
        {
            return View();
        }


        [CustomAuthorize(PaginaAcessoEnum.AcessoAutorizado)]
        public ActionResult MeuPerfil(int Id) {
            ViewBag.DadosAlterados = false;
            var model = new ClienteAcessoMeuPerfilModel();

            var userDB = clienteAcessoRepo.GetById(Id);

            if (userDB!=null)
            {
                model = new ClienteAcessoMeuPerfilModel
                {
                    IdUsuario = userDB.Id,
                    Nome = userDB.Nome,
                    Email = userDB.Email,
                    Departamento = userDB.Departamento,
                    AlterPassword = false,
                };
            }



            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> MeuPerfil(ClienteAcessoMeuPerfilModel model)
        {
            ViewBag.DadosAlterados = false;

            if (model.AlterPassword)
            {
                var senhaAntiga = await clienteAcessoRepo.GetByIdAsync(model.IdUsuario);
                if (senhaAntiga.Senha!=model.OldPassword )
                {
                    ModelState.AddModelError("OldPassword", "Senha antiga não confere.");                    
                }

                if (model.Password!=model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Senha e confirmação de senha não são iguais.");                    
                }
            }
            else
            {
                ModelState["Password"].Errors.Clear();
                ModelState["OldPassword"].Errors.Clear();
                ModelState["ConfirmPassword"].Errors.Clear();

            }
            if (model.Email != null)
            {
                var doubleCheckEmail = clienteAcessoRepo.GetAll().Where(x => x.Email.ToLower() == model.Email.Trim().ToLower() && x.Id != model.IdUsuario && x.FlagStatus);
                if (doubleCheckEmail.Any())
                {
                    ModelState.AddModelError("Email", "Já existe este e-mail cadastrado com outro usuário.");
                }
            }

            if (ModelState.IsValid)
            {
                    var edicaoUsuario = await clienteAcessoRepo.GetByIdAsync(model.IdUsuario);
                    edicaoUsuario.Nome = model.Nome;
                    edicaoUsuario.Email = model.Email.Trim().ToLower();
                    edicaoUsuario.Departamento = model.Departamento;                    
                    edicaoUsuario.Senha = model.AlterPassword? model.Password.Trim(): edicaoUsuario.Senha;
                    await clienteAcessoRepo.EditAsync(edicaoUsuario, model.IdUsuario);

                    ViewBag.DadosAlterados = true;
            }

            return View(model);            
                
        }

















    }
}
