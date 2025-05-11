using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiblioExe.Models
{
    public class Alumno
    {
        [Key]
        public string Boleta { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El nombre no puede tener mas de 50 caracteres")]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El Apellido Paterno no puede tener mas de 50 caracteres")]
        public string AP { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El Apellido Materno no puede tener mas de 50 caracteres")]
        public string AM { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "El grupo no puede ser mayor a 5 caracteres")]
        public string Grupo { get; set; }

        [Required]
        [MaxLength(1, ErrorMessage = "El semestre solo es de un digito")]
        public int Semestre { get; set; }

        [Required]
        public int IDCarrera { get; set; }

        [ForeignKey("IDCarrera")]
        public Carrera Carrera { get; set; }

    }
}
