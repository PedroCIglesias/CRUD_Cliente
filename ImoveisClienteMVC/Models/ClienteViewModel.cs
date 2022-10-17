using System.ComponentModel.DataAnnotations;

namespace ImoveisClienteMVC.Models
{
    public class ClienteViewModel
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string TelefoneContato { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
