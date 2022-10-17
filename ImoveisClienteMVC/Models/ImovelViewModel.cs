using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImoveisClienteMVC.Models
{
    public class ImovelViewModel
    {
        [Key]
        public int ImovelId { get; set; }
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
