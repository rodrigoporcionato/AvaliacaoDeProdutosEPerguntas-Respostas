
using System.Runtime.Serialization;

namespace BetaViews.Messages.Dtos
{
    /// <summary>
    /// Informações da loja do cliente. Usado em avaliações e QeA
    /// </summary>
    public  class LojaDTO
    {
        [IgnoreDataMember]
        public int IdCliente { get; set; }

        public string LojaCodigo { get; set; }

        public string LojaNome  { get; set; }

        public bool LojaMarketPlace { get; set; }
    }
}
