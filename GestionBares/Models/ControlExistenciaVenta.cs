using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class ControlExistenciaVenta
    {
        public int Id { get; set; }

        [Display(Name = "Turno")]
        public int TurnoId { get; set; }
        public virtual Turno Turno { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
        public bool Aprobado { get; set; }
        public virtual ICollection<DetalleControlExistenciaVenta> Detalles { get; set; }
        public ControlExistenciaVenta()
        {
            Detalles = new HashSet<DetalleControlExistenciaVenta>();
        }
    }
}