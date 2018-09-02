using BetaViews.Core.DataBase.Repository;
using BetaViews.Core.DataBase.Repository.Interface;
using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;

namespace BetaViews.API.Filters
{
    /// <summary>
    /// token de autorização
    /// </summary>
    public class APIAuthenticationAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (Authorize(actionContext))
            {
                return;
            }

            HandleUnauthorizedRequest(actionContext);

        }
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var httpResponseError = new System.Net.Http.HttpResponseMessage();
            httpResponseError.StatusCode = System.Net.HttpStatusCode.Unauthorized;
            httpResponseError.Content = new StringContent("Authorization - Token não autorizado");

            throw new HttpResponseException(httpResponseError);

        }

        private bool Authorize(System.Web.Http.Controllers.HttpActionContext actionContext)
        {            
            try
            {
                var container = new UnityContainer();
                container.RegisterType<IClienteRepository, ClienteRepository>();
                var clienteService = container.Resolve<ClienteRepository>();

                var httpRequestHeaderToken = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();

                var token = clienteService.CheckTokenAuthorization(httpRequestHeaderToken);

                if (token)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }























        //    public override void OnAuthorization(HttpActionContext actionContext)
        //    {

        //    try
        //    {

        //        if (actionContext.Request.Headers.Authorization == null)

        //        {

        //            actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

        //        }
        //        else
        //        {

        //            actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);

        //            var httpRequestHeaderToken = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();


        //            if (httpRequestHeaderToken.Equals("123456"))
        //            {

        //                // actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
        //                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);

        //                base.OnAuthorization(actionContext);

        //            }
        //            else
        //            {
        //                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

        //            }
        //        }
        //    }
        //    catch

        //    {

        //        actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);

        //    }

        //}



    }



}