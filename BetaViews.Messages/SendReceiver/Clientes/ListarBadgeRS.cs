using BetaViews.Messages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages.SendReceiver.Clientes
{
    public class ListarBadgeRS : BaseMessageResponse
    {
        public List<BadgeDTO> Badges { get; set; }
    }
}
