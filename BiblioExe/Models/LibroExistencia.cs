using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioExe.Models
{
    public class LibroExistencia
    {
        [Key]
        public int IDLibro { get; set; }

        [Required]
        public int Existencia { get; set; }
    }
}
