using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Display(Name = "Unidad de Medida")]
        public int UnidadId { get; set; }
        public virtual UnidadDeMedida Unidad { get; set; }
        [Display(Name = "Familia")]
        public int FamiliaId { get; set; }
        public virtual FamiliaDeProducto Familia { get; set; }
        public double LimiteParaSolicitar { get; set; }
    }
}