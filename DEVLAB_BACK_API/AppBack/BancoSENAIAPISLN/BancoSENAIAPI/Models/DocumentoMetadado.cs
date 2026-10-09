using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Models
{
    public class DocumentoMetadado
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Extensão { get; set;}
        [Required]
        public string Caminho { get; set; }
        [Required]
        public int CodigoCliente  { get; set; }
        
    }
}
