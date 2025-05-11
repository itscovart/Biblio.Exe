using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiblioExe.Models
{
    public class Categoria
    {
        [Key]
        public int IDCategoria { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "La categoria no puede ser mayor a 20 caracteres")]
        public string DescCategoria { get; set; }
    }
}
