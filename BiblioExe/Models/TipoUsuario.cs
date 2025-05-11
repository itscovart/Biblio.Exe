using System.ComponentModel.DataAnnotations;

namespace BiblioExe.Models
{
    public class TipoUsuario
    {
        [Key]
        [MaxLength(1, ErrorMessage = "El rol del usuario no puede ser mayor a 1 caracter")]
        public string IDTipoUsuario { get; set; }

        [Required]
        [MaxLength(13, ErrorMessage = "La descripcion del tipo de usuario no debe contener mas de 13 caracteres")]
        public string DescTipo { get; set; }
    }
}
