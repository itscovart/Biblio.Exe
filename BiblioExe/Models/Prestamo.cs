using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioExe.Models
{
    public class Prestamo
    {
        [Key]
        public string Folio { get; set; }

        [Required]
        public int IDLibro { get; set; }

        [Required]
        public string Boleta { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "El grupo no puede ser mayor a 5 caracteres")]
        public string Grupo { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "La materia no puede contener mas de 50 caracterese")]
        public string Materia { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "El nombre del Maestro no debe de contener mas de 80 caracteres")]
        public string Maestro { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha_inicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha_final { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public int IDEstado { get; set; }

        public DateTime FechaEntrega { get; set; }

        [ForeignKey("Boleta")]
        public Alumno Alumno { get; set; }

        [ForeignKey("IDEstado")]
        public Estado Estado { get; set; }

        [ForeignKey("IDLibro")]
        public Libro Libro { get; set; }


    }
}
