using System.ComponentModel.DataAnnotations;

namespace BiblioExe.Models
{
    public class Carrera
    {
        [Key]
        public int IDCarrera { get; set; }

        [Required]
        [MaxLength(36, ErrorMessage = "El nombre de la carrera no puede ser mayor a 36 caracteres")]
        public string DescCarrera { get; set; }
    }
}
