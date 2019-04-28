using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionBares.Models
{
    public class Turno
    {
        public int Id { get; set; }
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }
        [Display(Name = "Fecha Fin")]
        public DateTime? FechaFin { get; set; }
        public bool Activo { get; set; }
        [Display(Name = "Dependiente")]
        public int DependienteId { get; set; }
        public virtual Dependiente Dependiente { get; set; }
        [Display(Name = "Bar")]
        public int BarId { get; set; }
        public virtual Bar Bar { get; set; }

        [NotMapped]
        public string Descripcion { get => String.Format("{0} - {1}", Dependiente != null ? Dependiente.Nombre : "", Bar != null ? Bar.Nombre : ""); }
    }
}
