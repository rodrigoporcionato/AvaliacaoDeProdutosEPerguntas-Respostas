using BetaViews.Messages.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages.Models
{
    public class MarcasModel : PaginationDTO
    {
        public int Id { get; set; }
        public int IdLoja { get; set; }
        public string Marca { get; set; }        
        public int s1 { get; set; }
        public int s2 { get; set; }
        public int s3 { get; set; }
        public int s4 { get; set; }
        public int s5 { get; set; }
        public int TotalAvaliacoes { get; set; }
        public double MediaTotal { get; set; }

    }
}
