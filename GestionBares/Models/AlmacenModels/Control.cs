using System;

namespace GestionBares.Models.AlmacenModels
{
    public class Control
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int BarId { get; set; }
        public int ProductoId { get; set; }
        public double Cantidad { get; set; }
    }
}