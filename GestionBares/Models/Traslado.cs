using System;
using System.ComponentModel.DataAnnotations;

namespace GestionBares.Models
{
    public class Traslado
    {
        public int Id { get; set; }
        public int TurnoId { get; set; }
        public virtual Turno Turno { get; set; }
        public DateTime Fecha { get; set; }

        [Display(Name = "Producto")]
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
        public double Cantidad { get; set; }
        [Display(Name = "Origen")]
        public int OrigenId { get; set; }
        public virtual Bar Origen { get; set; }
        [Display(Name = "Destino")]
        public int DestinoId { get; set; }
        public virtual Bar Destino { get; set; }
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}