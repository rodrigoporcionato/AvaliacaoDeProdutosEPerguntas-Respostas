using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetaViews.Messages.Models
{
    public class RelatoriosDeLojas
    {
        public List<LojaModel> TopMaisAvaliado { get; set; }
        public List<LojaModel> TopMenosAvaliado { get; set; }
        public List<LojaModel> Lojas { get; set; }

    }
}