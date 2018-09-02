using BetaViews.Messages.Dtos;
using System.ComponentModel.DataAnnotations;

namespace BetaViews.Messages.SendReceiver.QA
{
    /// <summary>
    /// Request para Q&A
    /// </summary>
    public class PerguntasERespostasDisplayRQ : TokenAuthorizationDTO
    {


        /// <summary>
        /// Codigo do produto
        /// </summary>
        /// <value>
        /// Código do Produto interno da sua loja.
        /// </value>        
        [Required]        
        public string PrdCodigo { get; set; }
        /// <summary>
        /// Codigo da sua loja
        /// </summary>
        [Required]
        public string CodigoLoja { get; set; }
        /// <summary>
        /// codigo do cliente interno
        /// </summary>
        [Required]
        public string CodCliente { get; set; }
        /// <summary>
        /// usado para paginação começando com 1
        /// </summary>        
        public int PageNumber { get; set; }
        /// <summary>
        /// use filtros especificos para filtrar as Perguntas & Respostas
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// usado para buscar por termo nas Perguntas & Respostas
        /// </summary>
        public string Busca { get; set; }
    }
}
