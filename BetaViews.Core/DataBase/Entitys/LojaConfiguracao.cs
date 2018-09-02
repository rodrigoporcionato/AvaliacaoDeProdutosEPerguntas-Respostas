namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LojaConfiguracao")]
    public partial class LojaConfiguracao
    {
        public int Id { get; set; }

        public int IdLoja { get; set; }

        public bool ReviewClienteLogado { get; set; }

        public string PaginaRedirecionaLogin { get; set; }

        public bool FlagUploadImagem { get; set; }

        public int TotPageSize { get; set; }

        public bool FaceBookShare { get; set; }

        public bool TwitterShare { get; set; }

        public int FlagTermoAceite { get; set; }

        [Required]
        public string TermoAceite { get; set; }

        public bool FlagQA { get; set; }

        public string GA { get; set; }

        public virtual Loja Loja { get; set; }
    }
}
