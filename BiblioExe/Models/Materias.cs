using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioExe.Models
{
    public class Materias
    {
        [Key]
        [Required]
        public int IDMateria { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }
        
        [Required]
        [MaxLength(1)]
        public int Semestre { get; set; }

        [Required]
        public int Carrera { get; set; }
    }
}
