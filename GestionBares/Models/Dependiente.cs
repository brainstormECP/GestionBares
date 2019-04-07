using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class Dependiente
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}