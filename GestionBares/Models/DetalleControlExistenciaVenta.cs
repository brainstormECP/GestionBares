using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class DetalleControlExistenciaVenta
    {
        public int Id { get; set; }
        [Display(Name = "Control")]
        public int ControlId { get; set; }
        public virtual ControlExistenciaVenta Control { get; set; }
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
        public double Cantidad { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
    }
}