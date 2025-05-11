using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioExe.Models
{
    public class Grupos
    {
        [Key]
        public int IDGrupo { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "El grupo no puede ser mayor a 5 caracteres")]
        public string Grupo { get; set; }
    }
}
