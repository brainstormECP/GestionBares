using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class PedidoAlmacenVenta
    {
        public int Id { get; set; }
        [Display(Name = "Turno")]
        public int TurnoId { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual ICollection<DetallePedidoAlmacenVenta> Detalles { get; set; }
        public PedidoAlmacenVenta()
        {
            Detalles = new HashSet<DetallePedidoAlmacenVenta>();
        }
    }
}