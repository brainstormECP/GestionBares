using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class PedidoAlmacen
    {
        public int Id { get; set; }
        [Display(Name = "Turno")]
        public int TurnoId { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual ICollection<DetallePedidoAlmacen> Detalles { get; set; }
        public PedidoAlmacen()
        {
            Detalles = new HashSet<DetallePedidoAlmacen>();
        }
    }
}