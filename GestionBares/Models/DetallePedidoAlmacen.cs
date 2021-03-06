using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class DetallePedidoAlmacen
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public virtual PedidoAlmacen Pedido { get; set; }
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
        public double Cantidad { get; set; }
        public bool Atendido { get; set; }
    }
}