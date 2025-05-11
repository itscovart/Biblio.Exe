using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioExe.Models
{
    public class SolicitudPrestamo
    {

        [Key]
        public int IDSolicitud { get; set; }

        [Required]
        public int IDLibro { get; set; }

        [Required]
        public string Boleta { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "El grupo no puede ser mayor a 5 caracteres")]
        public string Grupo { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "La materia no puede contener mas de 100 caracterese")]
        public string Materia { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "El nombre del Maestro no debe de contener mas de 150 caracteres")]
        public string Maestro { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha_inicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha_final { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [ForeignKey("Boleta")]
        public Alumno BoletaAlumno { get; set; }

        [ForeignKey("IDLibro")]
        public Libro Libro { get; set; }

    }
}
