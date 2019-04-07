using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class EntregaDeAlmacen
    {
        public int Id { get; set; }
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
        [Display(Name = "Turno")]
        public int TurnoId { get; set; }
        public virtual Turno Turno { get; set; }
        public double Cantidad { get; set; }
    }
}