using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiblioExe.Models
{
    public class DatosAlumno
    {
        [Key]
        public string Boleta { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(80, ErrorMessage = "El correo no puede tener una longitud mayor a 80")]
        [MinLength(5, ErrorMessage = "El correo no puede tener menos de 5 caracteres")]
        public string Email { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "La contraseña no puede ser mayor a 20 caracteres")]
        [MinLength(8, ErrorMessage = "La contrasea debe contener minimo 8 caracteres")]
        public string Contraseña { get; set; }

        [Required]
        public string IDTipoUsuario { get; set; }

        public Guid? TokenRestauracion { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "El formato no es correcto")]
        public DateTime FechaCaducidadToken { get; set; }

        [ForeignKey("IDTipoUsuario")]
        public TipoUsuario TipoUsuario { get; set; }

        [ForeignKey("Boleta")]
        public Alumno Alumno { get; set; }
    }
}
