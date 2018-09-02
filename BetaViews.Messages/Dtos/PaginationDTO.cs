namespace BetaViews.Messages.Dtos
{
    public class PaginationDTO
    {
        /// <summary>
        /// inicia com 1 sempre. define o numero de paginacao
        /// </summary>
        public int ActualPageNumber { get; set; }
        /// <summary>
        /// total de registros de uma paginacao
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// Retorna o total de linhas por página
        /// </summary>
        public int RowsPerPage { get; set; }

        /// <summary>
        /// Nome do botao de acao da paginacao
        /// </summary>
        public string BtnPaginationName { get; set; }

    }
}
