using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class FamiliaDeProducto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}