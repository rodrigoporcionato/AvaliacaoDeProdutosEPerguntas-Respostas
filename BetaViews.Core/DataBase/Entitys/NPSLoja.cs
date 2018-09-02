namespace BetaViews.Core.DataBase.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NPSLoja")]
    public partial class NPSLoja
    {
        public int Id { get; set; }

        public int IdLoja { get; set; }

        public int Nota { get; set; }

        public DateTime DtAvaliacao { get; set; }

        public string Canal { get; set; }

        public virtual Loja Loja { get; set; }
    }
}
