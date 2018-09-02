using AutoMapper;
using BetaViews.Core.DataBase.Entitys;
using BetaViews.Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetaViews.Admin.App_Start
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration MapperConfiguration;

        public static void RegisterMappings()
        {
            MapperConfiguration = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ClienteAcesso, ClienteAcessoModel>().ReverseMap();
                    cfg.CreateMap<LogErros, LogErrosModel>().ReverseMap();
            });
        }
    }
}