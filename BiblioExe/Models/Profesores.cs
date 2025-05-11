using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BiblioExe.Models
{
    public class Profesores
    {
        [Key]
        [Required]
        public int IDProfesor { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(200)]
        public string ApellidoP { get; set; }

        [Required]
        [MaxLength(200)]
        public string ApellidoM { get; set; }

        [Required]
        [MaxLength (1)]
        public int Semestre { get; set; }

        [Required]
        [MaxLength(1)]
        public int Carrera { get; set; }
    }
}
