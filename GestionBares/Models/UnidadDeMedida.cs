using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class UnidadDeMedida
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}