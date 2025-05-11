using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioExe.Models
{
    public class Libro
    {
        [Key]
        public int IDLibro { get; set; }

        [Required]
        [MaxLength(120, ErrorMessage = "El nombre no puede ser mayor a 120 caracteres")]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "El autor del libro no puede contener mas de 50 caracteres")]
        public string Autor { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "El nombre de la editorial no puede ser mayor a 50 caracteres")]
        public string Editorial { get; set; }

        [Required]
        public string Sinopsis { get; set; }

        [Required]
        public string Imagen { get; set; }

        [Required]
        public int IDCategoria { get; set; }

        [ForeignKey ("IDCategoria")]
        public Categoria Categoria { get; set; }
    }
}
