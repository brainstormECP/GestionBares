using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public virtual Venta Venta { get; set; }
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
        public double Cantidad { get; set; }
        public decimal Importe { get; set; }
    }
}