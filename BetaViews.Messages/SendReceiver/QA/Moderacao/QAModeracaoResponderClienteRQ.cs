using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BetaViews.Messages.SendReceiver.QA.Moderacao
{
    public class QAModeracaoResponderClienteRQ
    {
        public int IdQuestion { get; set; }
        public int IdClienteAcesso { get; set; }
        public string NomeRespondente { get; set; }
        public int IdBadge { get; set; }
        [AllowHtml]
        public string Resposta { get; set; }
    }
}
