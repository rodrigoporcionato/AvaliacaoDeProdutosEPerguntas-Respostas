using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages.SendReceiver.QA.ListarPergunta
{
    public class ListarPerguntaRQ
    {
        public int IdCliente { get; set; }
        public int ActualPageNumber { get; set; }
        public string Busca { get; set; }
    }
}
