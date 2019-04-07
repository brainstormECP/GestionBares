using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class DetalleControlExistencia
    {
        public int Id { get; set; }
        [Display(Name = "Contrato")]
        public int ControlId { get; set; }
        public virtual ControlExistencia Control { get; set; }
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
        public double Cantidad { get; set; }
    }
}