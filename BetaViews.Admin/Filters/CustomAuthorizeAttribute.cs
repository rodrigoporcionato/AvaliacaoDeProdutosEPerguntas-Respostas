using BetaViews.Messages.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BetaViews.Admin.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        /// <summary>
        /// quando true, indica que deve ir para o logon novamente pq não conseguiu recuperar a sessao ou cookie
        /// </summary>
        public bool RedirecionaLogin { get; set; }        

        private readonly PaginaAcessoEnum paginaAcesso;
        public CustomAuthorizeAttribute(PaginaAcessoEnum pagina)
        {
            paginaAcesso = pagina;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            RedirecionaLogin = false;
            bool authorize = false;

            var clientePerfilAcessoModel = new PerfilAcessoLogado();

            if (httpContext.Request.Cookies["ClientePerfilAcessoCookie"] != null) {
                var serializerUser = new JavaScriptSerializer();
                clientePerfilAcessoModel = serializerUser.Deserialize<PerfilAcessoLogado>(httpContext.Request.Cookies["ClientePerfilAcessoCookie"].Value);
                if(httpContext.Session!=null) httpContext.Session.Add("ClientePerfilAcesso", clientePerfilAcessoModel);
            }
            else if(httpContext.Session["ClientePerfilAcesso"]!=null)
            {
                clientePerfilAcessoModel = (PerfilAcessoLogado)httpContext.Session["ClientePerfilAcesso"];
            }
            else
            {
                RedirecionaLogin = true;
            }

            if (clientePerfilAcessoModel!=null)
            {
                AppUserManager.Usuario = clientePerfilAcessoModel;
            }

            if (clientePerfilAcessoModel.PaginaAcesso!=null && clientePerfilAcessoModel.PaginaAcesso.Where(x=> x == (int)paginaAcesso).Any())
            {
                authorize = true;
            }          
            
            return authorize;
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();


            if (RedirecionaLogin)
            {
                filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary {
                    { "action","login"},
                    { "controller","clienteacesso"}
                }
                );
            }
            else
            {
                //sem acesso a pagina
                filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary {
                    { "action","PaginaNaoAutorizada"},
                    { "controller","clienteacesso"}
                }
                );
            }
            

            

        }
    }
}