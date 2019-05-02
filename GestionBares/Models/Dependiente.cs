using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionBares.Models
{
    public class Dependiente
    {
        public int Id { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        public bool Activo { get; set; }
        [NotMapped]
        public string NombreCompleto { get { return Nombres + " " + Apellidos; } }

        [Display(Name = "Usuario")]
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}