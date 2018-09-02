namespace BetaViews.Messages.Dtos
{
    public class BadgeDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Icone { get; set; }

        /// <summary>
        /// 1-produto ou 2 loja
        /// </summary>
        public int TipoBadge { get; set; }

        public int IdCliente { get; set; }

    }
}
