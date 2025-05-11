using System.ComponentModel.DataAnnotations;

namespace BiblioExe.Models
{
    public class Estado
    {
        [Key]
        public int IDEstado { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "La descripcion del estado no puede ser mayor a 10 caracteres")]
        public string EstadoNombre { get; set; }
    }
}
