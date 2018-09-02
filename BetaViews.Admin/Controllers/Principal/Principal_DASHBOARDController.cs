using BetaViews.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaViews.Messages.Models;

namespace BetaViews.Admin.Controllers.Principal
{
    
    [CustomAuthorize(PaginaAcessoEnum.Principal_DASHBOARD)]
    public class Principal_DASHBOARDController : Controller
    {
        // GET: Principal_DASHBOARD
        
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult teste()
        {
            return View();
        }
    }
}