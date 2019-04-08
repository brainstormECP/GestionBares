using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class DetallePedidoAlmacen
    {
        public int Id { get; set; }
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
        public double Cantidad { get; set; }
        public bool Atendido { get; set; }
    }
}