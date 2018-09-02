using BetaViews.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetaViews.Messages.Models;
using BetaViews.Core.DataBase.Repository.Interface;
using System.Threading.Tasks;
using BetaViews.Messages.Dtos;
using PagedList;
using AutoMapper;
using BetaViews.Admin.App_Start;
using System.Web.UI;

namespace BetaViews.Admin.Controllers.LogsSistema
{

    [CustomAuthorize(PaginaAcessoEnum.LogsSistema_LogsDoSistema)]
    public class LogsSistema_LogsDoSistemaController : Controller
    {
        public IMapper Mapper;
        private readonly ILogErrosRepository logService;
        public LogsSistema_LogsDoSistemaController(ILogErrosRepository logService)
        {
            this.logService = logService;
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

        [OutputCache(Duration = 360, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
        public async Task<ActionResult> Index(int? page, string busca)
        {            
            var pagination = new PaginationDTO();
            pagination.ActualPageNumber = page ?? 1;
            pagination.RowsPerPage = 20;

            var logs = await logService.GetAllAsync();

            var model = Mapper.Map<List<LogErrosModel>>(logs.ToList().OrderByDescending(x=> x.Id));
            return View(model.ToPagedList(pagination.ActualPageNumber, pagination.RowsPerPage));
        }
    }
}