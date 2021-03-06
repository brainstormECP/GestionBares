using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public enum TipoControl
    {
        InicioDeTurno, FinDeTurno
    }
    public class ControlExistencia
    {
        public int Id { get; set; }

        [Display(Name = "Turno")]
        public int TurnoId { get; set; }
        public virtual Turno Turno { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
        public bool Aprobado { get; set; }
        public virtual ICollection<DetalleControlExistencia> Detalles { get; set; }
        public ControlExistencia()
        {
            Detalles = new HashSet<DetalleControlExistencia>();
        }
    }
}