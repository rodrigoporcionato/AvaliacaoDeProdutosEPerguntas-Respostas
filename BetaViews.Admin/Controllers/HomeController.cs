using BetaViews.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaViews.Messages.Models;

namespace BetaViews.Admin.Controllers
{
    /// <summary>
    /// dashboard
    /// </summary>
    [CustomAuthorize(PaginaAcessoEnum.Principal_DASHBOARD)]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}