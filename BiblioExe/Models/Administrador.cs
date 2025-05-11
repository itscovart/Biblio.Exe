using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiblioExe.Models
{
    public class Administrador
    {
        [Key]
        public int IDAdmin { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Ap { get; set; }

        [Required]
        public string Am { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "El correo no puede tener una longitud mayor a 80")]
        [MinLength(5, ErrorMessage = "El correo no puede tener menos de 5 caracteres")]
        public string Correo { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "La contraseña no puede ser mayor a 20 caracteres")]
        [MinLength(8, ErrorMessage = "La contrasea debe contener minimo 8 caracteres")]
        public string Contraseña { get; set; }

        [Required]
        public string IDTipoUsuario { get; set; }

        [ForeignKey ("IDTipoUsuario")]
        public TipoUsuario TipoUsuario { get; set; }
    }
}
