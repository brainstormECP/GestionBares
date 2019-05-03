using System;
using System.Collections.Generic;

namespace GestionBares.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int TurnoId { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual ICollection<DetalleVenta> Detalles { get; set; }
        public Venta()
        {
            Detalles = new HashSet<DetalleVenta>();
        }
    }
}