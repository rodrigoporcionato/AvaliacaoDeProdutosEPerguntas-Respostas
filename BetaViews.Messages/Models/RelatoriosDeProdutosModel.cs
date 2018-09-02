using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetaViews.Messages.Models
{
    public class RelatoriosDeProdutosModel
    {
        public List<ProdutoModel> TopMaisAvaliado { get; set; }
        public List<ProdutoModel> TopMenosAvaliado { get; set; }
        public List<ProdutoModel> Produtos{ get; set; }
    }
}