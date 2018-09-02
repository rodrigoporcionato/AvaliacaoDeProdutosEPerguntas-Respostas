using System.ComponentModel.DataAnnotations;


namespace BetaViews.API.Models
{
    
    public class AvaliacaoModel
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }

    }
}